using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IContentPage
{
    VisualNode? TitleView { get; set; }
}

public partial class ContentPage<T> : TemplatedPage<T>, IContentPage where T : Microsoft.Maui.Controls.ContentPage, new()
{
    VisualNode? IContentPage.TitleView { get; set; }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsIVisualElement = (IContentPage)this;

        var children = base.RenderChildren();

        if (thisAsIVisualElement.TitleView != null)
        {
            children = children.Concat([thisAsIVisualElement.TitleView]);
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIVisualElement = (IContentPage)this;

        if (thisAsIVisualElement.TitleView == widget && childControl is View titleView)
        {
            NativeControl.SetValue(Microsoft.Maui.Controls.Shell.TitleViewProperty, titleView);
            return;
        }
        else if (childControl is View view)
        {
            NativeControl.Content = view;
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIVisualElement = (IContentPage)this;

        if (thisAsIVisualElement.TitleView == widget && childControl is View _)
        {
            NativeControl.SetValue(Microsoft.Maui.Controls.Shell.TitleViewProperty, null);
            return;
        }
        else if (childControl is View)
        {
            NativeControl.Content = null;
        }

        base.OnRemoveChild(widget, childControl);
    }
}


public partial class ContentPage
{
    partial class ContentPageWithBackButtonPressedOverriden : Microsoft.Maui.Controls.ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            var backButtonBehavior = (BackButtonBehavior?)this.GetValue(Microsoft.Maui.Controls.Shell.BackButtonBehaviorProperty);

            if (backButtonBehavior != null &&
                backButtonBehavior.Command != null &&
                backButtonBehavior.Command.CanExecute(null))
            {
                //we want to handle back button pressed event (including physical button on Android)
                backButtonBehavior.Command.Execute(null);
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }

    public ContentPage(string title) => this.Title(title);

    protected override void OnMount()
    {
        _nativeControl ??= new ContentPageWithBackButtonPressedOverriden();

        base.OnMount();
    }
}


public partial class Component
{
    public static ContentPage ContentPage(string title)
        => new ContentPage().Title(title);

    public static ContentPage ContentPage(string title, params IEnumerable<VisualNode?>? children)
        => ContentPage(children).Title(title);
}

public static partial class ContentPageExtensions
{
    public static T HasNavigationBar<T>(this T contentPage, bool hasNavigationBar)
        where T : IContentPage
    {
        contentPage.SetProperty(Microsoft.Maui.Controls.NavigationPage.HasNavigationBarProperty, hasNavigationBar);
        return contentPage;
    }

    public static T TitleView<T>(this T contentPage, VisualNode? titleView)
        where T : IContentPage
    {
        contentPage.TitleView = titleView;
        return contentPage;
    }

    public static T TitleView<T>(this T contentPage, Func<VisualNode?>? titleView)
        where T : IContentPage
    {
        contentPage.TitleView = titleView?.Invoke();
        return contentPage;
    }
}
