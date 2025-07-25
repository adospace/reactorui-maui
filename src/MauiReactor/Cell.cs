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
public partial interface ICell : IElement
{
    EventCommand<EventArgs>? AppearingEvent { get; set; }

    EventCommand<EventArgs>? DisappearingEvent { get; set; }

    EventCommand<EventArgs>? TappedEvent { get; set; }
}

public abstract partial class Cell<T> : Element<T>, ICell where T : Microsoft.Maui.Controls.Cell, new()
{
    protected Cell(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        CellStyles.Default?.Invoke(this);
    }

    EventCommand<EventArgs>? ICell.AppearingEvent { get; set; }

    EventCommand<EventArgs>? ICell.DisappearingEvent { get; set; }

    EventCommand<EventArgs>? ICell.TappedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && CellStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<EventArgs>? _executingAppearingEvent;
    private EventCommand<EventArgs>? _executingDisappearingEvent;
    private EventCommand<EventArgs>? _executingTappedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsICell = (ICell)this;
        if (thisAsICell.AppearingEvent != null)
        {
            NativeControl.Appearing += NativeControl_Appearing;
        }

        if (thisAsICell.DisappearingEvent != null)
        {
            NativeControl.Disappearing += NativeControl_Disappearing;
        }

        if (thisAsICell.TappedEvent != null)
        {
            NativeControl.Tapped += NativeControl_Tapped;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Appearing(object? sender, EventArgs e)
    {
        var thisAsICell = (ICell)this;
        if (_executingAppearingEvent == null || _executingAppearingEvent.IsCompleted)
        {
            _executingAppearingEvent = thisAsICell.AppearingEvent;
            _executingAppearingEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Disappearing(object? sender, EventArgs e)
    {
        var thisAsICell = (ICell)this;
        if (_executingDisappearingEvent == null || _executingDisappearingEvent.IsCompleted)
        {
            _executingDisappearingEvent = thisAsICell.DisappearingEvent;
            _executingDisappearingEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Tapped(object? sender, EventArgs e)
    {
        var thisAsICell = (ICell)this;
        if (_executingTappedEvent == null || _executingTappedEvent.IsCompleted)
        {
            _executingTappedEvent = thisAsICell.TappedEvent;
            _executingTappedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Appearing -= NativeControl_Appearing;
            NativeControl.Disappearing -= NativeControl_Disappearing;
            NativeControl.Tapped -= NativeControl_Tapped;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is Cell<T> @cell)
        {
            if (_executingAppearingEvent != null && !_executingAppearingEvent.IsCompleted)
            {
                @cell._executingAppearingEvent = _executingAppearingEvent;
            }

            if (_executingDisappearingEvent != null && !_executingDisappearingEvent.IsCompleted)
            {
                @cell._executingDisappearingEvent = _executingDisappearingEvent;
            }

            if (_executingTappedEvent != null && !_executingTappedEvent.IsCompleted)
            {
                @cell._executingTappedEvent = _executingTappedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public static partial class CellExtensions
{
    public static T IsEnabled<T>(this T cell, bool isEnabled)
        where T : ICell
    {
        //cell.IsEnabled = isEnabled;
        cell.SetProperty(Microsoft.Maui.Controls.Cell.IsEnabledProperty, isEnabled);
        return cell;
    }

    public static T IsEnabled<T>(this T cell, Func<bool> isEnabledFunc, IComponentWithState? componentWithState = null)
        where T : ICell
    {
        cell.SetProperty(Microsoft.Maui.Controls.Cell.IsEnabledProperty, new PropertyValue<bool>(isEnabledFunc, componentWithState));
        return cell;
    }

    public static T OnAppearing<T>(this T cell, Action? appearingAction)
        where T : ICell
    {
        cell.AppearingEvent = new SyncEventCommand<EventArgs>(execute: appearingAction);
        return cell;
    }

    public static T OnAppearing<T>(this T cell, Action<EventArgs>? appearingAction)
        where T : ICell
    {
        cell.AppearingEvent = new SyncEventCommand<EventArgs>(executeWithArgs: appearingAction);
        return cell;
    }

    public static T OnAppearing<T>(this T cell, Action<object?, EventArgs>? appearingAction)
        where T : ICell
    {
        cell.AppearingEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: appearingAction);
        return cell;
    }

    public static T OnAppearing<T>(this T cell, Func<Task>? appearingAction, bool runInBackground = false)
        where T : ICell
    {
        cell.AppearingEvent = new AsyncEventCommand<EventArgs>(execute: appearingAction, runInBackground);
        return cell;
    }

    public static T OnAppearing<T>(this T cell, Func<EventArgs, Task>? appearingAction, bool runInBackground = false)
        where T : ICell
    {
        cell.AppearingEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: appearingAction, runInBackground);
        return cell;
    }

    public static T OnAppearing<T>(this T cell, Func<object?, EventArgs, Task>? appearingAction, bool runInBackground = false)
        where T : ICell
    {
        cell.AppearingEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: appearingAction, runInBackground);
        return cell;
    }

    public static T OnDisappearing<T>(this T cell, Action? disappearingAction)
        where T : ICell
    {
        cell.DisappearingEvent = new SyncEventCommand<EventArgs>(execute: disappearingAction);
        return cell;
    }

    public static T OnDisappearing<T>(this T cell, Action<EventArgs>? disappearingAction)
        where T : ICell
    {
        cell.DisappearingEvent = new SyncEventCommand<EventArgs>(executeWithArgs: disappearingAction);
        return cell;
    }

    public static T OnDisappearing<T>(this T cell, Action<object?, EventArgs>? disappearingAction)
        where T : ICell
    {
        cell.DisappearingEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: disappearingAction);
        return cell;
    }

    public static T OnDisappearing<T>(this T cell, Func<Task>? disappearingAction, bool runInBackground = false)
        where T : ICell
    {
        cell.DisappearingEvent = new AsyncEventCommand<EventArgs>(execute: disappearingAction, runInBackground);
        return cell;
    }

    public static T OnDisappearing<T>(this T cell, Func<EventArgs, Task>? disappearingAction, bool runInBackground = false)
        where T : ICell
    {
        cell.DisappearingEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: disappearingAction, runInBackground);
        return cell;
    }

    public static T OnDisappearing<T>(this T cell, Func<object?, EventArgs, Task>? disappearingAction, bool runInBackground = false)
        where T : ICell
    {
        cell.DisappearingEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: disappearingAction, runInBackground);
        return cell;
    }

    public static T OnTapped<T>(this T cell, Action? tappedAction)
        where T : ICell
    {
        cell.TappedEvent = new SyncEventCommand<EventArgs>(execute: tappedAction);
        return cell;
    }

    public static T OnTapped<T>(this T cell, Action<EventArgs>? tappedAction)
        where T : ICell
    {
        cell.TappedEvent = new SyncEventCommand<EventArgs>(executeWithArgs: tappedAction);
        return cell;
    }

    public static T OnTapped<T>(this T cell, Action<object?, EventArgs>? tappedAction)
        where T : ICell
    {
        cell.TappedEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: tappedAction);
        return cell;
    }

    public static T OnTapped<T>(this T cell, Func<Task>? tappedAction, bool runInBackground = false)
        where T : ICell
    {
        cell.TappedEvent = new AsyncEventCommand<EventArgs>(execute: tappedAction, runInBackground);
        return cell;
    }

    public static T OnTapped<T>(this T cell, Func<EventArgs, Task>? tappedAction, bool runInBackground = false)
        where T : ICell
    {
        cell.TappedEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: tappedAction, runInBackground);
        return cell;
    }

    public static T OnTapped<T>(this T cell, Func<object?, EventArgs, Task>? tappedAction, bool runInBackground = false)
        where T : ICell
    {
        cell.TappedEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: tappedAction, runInBackground);
        return cell;
    }
}

public static partial class CellStyles
{
    public static Action<ICell>? Default { get; set; }
    public static Dictionary<string, Action<ICell>> Themes { get; } = [];
}