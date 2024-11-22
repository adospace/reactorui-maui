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
public partial interface ISwipeItemView : IContentView
{
    EventCommand<EventArgs>? InvokedEvent { get; set; }
}

public partial class SwipeItemView<T> : ContentView<T>, ISwipeItemView where T : Microsoft.Maui.Controls.SwipeItemView, new()
{
    public SwipeItemView()
    {
        SwipeItemViewStyles.Default?.Invoke(this);
    }

    public SwipeItemView(Action<T?> componentRefAction) : base(componentRefAction)
    {
        SwipeItemViewStyles.Default?.Invoke(this);
    }

    EventCommand<EventArgs>? ISwipeItemView.InvokedEvent { get; set; }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && SwipeItemViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<EventArgs>? _executingInvokedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwipeItemView = (ISwipeItemView)this;
        if (thisAsISwipeItemView.InvokedEvent != null)
        {
            NativeControl.Invoked += NativeControl_Invoked;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Invoked(object? sender, EventArgs e)
    {
        var thisAsISwipeItemView = (ISwipeItemView)this;
        if (_executingInvokedEvent == null || _executingInvokedEvent.IsCompleted)
        {
            _executingInvokedEvent = thisAsISwipeItemView.InvokedEvent;
            _executingInvokedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Invoked -= NativeControl_Invoked;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is SwipeItemView<T> @swipeitemview)
        {
            if (_executingInvokedEvent != null && !_executingInvokedEvent.IsCompleted)
            {
                @swipeitemview._executingInvokedEvent = _executingInvokedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class SwipeItemView : SwipeItemView<Microsoft.Maui.Controls.SwipeItemView>
{
    public SwipeItemView()
    {
    }

    public SwipeItemView(Action<Microsoft.Maui.Controls.SwipeItemView?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class SwipeItemViewExtensions
{
    public static T OnInvoked<T>(this T swipeItemView, Action? invokedAction)
        where T : ISwipeItemView
    {
        swipeItemView.InvokedEvent = new SyncEventCommand<EventArgs>(execute: invokedAction);
        return swipeItemView;
    }

    public static T OnInvoked<T>(this T swipeItemView, Action<EventArgs>? invokedAction)
        where T : ISwipeItemView
    {
        swipeItemView.InvokedEvent = new SyncEventCommand<EventArgs>(executeWithArgs: invokedAction);
        return swipeItemView;
    }

    public static T OnInvoked<T>(this T swipeItemView, Action<object?, EventArgs>? invokedAction)
        where T : ISwipeItemView
    {
        swipeItemView.InvokedEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: invokedAction);
        return swipeItemView;
    }

    public static T OnInvoked<T>(this T swipeItemView, Func<Task>? invokedAction)
        where T : ISwipeItemView
    {
        swipeItemView.InvokedEvent = new AsyncEventCommand<EventArgs>(execute: invokedAction);
        return swipeItemView;
    }

    public static T OnInvoked<T>(this T swipeItemView, Func<EventArgs, Task>? invokedAction)
        where T : ISwipeItemView
    {
        swipeItemView.InvokedEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: invokedAction);
        return swipeItemView;
    }

    public static T OnInvoked<T>(this T swipeItemView, Func<object?, EventArgs, Task>? invokedAction)
        where T : ISwipeItemView
    {
        swipeItemView.InvokedEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: invokedAction);
        return swipeItemView;
    }
}

public static partial class SwipeItemViewStyles
{
    public static Action<ISwipeItemView>? Default { get; set; }
    public static Dictionary<string, Action<ISwipeItemView>> Themes { get; } = [];
}