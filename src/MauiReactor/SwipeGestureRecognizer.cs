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
    PropertyValue<Microsoft.Maui.SwipeDirection>? Direction { get; set; }

    PropertyValue<uint>? Threshold { get; set; }

    Action? SwipedAction { get; set; }

    Action<object?, SwipedEventArgs>? SwipedActionWithArgs { get; set; }
}

public sealed partial class SwipeGestureRecognizer : GestureRecognizer<Microsoft.Maui.Controls.SwipeGestureRecognizer>, ISwipeGestureRecognizer
{
    public SwipeGestureRecognizer()
    {
    }

    public SwipeGestureRecognizer(Action<Microsoft.Maui.Controls.SwipeGestureRecognizer?> componentRefAction) : base(componentRefAction)
    {
    }

    PropertyValue<Microsoft.Maui.SwipeDirection>? ISwipeGestureRecognizer.Direction { get; set; }

    PropertyValue<uint>? ISwipeGestureRecognizer.Threshold { get; set; }

    Action? ISwipeGestureRecognizer.SwipedAction { get; set; }

    Action<object?, SwipedEventArgs>? ISwipeGestureRecognizer.SwipedActionWithArgs { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsISwipeGestureRecognizer = (ISwipeGestureRecognizer)this;
        thisAsISwipeGestureRecognizer.Direction = null;
        thisAsISwipeGestureRecognizer.Threshold = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwipeGestureRecognizer = (ISwipeGestureRecognizer)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.SwipeGestureRecognizer.DirectionProperty, thisAsISwipeGestureRecognizer.Direction);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.SwipeGestureRecognizer.ThresholdProperty, thisAsISwipeGestureRecognizer.Threshold);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwipeGestureRecognizer = (ISwipeGestureRecognizer)this;
        if (thisAsISwipeGestureRecognizer.SwipedAction != null || thisAsISwipeGestureRecognizer.SwipedActionWithArgs != null)
        {
            NativeControl.Swiped += NativeControl_Swiped;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Swiped(object? sender, SwipedEventArgs e)
    {
        var thisAsISwipeGestureRecognizer = (ISwipeGestureRecognizer)this;
        thisAsISwipeGestureRecognizer.SwipedAction?.Invoke();
        thisAsISwipeGestureRecognizer.SwipedActionWithArgs?.Invoke(sender, e);
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
}

public static partial class SwipeGestureRecognizerExtensions
{
    public static T Direction<T>(this T swipeGestureRecognizer, Microsoft.Maui.SwipeDirection direction)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.Direction = new PropertyValue<Microsoft.Maui.SwipeDirection>(direction);
        return swipeGestureRecognizer;
    }

    public static T Direction<T>(this T swipeGestureRecognizer, Func<Microsoft.Maui.SwipeDirection> directionFunc)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.Direction = new PropertyValue<Microsoft.Maui.SwipeDirection>(directionFunc);
        return swipeGestureRecognizer;
    }

    public static T Threshold<T>(this T swipeGestureRecognizer, uint threshold)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.Threshold = new PropertyValue<uint>(threshold);
        return swipeGestureRecognizer;
    }

    public static T Threshold<T>(this T swipeGestureRecognizer, Func<uint> thresholdFunc)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.Threshold = new PropertyValue<uint>(thresholdFunc);
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Action? swipedAction)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedAction = swipedAction;
        return swipeGestureRecognizer;
    }

    public static T OnSwiped<T>(this T swipeGestureRecognizer, Action<object?, SwipedEventArgs>? swipedActionWithArgs)
        where T : ISwipeGestureRecognizer
    {
        swipeGestureRecognizer.SwipedActionWithArgs = swipedActionWithArgs;
        return swipeGestureRecognizer;
    }
}