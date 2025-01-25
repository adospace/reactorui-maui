// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable
namespace MauiReactor;
public partial interface IMenuItem : IBaseMenuItem
{
    EventCommand<EventArgs>? ClickedEvent { get; set; }
}

public partial class MenuItem<T> : BaseMenuItem<T>, IMenuItem where T : Microsoft.Maui.Controls.MenuItem, new()
{
    public MenuItem(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        MenuItemStyles.Default?.Invoke(this);
    }

    EventCommand<EventArgs>? IMenuItem.ClickedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && MenuItemStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<EventArgs>? _executingClickedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIMenuItem = (IMenuItem)this;
        if (thisAsIMenuItem.ClickedEvent != null)
        {
            NativeControl.Clicked += NativeControl_Clicked;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Clicked(object? sender, EventArgs e)
    {
        var thisAsIMenuItem = (IMenuItem)this;
        if (_executingClickedEvent == null || _executingClickedEvent.IsCompleted)
        {
            _executingClickedEvent = thisAsIMenuItem.ClickedEvent;
            _executingClickedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Clicked -= NativeControl_Clicked;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is MenuItem<T> @menuitem)
        {
            if (_executingClickedEvent != null && !_executingClickedEvent.IsCompleted)
            {
                @menuitem._executingClickedEvent = _executingClickedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class MenuItem : MenuItem<Microsoft.Maui.Controls.MenuItem>
{
    public MenuItem(Action<Microsoft.Maui.Controls.MenuItem?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public MenuItem(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class MenuItemExtensions
{
    public static T IsDestructive<T>(this T menuItem, bool isDestructive)
        where T : IMenuItem
    {
        //menuItem.IsDestructive = isDestructive;
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IsDestructiveProperty, isDestructive);
        return menuItem;
    }

    public static T IsDestructive<T>(this T menuItem, Func<bool> isDestructiveFunc)
        where T : IMenuItem
    {
        //menuItem.IsDestructive = new PropertyValue<bool>(isDestructiveFunc);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IsDestructiveProperty, new PropertyValue<bool>(isDestructiveFunc));
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, Microsoft.Maui.Controls.ImageSource iconImageSource)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = iconImageSource;
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, iconImageSource);
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, Func<Microsoft.Maui.Controls.ImageSource> iconImageSourceFunc)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(iconImageSourceFunc);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(iconImageSourceFunc));
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, string file)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = Microsoft.Maui.Controls.ImageSource.FromFile(file);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromFile(file));
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, Func<string> action)
        where T : IMenuItem
    {
        /*menuItem.IconImageSource = new PropertyValue<Microsoft.Maui.Controls.ImageSource>(
            () => Microsoft.Maui.Controls.ImageSource.FromFile(action()));*/
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, new PropertyValue<Microsoft.Maui.Controls.ImageSource>(() => Microsoft.Maui.Controls.ImageSource.FromFile(action())));
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, string resourceName, Assembly sourceAssembly)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromResource(resourceName, sourceAssembly));
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, Uri imageUri)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = Microsoft.Maui.Controls.ImageSource.FromUri(imageUri);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromUri(imageUri));
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, Uri imageUri, bool cachingEnabled, TimeSpan cacheValidity)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = new Microsoft.Maui.Controls.UriImageSource
        //{
        //    Uri = imageUri,
        //    CachingEnabled = cachingEnabled,
        //    CacheValidity = cacheValidity
        //};
        var newValue = new Microsoft.Maui.Controls.UriImageSource
        {
            Uri = imageUri,
            CachingEnabled = cachingEnabled,
            CacheValidity = cacheValidity
        };
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, newValue);
        return menuItem;
    }

    public static T IconImageSource<T>(this T menuItem, Func<Stream> imageStream)
        where T : IMenuItem
    {
        //menuItem.IconImageSource = Microsoft.Maui.Controls.ImageSource.FromStream(imageStream);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.IconImageSourceProperty, Microsoft.Maui.Controls.ImageSource.FromStream(imageStream));
        return menuItem;
    }

    public static T Text<T>(this T menuItem, string text)
        where T : IMenuItem
    {
        //menuItem.Text = text;
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.TextProperty, text);
        return menuItem;
    }

    public static T Text<T>(this T menuItem, Func<string> textFunc)
        where T : IMenuItem
    {
        //menuItem.Text = new PropertyValue<string>(textFunc);
        menuItem.SetProperty(Microsoft.Maui.Controls.MenuItem.TextProperty, new PropertyValue<string>(textFunc));
        return menuItem;
    }

    public static T OnClicked<T>(this T menuItem, Action? clickedAction)
        where T : IMenuItem
    {
        menuItem.ClickedEvent = new SyncEventCommand<EventArgs>(execute: clickedAction);
        return menuItem;
    }

    public static T OnClicked<T>(this T menuItem, Action<EventArgs>? clickedAction)
        where T : IMenuItem
    {
        menuItem.ClickedEvent = new SyncEventCommand<EventArgs>(executeWithArgs: clickedAction);
        return menuItem;
    }

    public static T OnClicked<T>(this T menuItem, Action<object?, EventArgs>? clickedAction)
        where T : IMenuItem
    {
        menuItem.ClickedEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: clickedAction);
        return menuItem;
    }

    public static T OnClicked<T>(this T menuItem, Func<Task>? clickedAction, bool runInBackground = false)
        where T : IMenuItem
    {
        menuItem.ClickedEvent = new AsyncEventCommand<EventArgs>(execute: clickedAction, runInBackground);
        return menuItem;
    }

    public static T OnClicked<T>(this T menuItem, Func<EventArgs, Task>? clickedAction, bool runInBackground = false)
        where T : IMenuItem
    {
        menuItem.ClickedEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: clickedAction, runInBackground);
        return menuItem;
    }

    public static T OnClicked<T>(this T menuItem, Func<object?, EventArgs, Task>? clickedAction, bool runInBackground = false)
        where T : IMenuItem
    {
        menuItem.ClickedEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: clickedAction, runInBackground);
        return menuItem;
    }
}

public static partial class MenuItemStyles
{
    public static Action<IMenuItem>? Default { get; set; }
    public static Dictionary<string, Action<IMenuItem>> Themes { get; } = [];
}