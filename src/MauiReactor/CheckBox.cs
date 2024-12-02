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
public partial interface ICheckBox : IView
{
    EventCommand<CheckedChangedEventArgs>? CheckedChangedEvent { get; set; }
}

public partial class CheckBox<T> : View<T>, ICheckBox where T : Microsoft.Maui.Controls.CheckBox, new()
{
    public CheckBox()
    {
        CheckBoxStyles.Default?.Invoke(this);
    }

    public CheckBox(Action<T?> componentRefAction) : base(componentRefAction)
    {
        CheckBoxStyles.Default?.Invoke(this);
    }

    EventCommand<CheckedChangedEventArgs>? ICheckBox.CheckedChangedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && CheckBoxStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<CheckedChangedEventArgs>? _executingCheckedChangedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsICheckBox = (ICheckBox)this;
        if (thisAsICheckBox.CheckedChangedEvent != null)
        {
            NativeControl.CheckedChanged += NativeControl_CheckedChanged;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_CheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        var thisAsICheckBox = (ICheckBox)this;
        if (_executingCheckedChangedEvent == null || _executingCheckedChangedEvent.IsCompleted)
        {
            _executingCheckedChangedEvent = thisAsICheckBox.CheckedChangedEvent;
            _executingCheckedChangedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.CheckedChanged -= NativeControl_CheckedChanged;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is CheckBox<T> @checkbox)
        {
            if (_executingCheckedChangedEvent != null && !_executingCheckedChangedEvent.IsCompleted)
            {
                @checkbox._executingCheckedChangedEvent = _executingCheckedChangedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class CheckBox : CheckBox<Microsoft.Maui.Controls.CheckBox>
{
    public CheckBox()
    {
    }

    public CheckBox(Action<Microsoft.Maui.Controls.CheckBox?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class CheckBoxExtensions
{
    /*
    
    
    
    
    */
    public static T IsChecked<T>(this T checkBox, bool isChecked)
        where T : ICheckBox
    {
        //checkBox.IsChecked = isChecked;
        checkBox.SetProperty(Microsoft.Maui.Controls.CheckBox.IsCheckedProperty, isChecked);
        return checkBox;
    }

    public static T IsChecked<T>(this T checkBox, Func<bool> isCheckedFunc)
        where T : ICheckBox
    {
        //checkBox.IsChecked = new PropertyValue<bool>(isCheckedFunc);
        checkBox.SetProperty(Microsoft.Maui.Controls.CheckBox.IsCheckedProperty, new PropertyValue<bool>(isCheckedFunc));
        return checkBox;
    }

    public static T Color<T>(this T checkBox, Microsoft.Maui.Graphics.Color color)
        where T : ICheckBox
    {
        //checkBox.Color = color;
        checkBox.SetProperty(Microsoft.Maui.Controls.CheckBox.ColorProperty, color);
        return checkBox;
    }

    public static T Color<T>(this T checkBox, Func<Microsoft.Maui.Graphics.Color> colorFunc)
        where T : ICheckBox
    {
        //checkBox.Color = new PropertyValue<Microsoft.Maui.Graphics.Color>(colorFunc);
        checkBox.SetProperty(Microsoft.Maui.Controls.CheckBox.ColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(colorFunc));
        return checkBox;
    }

    public static T OnCheckedChanged<T>(this T checkBox, Action? checkedChangedAction)
        where T : ICheckBox
    {
        checkBox.CheckedChangedEvent = new SyncEventCommand<CheckedChangedEventArgs>(execute: checkedChangedAction);
        return checkBox;
    }

    public static T OnCheckedChanged<T>(this T checkBox, Action<CheckedChangedEventArgs>? checkedChangedAction)
        where T : ICheckBox
    {
        checkBox.CheckedChangedEvent = new SyncEventCommand<CheckedChangedEventArgs>(executeWithArgs: checkedChangedAction);
        return checkBox;
    }

    public static T OnCheckedChanged<T>(this T checkBox, Action<object?, CheckedChangedEventArgs>? checkedChangedAction)
        where T : ICheckBox
    {
        checkBox.CheckedChangedEvent = new SyncEventCommand<CheckedChangedEventArgs>(executeWithFullArgs: checkedChangedAction);
        return checkBox;
    }

    public static T OnCheckedChanged<T>(this T checkBox, Func<Task>? checkedChangedAction)
        where T : ICheckBox
    {
        checkBox.CheckedChangedEvent = new AsyncEventCommand<CheckedChangedEventArgs>(execute: checkedChangedAction);
        return checkBox;
    }

    public static T OnCheckedChanged<T>(this T checkBox, Func<CheckedChangedEventArgs, Task>? checkedChangedAction)
        where T : ICheckBox
    {
        checkBox.CheckedChangedEvent = new AsyncEventCommand<CheckedChangedEventArgs>(executeWithArgs: checkedChangedAction);
        return checkBox;
    }

    public static T OnCheckedChanged<T>(this T checkBox, Func<object?, CheckedChangedEventArgs, Task>? checkedChangedAction)
        where T : ICheckBox
    {
        checkBox.CheckedChangedEvent = new AsyncEventCommand<CheckedChangedEventArgs>(executeWithFullArgs: checkedChangedAction);
        return checkBox;
    }
}

public static partial class CheckBoxStyles
{
    public static Action<ICheckBox>? Default { get; set; }
    public static Dictionary<string, Action<ICheckBox>> Themes { get; } = [];
}