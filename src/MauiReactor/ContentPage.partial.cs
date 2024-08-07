﻿using MauiReactor.Internals;

namespace MauiReactor;

public partial class ContentPage<T> : TemplatedPage<T>, IContentPage where T : Microsoft.Maui.Controls.ContentPage, new()
{
    public ContentPage(string title)
        : base(title)
    {

    }

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
    public ContentPage(string title)
        : base(title)
    {

    }

    public ContentPage(VisualNode content)
    {
        _internalChildren.Add(content);
    }

}

public partial class Component
{
    public static ContentPage ContentPage(string title) 
        => new ContentPage().Title(title);

    public static ContentPage ContentPage(string title, params VisualNode?[]? children)
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
