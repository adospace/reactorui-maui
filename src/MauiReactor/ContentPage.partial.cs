using MauiReactor.Internals;

namespace MauiReactor;

public partial class ContentPage<T> : TemplatedPage<T>, IContentPage where T : Microsoft.Maui.Controls.ContentPage, new()
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is View view)
        {
            NativeControl.Content = view;
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is View)
        {
            NativeControl.Content = null;
        }

        base.OnRemoveChild(widget, childControl);
    }
}


public partial class ContentPage
{
    private class ContentPageWithBackButtonPressedOverriden : Microsoft.Maui.Controls.ContentPage
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
        where T : IContentPage, IVisualNodeWithAttachedProperties
    {
        contentPage.Set(Microsoft.Maui.Controls.NavigationPage.HasNavigationBarProperty, hasNavigationBar);
        return contentPage;
    }

}
