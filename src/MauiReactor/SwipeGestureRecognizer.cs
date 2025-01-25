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
public partial interface ISwipeGestureRecognizer : IGestureRecognizer
{
    EventCommand<SwipedEventArgs>? SwipedEvent { get; set; }
}

public sealed partial class SwipeGestureRecognizer : GestureRecognizer<Microsoft.Maui.Controls.SwipeGestureRecognizer>, ISwipeGestureRecognizer
{
    public SwipeGestureRecognizer(Action<Microsoft.Maui.Controls.SwipeGestureRecognizer?>? componentRefAction = null) : base(componentRefAction)
    {
        SwipeGestureRecognizerStyles.Default?.Invoke(this);
    }

    EventCommand<SwipedEventArgs>? ISwipeGestureRecognizer.SwipedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && SwipeGestureRecognizerStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<SwipedEventArgs>? _executingSwipedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwipeGestureRecognizer = (ISwipeGestureRecognizer)this;
        if (thisAsISwipeGestureRecognizer.SwipedEvent != null)
        {
            NativeControl.Swiped += NativeControl_Swiped;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Swiped(object? sender, SwipedEventArgs e)
    {
        var thisAsISwipeGestureRecognizer = (ISwipeGestureRecognizer)this;
        if (_executingSwipedEvent == null || _executingSwipedEvent.IsCompleted)
        {
            _executingSwipedEvent = thisAsISwipeGestureRecognizer.SwipedEvent;
            _executingSwipedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Swiped -= NativeControl_Swiped;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is SwipeGestureRecognizer @swipegesturerecognizer)
        {
            if (_executingSwipedEvent != null && !_executingSwipedEvent.IsCompleted)
            {
                @swipegesturerecognizer._executingSwipedEvent = _executingSwipedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public static partial class SwipeGestureRecognizerExtensions
{
    public static T Direction<T>(this T swipeGestureRecognizer, Microsoft.Maui.SwipeDirection direction)
        where T : ISwipeGestureRecognizer
    {
        //swipeGestureRecognizer.Direction = direction;
        swipeGestureRecognizer.SetProperty(Microsoft.Maui.Controls.SwipeGestureRecognizer.DirectionProperty, direction);
        return swipeGestureRecognizer;
    }

    public static T Direction<T>(this T swipeGestureRecognizer, Func<Microsoft.Maui.SwipeDirection> directionFunc)
        where T : ISwipeGestureRecognizer
    {
        //swipeGestureRecognizer.Direction = new PropertyValue<Microsoft.Maui.SwipeDirection>(directionFunc);
        swipeGestureRecognizer.SetProperty(Microsoft.Maui.Controls.SwipeGestureRecognizer.DirectionProperty, new PropertyValue<Microsoft.Maui.SwipeDirection>(directionFunc));
        return swipeGestureRecognizer;
    }

    public static T Threshold<T>(this T swipeGestureRecognizer, uint threshold)
        where T : ISwipeGestureRecognizer
    {
        //swipeGestureRecognizer.Threshold = threshold;
        swipeGestureRecognizer.SetProperty(Microsoft.Maui.Controls.SwipeGestureRecognizer.ThresholdProperty, threshold);
        return swipeGestureRecognizer;
    }

    public static T Threshold<T>(this T swipeGestureRecognizer, Func<uint> thresholdFunc)
        where T : ISwipeGestureRecognizer
    {
        //swipeGestureRecognizer.Threshold = new PropertyValue<uint>(thresholdFunc);
        swipeGestureRecognizer.SetProperty(Microsoft.Maui.Controls.SwipeGestureRecognizer.ThresholdProperty, new PropertyValue<uint>(thresholdFunc));
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Action? swipedAction)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedEvent = new SyncEventCommand<SwipedEventArgs>(execute: swipedAction);
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Action<SwipedEventArgs>? swipedAction)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedEvent = new SyncEventCommand<SwipedEventArgs>(executeWithArgs: swipedAction);
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Action<object?, SwipedEventArgs>? swipedAction)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedEvent = new SyncEventCommand<SwipedEventArgs>(executeWithFullArgs: swipedAction);
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Func<Task>? swipedAction, bool runInBackground = false)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedEvent = new AsyncEventCommand<SwipedEventArgs>(execute: swipedAction, runInBackground);
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Func<SwipedEventArgs, Task>? swipedAction, bool runInBackground = false)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedEvent = new AsyncEventCommand<SwipedEventArgs>(executeWithArgs: swipedAction, runInBackground);
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Func<object?, SwipedEventArgs, Task>? swipedAction, bool runInBackground = false)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedEvent = new AsyncEventCommand<SwipedEventArgs>(executeWithFullArgs: swipedAction, runInBackground);
        return swipeGestureRecognizer;
    }
}

public static partial class SwipeGestureRecognizerStyles
{
    public static Action<ISwipeGestureRecognizer>? Default { get; set; }
    public static Dictionary<string, Action<ISwipeGestureRecognizer>> Themes { get; } = [];
}