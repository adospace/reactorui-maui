using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface ITitleBar
{
    VisualNode? LeadingContent { get; set; }

    VisualNode? TrailingContent { get; set; }

    VisualNode? Content { get; set; }
}

public abstract partial class TitleBar<T>
{
    VisualNode? ITitleBar.LeadingContent { get; set; }
    VisualNode? ITitleBar.TrailingContent { get; set; }
    VisualNode? ITitleBar.Content { get; set; }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsITitleBar = (ITitleBar)this;

        var children = base.RenderChildren();

        if (thisAsITitleBar.LeadingContent != null)
        {
            children = children.Concat([thisAsITitleBar.LeadingContent]);
        }
        if (thisAsITitleBar.TrailingContent != null)
        {
            children = children.Concat([thisAsITitleBar.TrailingContent]);
        }
        if (thisAsITitleBar.Content != null)
        {
            children = children.Concat([thisAsITitleBar.Content]);
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsITitleBar = (ITitleBar)this;

        if (widget == thisAsITitleBar.LeadingContent)
        {
            NativeControl.LeadingContent = childControl as View;
        }
        else if (widget == thisAsITitleBar.TrailingContent)
        {
            NativeControl.TrailingContent = childControl as View;
        }
        else // if (widget == thisAsITitleBar.Content) also handle the case when content is added as a child
        {
            NativeControl.Content = childControl as View;
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsITitleBar = (ITitleBar)this;

        if (widget == thisAsITitleBar.LeadingContent)
        {
            NativeControl.LeadingContent = null;
        }
        else if (widget == thisAsITitleBar.TrailingContent)
        {
            NativeControl.TrailingContent = null;
        }
        else //if (widget == thisAsITitleBar.Content)
        {
            NativeControl.Content = null;
        }

        base.OnRemoveChild(widget, childControl);
    }

}

public static partial class TitleBarExtensions
{
    public static T LeadingContent<T>(this T titleBar, VisualNode content) where T : ITitleBar
    {
        titleBar.LeadingContent = content;
        return titleBar;
    }
    public static T TrailingContent<T>(this T titleBar, VisualNode content) where T : ITitleBar
    {
        titleBar.TrailingContent = content;
        return titleBar;
    }
    public static T Content<T>(this T titleBar, VisualNode content) where T : ITitleBar
    {
        titleBar.Content = content;
        return titleBar;
    }
}

public partial class Component
{

}