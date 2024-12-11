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
public partial interface ISwitchCell : ICell
{
    EventCommand<ToggledEventArgs>? OnChangedEvent { get; set; }
}

public partial class SwitchCell<T> : Cell<T>, ISwitchCell where T : Microsoft.Maui.Controls.SwitchCell, new()
{
    public SwitchCell()
    {
        SwitchCellStyles.Default?.Invoke(this);
    }

    public SwitchCell(Action<T?> componentRefAction) : base(componentRefAction)
    {
        SwitchCellStyles.Default?.Invoke(this);
    }

    EventCommand<ToggledEventArgs>? ISwitchCell.OnChangedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && SwitchCellStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<ToggledEventArgs>? _executingOnChangedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISwitchCell = (ISwitchCell)this;
        if (thisAsISwitchCell.OnChangedEvent != null)
        {
            NativeControl.OnChanged += NativeControl_OnChanged;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_OnChanged(object? sender, ToggledEventArgs e)
    {
        var thisAsISwitchCell = (ISwitchCell)this;
        if (_executingOnChangedEvent == null || _executingOnChangedEvent.IsCompleted)
        {
            _executingOnChangedEvent = thisAsISwitchCell.OnChangedEvent;
            _executingOnChangedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.OnChanged -= NativeControl_OnChanged;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is SwitchCell<T> @switchcell)
        {
            if (_executingOnChangedEvent != null && !_executingOnChangedEvent.IsCompleted)
            {
                @switchcell._executingOnChangedEvent = _executingOnChangedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class SwitchCell : SwitchCell<Microsoft.Maui.Controls.SwitchCell>
{
    public SwitchCell()
    {
    }

    public SwitchCell(Action<Microsoft.Maui.Controls.SwitchCell?> componentRefAction) : base(componentRefAction)
    {
    }

    public SwitchCell(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class SwitchCellExtensions
{
    /*
    
    
    
    
    
    
    */
    public static T On<T>(this T switchCell, bool on)
        where T : ISwitchCell
    {
        //switchCell.On = on;
        switchCell.SetProperty(Microsoft.Maui.Controls.SwitchCell.OnProperty, on);
        return switchCell;
    }

    public static T On<T>(this T switchCell, Func<bool> onFunc)
        where T : ISwitchCell
    {
        //switchCell.On = new PropertyValue<bool>(onFunc);
        switchCell.SetProperty(Microsoft.Maui.Controls.SwitchCell.OnProperty, new PropertyValue<bool>(onFunc));
        return switchCell;
    }

    public static T Text<T>(this T switchCell, string text)
        where T : ISwitchCell
    {
        //switchCell.Text = text;
        switchCell.SetProperty(Microsoft.Maui.Controls.SwitchCell.TextProperty, text);
        return switchCell;
    }

    public static T Text<T>(this T switchCell, Func<string> textFunc)
        where T : ISwitchCell
    {
        //switchCell.Text = new PropertyValue<string>(textFunc);
        switchCell.SetProperty(Microsoft.Maui.Controls.SwitchCell.TextProperty, new PropertyValue<string>(textFunc));
        return switchCell;
    }

    public static T OnColor<T>(this T switchCell, Microsoft.Maui.Graphics.Color onColor)
        where T : ISwitchCell
    {
        //switchCell.OnColor = onColor;
        switchCell.SetProperty(Microsoft.Maui.Controls.SwitchCell.OnColorProperty, onColor);
        return switchCell;
    }

    public static T OnColor<T>(this T switchCell, Func<Microsoft.Maui.Graphics.Color> onColorFunc)
        where T : ISwitchCell
    {
        //switchCell.OnColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(onColorFunc);
        switchCell.SetProperty(Microsoft.Maui.Controls.SwitchCell.OnColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(onColorFunc));
        return switchCell;
    }

    public static T OnOnChanged<T>(this T switchCell, Action? onChangedAction)
        where T : ISwitchCell
    {
        switchCell.OnChangedEvent = new SyncEventCommand<ToggledEventArgs>(execute: onChangedAction);
        return switchCell;
    }

    public static T OnOnChanged<T>(this T switchCell, Action<ToggledEventArgs>? onChangedAction)
        where T : ISwitchCell
    {
        switchCell.OnChangedEvent = new SyncEventCommand<ToggledEventArgs>(executeWithArgs: onChangedAction);
        return switchCell;
    }

    public static T OnOnChanged<T>(this T switchCell, Action<object?, ToggledEventArgs>? onChangedAction)
        where T : ISwitchCell
    {
        switchCell.OnChangedEvent = new SyncEventCommand<ToggledEventArgs>(executeWithFullArgs: onChangedAction);
        return switchCell;
    }

    public static T OnOnChanged<T>(this T switchCell, Func<Task>? onChangedAction)
        where T : ISwitchCell
    {
        switchCell.OnChangedEvent = new AsyncEventCommand<ToggledEventArgs>(execute: onChangedAction);
        return switchCell;
    }

    public static T OnOnChanged<T>(this T switchCell, Func<ToggledEventArgs, Task>? onChangedAction)
        where T : ISwitchCell
    {
        switchCell.OnChangedEvent = new AsyncEventCommand<ToggledEventArgs>(executeWithArgs: onChangedAction);
        return switchCell;
    }

    public static T OnOnChanged<T>(this T switchCell, Func<object?, ToggledEventArgs, Task>? onChangedAction)
        where T : ISwitchCell
    {
        switchCell.OnChangedEvent = new AsyncEventCommand<ToggledEventArgs>(executeWithFullArgs: onChangedAction);
        return switchCell;
    }
}

public static partial class SwitchCellStyles
{
    public static Action<ISwitchCell>? Default { get; set; }
    public static Dictionary<string, Action<ISwitchCell>> Themes { get; } = [];
}