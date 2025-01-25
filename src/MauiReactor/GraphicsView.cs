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
public partial interface IGraphicsView : IView
{
    EventCommand<TouchEventArgs>? StartHoverInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? MoveHoverInteractionEvent { get; set; }

    EventCommand<EventArgs>? EndHoverInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? StartInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? DragInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? EndInteractionEvent { get; set; }

    EventCommand<EventArgs>? CancelInteractionEvent { get; set; }
}

public partial class GraphicsView<T> : View<T>, IGraphicsView where T : Microsoft.Maui.Controls.GraphicsView, new()
{
    public GraphicsView(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        GraphicsViewStyles.Default?.Invoke(this);
    }

    EventCommand<TouchEventArgs>? IGraphicsView.StartHoverInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? IGraphicsView.MoveHoverInteractionEvent { get; set; }

    EventCommand<EventArgs>? IGraphicsView.EndHoverInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? IGraphicsView.StartInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? IGraphicsView.DragInteractionEvent { get; set; }

    EventCommand<TouchEventArgs>? IGraphicsView.EndInteractionEvent { get; set; }

    EventCommand<EventArgs>? IGraphicsView.CancelInteractionEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && GraphicsViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<TouchEventArgs>? _executingStartHoverInteractionEvent;
    private EventCommand<TouchEventArgs>? _executingMoveHoverInteractionEvent;
    private EventCommand<EventArgs>? _executingEndHoverInteractionEvent;
    private EventCommand<TouchEventArgs>? _executingStartInteractionEvent;
    private EventCommand<TouchEventArgs>? _executingDragInteractionEvent;
    private EventCommand<TouchEventArgs>? _executingEndInteractionEvent;
    private EventCommand<EventArgs>? _executingCancelInteractionEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (thisAsIGraphicsView.StartHoverInteractionEvent != null)
        {
            NativeControl.StartHoverInteraction += NativeControl_StartHoverInteraction;
        }

        if (thisAsIGraphicsView.MoveHoverInteractionEvent != null)
        {
            NativeControl.MoveHoverInteraction += NativeControl_MoveHoverInteraction;
        }

        if (thisAsIGraphicsView.EndHoverInteractionEvent != null)
        {
            NativeControl.EndHoverInteraction += NativeControl_EndHoverInteraction;
        }

        if (thisAsIGraphicsView.StartInteractionEvent != null)
        {
            NativeControl.StartInteraction += NativeControl_StartInteraction;
        }

        if (thisAsIGraphicsView.DragInteractionEvent != null)
        {
            NativeControl.DragInteraction += NativeControl_DragInteraction;
        }

        if (thisAsIGraphicsView.EndInteractionEvent != null)
        {
            NativeControl.EndInteraction += NativeControl_EndInteraction;
        }

        if (thisAsIGraphicsView.CancelInteractionEvent != null)
        {
            NativeControl.CancelInteraction += NativeControl_CancelInteraction;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_StartHoverInteraction(object? sender, TouchEventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingStartHoverInteractionEvent == null || _executingStartHoverInteractionEvent.IsCompleted)
        {
            _executingStartHoverInteractionEvent = thisAsIGraphicsView.StartHoverInteractionEvent;
            _executingStartHoverInteractionEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_MoveHoverInteraction(object? sender, TouchEventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingMoveHoverInteractionEvent == null || _executingMoveHoverInteractionEvent.IsCompleted)
        {
            _executingMoveHoverInteractionEvent = thisAsIGraphicsView.MoveHoverInteractionEvent;
            _executingMoveHoverInteractionEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_EndHoverInteraction(object? sender, EventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingEndHoverInteractionEvent == null || _executingEndHoverInteractionEvent.IsCompleted)
        {
            _executingEndHoverInteractionEvent = thisAsIGraphicsView.EndHoverInteractionEvent;
            _executingEndHoverInteractionEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_StartInteraction(object? sender, TouchEventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingStartInteractionEvent == null || _executingStartInteractionEvent.IsCompleted)
        {
            _executingStartInteractionEvent = thisAsIGraphicsView.StartInteractionEvent;
            _executingStartInteractionEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_DragInteraction(object? sender, TouchEventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingDragInteractionEvent == null || _executingDragInteractionEvent.IsCompleted)
        {
            _executingDragInteractionEvent = thisAsIGraphicsView.DragInteractionEvent;
            _executingDragInteractionEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_EndInteraction(object? sender, TouchEventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingEndInteractionEvent == null || _executingEndInteractionEvent.IsCompleted)
        {
            _executingEndInteractionEvent = thisAsIGraphicsView.EndInteractionEvent;
            _executingEndInteractionEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_CancelInteraction(object? sender, EventArgs e)
    {
        var thisAsIGraphicsView = (IGraphicsView)this;
        if (_executingCancelInteractionEvent == null || _executingCancelInteractionEvent.IsCompleted)
        {
            _executingCancelInteractionEvent = thisAsIGraphicsView.CancelInteractionEvent;
            _executingCancelInteractionEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.StartHoverInteraction -= NativeControl_StartHoverInteraction;
            NativeControl.MoveHoverInteraction -= NativeControl_MoveHoverInteraction;
            NativeControl.EndHoverInteraction -= NativeControl_EndHoverInteraction;
            NativeControl.StartInteraction -= NativeControl_StartInteraction;
            NativeControl.DragInteraction -= NativeControl_DragInteraction;
            NativeControl.EndInteraction -= NativeControl_EndInteraction;
            NativeControl.CancelInteraction -= NativeControl_CancelInteraction;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is GraphicsView<T> @graphicsview)
        {
            if (_executingStartHoverInteractionEvent != null && !_executingStartHoverInteractionEvent.IsCompleted)
            {
                @graphicsview._executingStartHoverInteractionEvent = _executingStartHoverInteractionEvent;
            }

            if (_executingMoveHoverInteractionEvent != null && !_executingMoveHoverInteractionEvent.IsCompleted)
            {
                @graphicsview._executingMoveHoverInteractionEvent = _executingMoveHoverInteractionEvent;
            }

            if (_executingEndHoverInteractionEvent != null && !_executingEndHoverInteractionEvent.IsCompleted)
            {
                @graphicsview._executingEndHoverInteractionEvent = _executingEndHoverInteractionEvent;
            }

            if (_executingStartInteractionEvent != null && !_executingStartInteractionEvent.IsCompleted)
            {
                @graphicsview._executingStartInteractionEvent = _executingStartInteractionEvent;
            }

            if (_executingDragInteractionEvent != null && !_executingDragInteractionEvent.IsCompleted)
            {
                @graphicsview._executingDragInteractionEvent = _executingDragInteractionEvent;
            }

            if (_executingEndInteractionEvent != null && !_executingEndInteractionEvent.IsCompleted)
            {
                @graphicsview._executingEndInteractionEvent = _executingEndInteractionEvent;
            }

            if (_executingCancelInteractionEvent != null && !_executingCancelInteractionEvent.IsCompleted)
            {
                @graphicsview._executingCancelInteractionEvent = _executingCancelInteractionEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class GraphicsView : GraphicsView<Microsoft.Maui.Controls.GraphicsView>
{
    public GraphicsView(Action<Microsoft.Maui.Controls.GraphicsView?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public GraphicsView(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class GraphicsViewExtensions
{
    public static T Drawable<T>(this T graphicsView, Microsoft.Maui.Graphics.IDrawable drawable)
        where T : IGraphicsView
    {
        //graphicsView.Drawable = drawable;
        graphicsView.SetProperty(Microsoft.Maui.Controls.GraphicsView.DrawableProperty, drawable);
        return graphicsView;
    }

    public static T Drawable<T>(this T graphicsView, Func<Microsoft.Maui.Graphics.IDrawable> drawableFunc)
        where T : IGraphicsView
    {
        //graphicsView.Drawable = new PropertyValue<Microsoft.Maui.Graphics.IDrawable>(drawableFunc);
        graphicsView.SetProperty(Microsoft.Maui.Controls.GraphicsView.DrawableProperty, new PropertyValue<Microsoft.Maui.Graphics.IDrawable>(drawableFunc));
        return graphicsView;
    }

    public static T OnStartHoverInteraction<T>(this T graphicsView, Action? startHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.StartHoverInteractionEvent = new SyncEventCommand<TouchEventArgs>(execute: startHoverInteractionAction);
        return graphicsView;
    }

    public static T OnStartHoverInteraction<T>(this T graphicsView, Action<TouchEventArgs>? startHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.StartHoverInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithArgs: startHoverInteractionAction);
        return graphicsView;
    }

    public static T OnStartHoverInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? startHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.StartHoverInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithFullArgs: startHoverInteractionAction);
        return graphicsView;
    }

    public static T OnStartHoverInteraction<T>(this T graphicsView, Func<Task>? startHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.StartHoverInteractionEvent = new AsyncEventCommand<TouchEventArgs>(execute: startHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnStartHoverInteraction<T>(this T graphicsView, Func<TouchEventArgs, Task>? startHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.StartHoverInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithArgs: startHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnStartHoverInteraction<T>(this T graphicsView, Func<object?, TouchEventArgs, Task>? startHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.StartHoverInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithFullArgs: startHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnMoveHoverInteraction<T>(this T graphicsView, Action? moveHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.MoveHoverInteractionEvent = new SyncEventCommand<TouchEventArgs>(execute: moveHoverInteractionAction);
        return graphicsView;
    }

    public static T OnMoveHoverInteraction<T>(this T graphicsView, Action<TouchEventArgs>? moveHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.MoveHoverInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithArgs: moveHoverInteractionAction);
        return graphicsView;
    }

    public static T OnMoveHoverInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? moveHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.MoveHoverInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithFullArgs: moveHoverInteractionAction);
        return graphicsView;
    }

    public static T OnMoveHoverInteraction<T>(this T graphicsView, Func<Task>? moveHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.MoveHoverInteractionEvent = new AsyncEventCommand<TouchEventArgs>(execute: moveHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnMoveHoverInteraction<T>(this T graphicsView, Func<TouchEventArgs, Task>? moveHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.MoveHoverInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithArgs: moveHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnMoveHoverInteraction<T>(this T graphicsView, Func<object?, TouchEventArgs, Task>? moveHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.MoveHoverInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithFullArgs: moveHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnEndHoverInteraction<T>(this T graphicsView, Action? endHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.EndHoverInteractionEvent = new SyncEventCommand<EventArgs>(execute: endHoverInteractionAction);
        return graphicsView;
    }

    public static T OnEndHoverInteraction<T>(this T graphicsView, Action<EventArgs>? endHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.EndHoverInteractionEvent = new SyncEventCommand<EventArgs>(executeWithArgs: endHoverInteractionAction);
        return graphicsView;
    }

    public static T OnEndHoverInteraction<T>(this T graphicsView, Action<object?, EventArgs>? endHoverInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.EndHoverInteractionEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: endHoverInteractionAction);
        return graphicsView;
    }

    public static T OnEndHoverInteraction<T>(this T graphicsView, Func<Task>? endHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.EndHoverInteractionEvent = new AsyncEventCommand<EventArgs>(execute: endHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnEndHoverInteraction<T>(this T graphicsView, Func<EventArgs, Task>? endHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.EndHoverInteractionEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: endHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnEndHoverInteraction<T>(this T graphicsView, Func<object?, EventArgs, Task>? endHoverInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.EndHoverInteractionEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: endHoverInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnStartInteraction<T>(this T graphicsView, Action? startInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.StartInteractionEvent = new SyncEventCommand<TouchEventArgs>(execute: startInteractionAction);
        return graphicsView;
    }

    public static T OnStartInteraction<T>(this T graphicsView, Action<TouchEventArgs>? startInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.StartInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithArgs: startInteractionAction);
        return graphicsView;
    }

    public static T OnStartInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? startInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.StartInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithFullArgs: startInteractionAction);
        return graphicsView;
    }

    public static T OnStartInteraction<T>(this T graphicsView, Func<Task>? startInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.StartInteractionEvent = new AsyncEventCommand<TouchEventArgs>(execute: startInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnStartInteraction<T>(this T graphicsView, Func<TouchEventArgs, Task>? startInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.StartInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithArgs: startInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnStartInteraction<T>(this T graphicsView, Func<object?, TouchEventArgs, Task>? startInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.StartInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithFullArgs: startInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnDragInteraction<T>(this T graphicsView, Action? dragInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.DragInteractionEvent = new SyncEventCommand<TouchEventArgs>(execute: dragInteractionAction);
        return graphicsView;
    }

    public static T OnDragInteraction<T>(this T graphicsView, Action<TouchEventArgs>? dragInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.DragInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithArgs: dragInteractionAction);
        return graphicsView;
    }

    public static T OnDragInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? dragInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.DragInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithFullArgs: dragInteractionAction);
        return graphicsView;
    }

    public static T OnDragInteraction<T>(this T graphicsView, Func<Task>? dragInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.DragInteractionEvent = new AsyncEventCommand<TouchEventArgs>(execute: dragInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnDragInteraction<T>(this T graphicsView, Func<TouchEventArgs, Task>? dragInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.DragInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithArgs: dragInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnDragInteraction<T>(this T graphicsView, Func<object?, TouchEventArgs, Task>? dragInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.DragInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithFullArgs: dragInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnEndInteraction<T>(this T graphicsView, Action? endInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.EndInteractionEvent = new SyncEventCommand<TouchEventArgs>(execute: endInteractionAction);
        return graphicsView;
    }

    public static T OnEndInteraction<T>(this T graphicsView, Action<TouchEventArgs>? endInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.EndInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithArgs: endInteractionAction);
        return graphicsView;
    }

    public static T OnEndInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? endInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.EndInteractionEvent = new SyncEventCommand<TouchEventArgs>(executeWithFullArgs: endInteractionAction);
        return graphicsView;
    }

    public static T OnEndInteraction<T>(this T graphicsView, Func<Task>? endInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.EndInteractionEvent = new AsyncEventCommand<TouchEventArgs>(execute: endInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnEndInteraction<T>(this T graphicsView, Func<TouchEventArgs, Task>? endInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.EndInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithArgs: endInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnEndInteraction<T>(this T graphicsView, Func<object?, TouchEventArgs, Task>? endInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.EndInteractionEvent = new AsyncEventCommand<TouchEventArgs>(executeWithFullArgs: endInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnCancelInteraction<T>(this T graphicsView, Action? cancelInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.CancelInteractionEvent = new SyncEventCommand<EventArgs>(execute: cancelInteractionAction);
        return graphicsView;
    }

    public static T OnCancelInteraction<T>(this T graphicsView, Action<EventArgs>? cancelInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.CancelInteractionEvent = new SyncEventCommand<EventArgs>(executeWithArgs: cancelInteractionAction);
        return graphicsView;
    }

    public static T OnCancelInteraction<T>(this T graphicsView, Action<object?, EventArgs>? cancelInteractionAction)
        where T : IGraphicsView
    {
        graphicsView.CancelInteractionEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: cancelInteractionAction);
        return graphicsView;
    }

    public static T OnCancelInteraction<T>(this T graphicsView, Func<Task>? cancelInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.CancelInteractionEvent = new AsyncEventCommand<EventArgs>(execute: cancelInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnCancelInteraction<T>(this T graphicsView, Func<EventArgs, Task>? cancelInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.CancelInteractionEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: cancelInteractionAction, runInBackground);
        return graphicsView;
    }

    public static T OnCancelInteraction<T>(this T graphicsView, Func<object?, EventArgs, Task>? cancelInteractionAction, bool runInBackground = false)
        where T : IGraphicsView
    {
        graphicsView.CancelInteractionEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: cancelInteractionAction, runInBackground);
        return graphicsView;
    }
}

public static partial class GraphicsViewStyles
{
    public static Action<IGraphicsView>? Default { get; set; }
    public static Dictionary<string, Action<IGraphicsView>> Themes { get; } = [];
}