using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MauiReactor.Internals;

namespace MauiReactor;

public partial interface ISwipeView
{
    SwipeItems? LeftItems { get; set; }
    SwipeItems? RightItems { get; set; }
    SwipeItems? TopItems { get; set; }
    SwipeItems? BottomItems { get; set; }
}

public partial class SwipeView<T>
{
    SwipeItems? ISwipeView.LeftItems { get; set; }
    SwipeItems? ISwipeView.RightItems { get; set; }
    SwipeItems? ISwipeView.TopItems { get; set; }
    SwipeItems? ISwipeView.BottomItems { get; set; }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwipeView = (ISwipeView)this;

        var children = base.RenderChildren();

        if (thisAsISwipeView.LeftItems != null)
        {
            children = children.Concat(new[] { thisAsISwipeView.LeftItems });
        }
        if (thisAsISwipeView.RightItems != null)
        {
            children = children.Concat(new[] { thisAsISwipeView.RightItems });
        }
        if (thisAsISwipeView.TopItems != null)
        {
            children = children.Concat(new[] { thisAsISwipeView.TopItems });
        }
        if (thisAsISwipeView.BottomItems != null)
        {
            children = children.Concat(new[] { thisAsISwipeView.BottomItems });
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwipeView = (ISwipeView)this;

        if (childControl is Microsoft.Maui.Controls.SwipeItems swipeItems)
        {
            if (widget == thisAsISwipeView.LeftItems)
            {
                NativeControl.LeftItems = swipeItems;
            }
            else if (widget == thisAsISwipeView.RightItems)
            {
                NativeControl.RightItems = swipeItems;
            }
            else if (widget == thisAsISwipeView.TopItems)
            {
                NativeControl.TopItems = swipeItems;
            }
            else if (widget == thisAsISwipeView.BottomItems)
            {
                NativeControl.BottomItems = swipeItems;
            }
        }
        else
        {
            base.OnAddChild(widget, childControl);
        }
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        base.OnRemoveChild(widget, childControl);
    }
}

public static partial class SwipeViewExtensions
{
    public static T LeftItems<T>(this T swipeView, SwipeItems items)
        where T : ISwipeView
    {
        swipeView.LeftItems = items;
        return swipeView;
    }

    public static T RightItems<T>(this T swipeView, SwipeItems items)
        where T : ISwipeView
    {
        swipeView.RightItems = items;
        return swipeView;
    }
    public static T TopItems<T>(this T swipeView, SwipeItems items)
        where T : ISwipeView
    {
        swipeView.TopItems = items;
        return swipeView;
    }
    public static T BottomItems<T>(this T swipeView, SwipeItems items)
        where T : ISwipeView
    {
        swipeView.BottomItems = items;
        return swipeView;
    }
}
