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
public partial interface IScrollView : Compatibility.ILayout
{
    EventCommand<ScrolledEventArgs>? ScrolledEvent { get; set; }
}

public partial class ScrollView<T> : Compatibility.Layout<T>, IScrollView where T : Microsoft.Maui.Controls.ScrollView, new()
{
    public ScrollView(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        ScrollViewStyles.Default?.Invoke(this);
    }

    EventCommand<ScrolledEventArgs>? IScrollView.ScrolledEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ScrollViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<ScrolledEventArgs>? _executingScrolledEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIScrollView = (IScrollView)this;
        if (thisAsIScrollView.ScrolledEvent != null)
        {
            NativeControl.Scrolled += NativeControl_Scrolled;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Scrolled(object? sender, ScrolledEventArgs e)
    {
        var thisAsIScrollView = (IScrollView)this;
        if (_executingScrolledEvent == null || _executingScrolledEvent.IsCompleted)
        {
            _executingScrolledEvent = thisAsIScrollView.ScrolledEvent;
            _executingScrolledEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Scrolled -= NativeControl_Scrolled;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is ScrollView<T> @scrollview)
        {
            if (_executingScrolledEvent != null && !_executingScrolledEvent.IsCompleted)
            {
                @scrollview._executingScrolledEvent = _executingScrolledEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class ScrollView : ScrollView<Microsoft.Maui.Controls.ScrollView>
{
    public ScrollView(Action<Microsoft.Maui.Controls.ScrollView?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public ScrollView(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ScrollViewExtensions
{
    public static T Orientation<T>(this T scrollView, Microsoft.Maui.ScrollOrientation orientation)
        where T : IScrollView
    {
        //scrollView.Orientation = orientation;
        scrollView.SetProperty(Microsoft.Maui.Controls.ScrollView.OrientationProperty, orientation);
        return scrollView;
    }

    public static T Orientation<T>(this T scrollView, Func<Microsoft.Maui.ScrollOrientation> orientationFunc, IComponentWithState? componentWithState = null)
        where T : IScrollView
    {
        scrollView.SetProperty(Microsoft.Maui.Controls.ScrollView.OrientationProperty, new PropertyValue<Microsoft.Maui.ScrollOrientation>(orientationFunc, componentWithState));
        return scrollView;
    }

    public static T HorizontalScrollBarVisibility<T>(this T scrollView, Microsoft.Maui.ScrollBarVisibility horizontalScrollBarVisibility)
        where T : IScrollView
    {
        //scrollView.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
        scrollView.SetProperty(Microsoft.Maui.Controls.ScrollView.HorizontalScrollBarVisibilityProperty, horizontalScrollBarVisibility);
        return scrollView;
    }

    public static T HorizontalScrollBarVisibility<T>(this T scrollView, Func<Microsoft.Maui.ScrollBarVisibility> horizontalScrollBarVisibilityFunc, IComponentWithState? componentWithState = null)
        where T : IScrollView
    {
        scrollView.SetProperty(Microsoft.Maui.Controls.ScrollView.HorizontalScrollBarVisibilityProperty, new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(horizontalScrollBarVisibilityFunc, componentWithState));
        return scrollView;
    }

    public static T VerticalScrollBarVisibility<T>(this T scrollView, Microsoft.Maui.ScrollBarVisibility verticalScrollBarVisibility)
        where T : IScrollView
    {
        //scrollView.VerticalScrollBarVisibility = verticalScrollBarVisibility;
        scrollView.SetProperty(Microsoft.Maui.Controls.ScrollView.VerticalScrollBarVisibilityProperty, verticalScrollBarVisibility);
        return scrollView;
    }

    public static T VerticalScrollBarVisibility<T>(this T scrollView, Func<Microsoft.Maui.ScrollBarVisibility> verticalScrollBarVisibilityFunc, IComponentWithState? componentWithState = null)
        where T : IScrollView
    {
        scrollView.SetProperty(Microsoft.Maui.Controls.ScrollView.VerticalScrollBarVisibilityProperty, new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(verticalScrollBarVisibilityFunc, componentWithState));
        return scrollView;
    }

    public static T OnScrolled<T>(this T scrollView, Action? scrolledAction)
        where T : IScrollView
    {
        scrollView.ScrolledEvent = new SyncEventCommand<ScrolledEventArgs>(execute: scrolledAction);
        return scrollView;
    }

    public static T OnScrolled<T>(this T scrollView, Action<ScrolledEventArgs>? scrolledAction)
        where T : IScrollView
    {
        scrollView.ScrolledEvent = new SyncEventCommand<ScrolledEventArgs>(executeWithArgs: scrolledAction);
        return scrollView;
    }

    public static T OnScrolled<T>(this T scrollView, Action<object?, ScrolledEventArgs>? scrolledAction)
        where T : IScrollView
    {
        scrollView.ScrolledEvent = new SyncEventCommand<ScrolledEventArgs>(executeWithFullArgs: scrolledAction);
        return scrollView;
    }

    public static T OnScrolled<T>(this T scrollView, Func<Task>? scrolledAction, bool runInBackground = false)
        where T : IScrollView
    {
        scrollView.ScrolledEvent = new AsyncEventCommand<ScrolledEventArgs>(execute: scrolledAction, runInBackground);
        return scrollView;
    }

    public static T OnScrolled<T>(this T scrollView, Func<ScrolledEventArgs, Task>? scrolledAction, bool runInBackground = false)
        where T : IScrollView
    {
        scrollView.ScrolledEvent = new AsyncEventCommand<ScrolledEventArgs>(executeWithArgs: scrolledAction, runInBackground);
        return scrollView;
    }

    public static T OnScrolled<T>(this T scrollView, Func<object?, ScrolledEventArgs, Task>? scrolledAction, bool runInBackground = false)
        where T : IScrollView
    {
        scrollView.ScrolledEvent = new AsyncEventCommand<ScrolledEventArgs>(executeWithFullArgs: scrolledAction, runInBackground);
        return scrollView;
    }
}

public static partial class ScrollViewStyles
{
    public static Action<IScrollView>? Default { get; set; }
    public static Dictionary<string, Action<IScrollView>> Themes { get; } = [];
}