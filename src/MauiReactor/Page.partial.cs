using MauiReactor.Internals;
using System.Reflection;

namespace MauiReactor;

public partial interface IPage
{
    public string? WindowTitle { get; set; }

    public BackButtonBehavior? BackButtonBehavior { get; set; }
}


public partial class Page<T>
{
    public Page(string title) => this.Title(title);

    string? IPage.WindowTitle { get; set; }

    BackButtonBehavior? IPage.BackButtonBehavior { get; set; }

    protected override void OnBeforeMount()
    {
        var pageHost = GetParent<PageHost>();
        var containerPage = ((IVisualNode)this).GetContainerPage();

        if (pageHost != null && containerPage != null && containerPage is T nativeContainerPage)
        {
            nativeContainerPage.MenuBarItems.Clear();
            nativeContainerPage.ToolbarItems.Clear();

            _nativeControl = nativeContainerPage;
        }

        base.OnBeforeMount();
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.Controls.MenuBarItem menuBarItem)
        {
            NativeControl.MenuBarItems.Insert(widget.ChildIndex, menuBarItem);
        }
        else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
        {
            NativeControl.ToolbarItems.Insert(widget.ChildIndex, toolbarItem);
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.Controls.MenuBarItem menuBarItem)
        {
            NativeControl.MenuBarItems.Remove(menuBarItem);
        }
        else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
        {
            NativeControl.ToolbarItems.Remove(toolbarItem);
        }


        base.OnRemoveChild(widget, childControl);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        var thisAsIPage = (IPage)this;

        if (thisAsIPage.WindowTitle != null &&                 
            Application.Current != null)
        {
            Application.Current.Dispatcher.Dispatch(() =>
            {
                Validate.EnsureNotNull(NativeControl);
                if (NativeControl.Parent is Microsoft.Maui.Controls.Window parentWindow)
                {
                    parentWindow.Title = thisAsIPage.WindowTitle;
                }
            });
        }

        Validate.EnsureNotNull(NativeControl);
        NativeControl.SetPropertyValue(
            Microsoft.Maui.Controls.Shell.BackButtonBehaviorProperty, 
            thisAsIPage.BackButtonBehavior);
    }
}

public partial class PageExtensions
{
    public static T WindowTitle<T>(this T page, string title) where T : IPage
    {
        page.WindowTitle = title;
        return page;
    }

    public static T BackButtonBehavior<T>(this T page, BackButtonBehavior? behavior) where T : IPage
    {
        page.BackButtonBehavior = behavior;
        return page;
    }

    public static T OnBackButtonPressed<T>(this T page, Action? action, Func<bool>? canExecute = null) where T : IPage
    {
        if (action == null)
        {
            if (page.BackButtonBehavior != null)
            {
                page.BackButtonBehavior.Command = null;
            }
        }
        else
        {
            page.BackButtonBehavior ??= new BackButtonBehavior();
            page.BackButtonBehavior.Command = canExecute != null ? new Command(action, canExecute) : new Command(action);
        }
        return page;
    }

    public static T OnBackButtonPressed<T>(this T page, Func<Task>? action, Func<bool>? canExecute = null) where T : IPage
    {
        if (action == null)
        {
            if (page.BackButtonBehavior != null)
            {
                page.BackButtonBehavior.Command = null;
            }
        }
        else
        {
            page.BackButtonBehavior ??= new BackButtonBehavior();
            page.BackButtonBehavior.Command = canExecute != null ? new AsyncCommand(action, canExecute) : new AsyncCommand(action);
        }
        return page;
    }

    public static T BackButtonIsVisible<T>(this T page, bool isVisible) where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IsVisible = isVisible;
        return page;
    }

    public static T BackButtonIsEnabled<T>(this T page, bool isEnabled) where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IsEnabled = isEnabled;
        return page;
    }

    public static T BackButtonText<T>(this T page, string text) where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.TextOverride = text;
        return page;
    }

    public static T BackButtonIcon<T>(this T page, Microsoft.Maui.Controls.ImageSource source) 
        where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IconOverride = source;
        return page;
    }

    public static T Source<T>(this T page, string file)
        where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IconOverride = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        return page;
    }

    public static T Source<T>(this T page, string resourceName, Assembly sourceAssembly)
        where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IconOverride = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        return page;
    }

    public static T Source<T>(this T page, Uri pageUri)
        where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IconOverride = Microsoft.Maui.Controls.ImageSource.FromUri(pageUri);
        return page;
    }

    public static T Source<T>(this T page, Uri pageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IconOverride = new Microsoft.Maui.Controls.UriImageSource { Uri = pageUri, CachingEnabled = cachingEnabled, CacheValidity = cacheValidity };
        return page;
    }

    public static T Source<T>(this T page, Func<Stream> pageStream)
        where T : IPage
    {
        page.BackButtonBehavior ??= new BackButtonBehavior();
        page.BackButtonBehavior.IconOverride = Microsoft.Maui.Controls.ImageSource.FromStream(pageStream);
        return page;
    }

}
