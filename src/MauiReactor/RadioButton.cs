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
public partial interface IRadioButton : ITemplatedView
{
    EventCommand<CheckedChangedEventArgs>? CheckedChangedEvent { get; set; }
}

public partial class RadioButton<T> : TemplatedView<T>, IRadioButton where T : Microsoft.Maui.Controls.RadioButton, new()
{
    public RadioButton()
    {
        RadioButtonStyles.Default?.Invoke(this);
    }

    public RadioButton(Action<T?> componentRefAction) : base(componentRefAction)
    {
        RadioButtonStyles.Default?.Invoke(this);
    }

    EventCommand<CheckedChangedEventArgs>? IRadioButton.CheckedChangedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && RadioButtonStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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
        var thisAsIRadioButton = (IRadioButton)this;
        if (thisAsIRadioButton.CheckedChangedEvent != null)
        {
            NativeControl.CheckedChanged += NativeControl_CheckedChanged;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_CheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        var thisAsIRadioButton = (IRadioButton)this;
        if (_executingCheckedChangedEvent == null || _executingCheckedChangedEvent.IsCompleted)
        {
            _executingCheckedChangedEvent = thisAsIRadioButton.CheckedChangedEvent;
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
        if (newNode is RadioButton<T> @radiobutton)
        {
            if (_executingCheckedChangedEvent != null && !_executingCheckedChangedEvent.IsCompleted)
            {
                @radiobutton._executingCheckedChangedEvent = _executingCheckedChangedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class RadioButton : RadioButton<Microsoft.Maui.Controls.RadioButton>
{
    public RadioButton()
    {
    }

    public RadioButton(Action<Microsoft.Maui.Controls.RadioButton?> componentRefAction) : base(componentRefAction)
    {
    }

    public RadioButton(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class RadioButtonExtensions
{
    /*
    
    
    
    
    
    
    
    
    
    
    static object? SetCharacterSpacing(object radioButton, RxAnimation animation)
        => ((IRadioButton)radioButton).CharacterSpacing = ((RxDoubleAnimation)animation).CurrentValue();

    
    
    
    
    
    
    
    
    
    static object? SetFontSize(object radioButton, RxAnimation animation)
        => ((IRadioButton)radioButton).FontSize = ((RxDoubleAnimation)animation).CurrentValue();

    
    
    
    
    
    
    
    
    
    static object? SetBorderWidth(object radioButton, RxAnimation animation)
        => ((IRadioButton)radioButton).BorderWidth = ((RxDoubleAnimation)animation).CurrentValue();

    
    */
    public static T Value<T>(this T radioButton, object? value)
        where T : IRadioButton
    {
        //radioButton.Value = value;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.ValueProperty, value);
        return radioButton;
    }

    public static T Value<T>(this T radioButton, Func<object?> valueFunc)
        where T : IRadioButton
    {
        //radioButton.Value = new PropertyValue<object?>(valueFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.ValueProperty, new PropertyValue<object?>(valueFunc));
        return radioButton;
    }

    public static T IsChecked<T>(this T radioButton, bool isChecked)
        where T : IRadioButton
    {
        //radioButton.IsChecked = isChecked;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.IsCheckedProperty, isChecked);
        return radioButton;
    }

    public static T IsChecked<T>(this T radioButton, Func<bool> isCheckedFunc)
        where T : IRadioButton
    {
        //radioButton.IsChecked = new PropertyValue<bool>(isCheckedFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.IsCheckedProperty, new PropertyValue<bool>(isCheckedFunc));
        return radioButton;
    }

    public static T GroupName<T>(this T radioButton, string groupName)
        where T : IRadioButton
    {
        //radioButton.GroupName = groupName;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.GroupNameProperty, groupName);
        return radioButton;
    }

    public static T GroupName<T>(this T radioButton, Func<string> groupNameFunc)
        where T : IRadioButton
    {
        //radioButton.GroupName = new PropertyValue<string>(groupNameFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.GroupNameProperty, new PropertyValue<string>(groupNameFunc));
        return radioButton;
    }

    public static T TextColor<T>(this T radioButton, Microsoft.Maui.Graphics.Color textColor)
        where T : IRadioButton
    {
        //radioButton.TextColor = textColor;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.TextColorProperty, textColor);
        return radioButton;
    }

    public static T TextColor<T>(this T radioButton, Func<Microsoft.Maui.Graphics.Color> textColorFunc)
        where T : IRadioButton
    {
        //radioButton.TextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(textColorFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.TextColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(textColorFunc));
        return radioButton;
    }

    public static T CharacterSpacing<T>(this T radioButton, double characterSpacing, RxDoubleAnimation? customAnimation = null)
        where T : IRadioButton
    {
        //radioButton.CharacterSpacing = characterSpacing;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.CharacterSpacingProperty, characterSpacing);
        radioButton.AppendAnimatable(Microsoft.Maui.Controls.RadioButton.CharacterSpacingProperty, customAnimation ?? new RxDoubleAnimation(characterSpacing));
        return radioButton;
    }

    public static T CharacterSpacing<T>(this T radioButton, Func<double> characterSpacingFunc)
        where T : IRadioButton
    {
        //radioButton.CharacterSpacing = new PropertyValue<double>(characterSpacingFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.CharacterSpacingProperty, new PropertyValue<double>(characterSpacingFunc));
        return radioButton;
    }

    public static T TextTransform<T>(this T radioButton, Microsoft.Maui.TextTransform textTransform)
        where T : IRadioButton
    {
        //radioButton.TextTransform = textTransform;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.TextTransformProperty, textTransform);
        return radioButton;
    }

    public static T TextTransform<T>(this T radioButton, Func<Microsoft.Maui.TextTransform> textTransformFunc)
        where T : IRadioButton
    {
        //radioButton.TextTransform = new PropertyValue<Microsoft.Maui.TextTransform>(textTransformFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.TextTransformProperty, new PropertyValue<Microsoft.Maui.TextTransform>(textTransformFunc));
        return radioButton;
    }

    public static T FontAttributes<T>(this T radioButton, Microsoft.Maui.Controls.FontAttributes fontAttributes)
        where T : IRadioButton
    {
        //radioButton.FontAttributes = fontAttributes;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontAttributesProperty, fontAttributes);
        return radioButton;
    }

    public static T FontAttributes<T>(this T radioButton, Func<Microsoft.Maui.Controls.FontAttributes> fontAttributesFunc)
        where T : IRadioButton
    {
        //radioButton.FontAttributes = new PropertyValue<Microsoft.Maui.Controls.FontAttributes>(fontAttributesFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontAttributesProperty, new PropertyValue<Microsoft.Maui.Controls.FontAttributes>(fontAttributesFunc));
        return radioButton;
    }

    public static T FontFamily<T>(this T radioButton, string fontFamily)
        where T : IRadioButton
    {
        //radioButton.FontFamily = fontFamily;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontFamilyProperty, fontFamily);
        return radioButton;
    }

    public static T FontFamily<T>(this T radioButton, Func<string> fontFamilyFunc)
        where T : IRadioButton
    {
        //radioButton.FontFamily = new PropertyValue<string>(fontFamilyFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontFamilyProperty, new PropertyValue<string>(fontFamilyFunc));
        return radioButton;
    }

    public static T FontSize<T>(this T radioButton, double fontSize, RxDoubleAnimation? customAnimation = null)
        where T : IRadioButton
    {
        //radioButton.FontSize = fontSize;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontSizeProperty, fontSize);
        radioButton.AppendAnimatable(Microsoft.Maui.Controls.RadioButton.FontSizeProperty, customAnimation ?? new RxDoubleAnimation(fontSize));
        return radioButton;
    }

    public static T FontSize<T>(this T radioButton, Func<double> fontSizeFunc)
        where T : IRadioButton
    {
        //radioButton.FontSize = new PropertyValue<double>(fontSizeFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontSizeProperty, new PropertyValue<double>(fontSizeFunc));
        return radioButton;
    }

    public static T FontAutoScalingEnabled<T>(this T radioButton, bool fontAutoScalingEnabled)
        where T : IRadioButton
    {
        //radioButton.FontAutoScalingEnabled = fontAutoScalingEnabled;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontAutoScalingEnabledProperty, fontAutoScalingEnabled);
        return radioButton;
    }

    public static T FontAutoScalingEnabled<T>(this T radioButton, Func<bool> fontAutoScalingEnabledFunc)
        where T : IRadioButton
    {
        //radioButton.FontAutoScalingEnabled = new PropertyValue<bool>(fontAutoScalingEnabledFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.FontAutoScalingEnabledProperty, new PropertyValue<bool>(fontAutoScalingEnabledFunc));
        return radioButton;
    }

    public static T BorderColor<T>(this T radioButton, Microsoft.Maui.Graphics.Color borderColor)
        where T : IRadioButton
    {
        //radioButton.BorderColor = borderColor;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.BorderColorProperty, borderColor);
        return radioButton;
    }

    public static T BorderColor<T>(this T radioButton, Func<Microsoft.Maui.Graphics.Color> borderColorFunc)
        where T : IRadioButton
    {
        //radioButton.BorderColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(borderColorFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.BorderColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(borderColorFunc));
        return radioButton;
    }

    public static T CornerRadius<T>(this T radioButton, int cornerRadius)
        where T : IRadioButton
    {
        //radioButton.CornerRadius = cornerRadius;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.CornerRadiusProperty, cornerRadius);
        return radioButton;
    }

    public static T CornerRadius<T>(this T radioButton, Func<int> cornerRadiusFunc)
        where T : IRadioButton
    {
        //radioButton.CornerRadius = new PropertyValue<int>(cornerRadiusFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.CornerRadiusProperty, new PropertyValue<int>(cornerRadiusFunc));
        return radioButton;
    }

    public static T BorderWidth<T>(this T radioButton, double borderWidth, RxDoubleAnimation? customAnimation = null)
        where T : IRadioButton
    {
        //radioButton.BorderWidth = borderWidth;
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.BorderWidthProperty, borderWidth);
        radioButton.AppendAnimatable(Microsoft.Maui.Controls.RadioButton.BorderWidthProperty, customAnimation ?? new RxDoubleAnimation(borderWidth));
        return radioButton;
    }

    public static T BorderWidth<T>(this T radioButton, Func<double> borderWidthFunc)
        where T : IRadioButton
    {
        //radioButton.BorderWidth = new PropertyValue<double>(borderWidthFunc);
        radioButton.SetProperty(Microsoft.Maui.Controls.RadioButton.BorderWidthProperty, new PropertyValue<double>(borderWidthFunc));
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Action? checkedChangedAction)
        where T : IRadioButton
    {
        radioButton.CheckedChangedEvent = new SyncEventCommand<CheckedChangedEventArgs>(execute: checkedChangedAction);
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Action<CheckedChangedEventArgs>? checkedChangedAction)
        where T : IRadioButton
    {
        radioButton.CheckedChangedEvent = new SyncEventCommand<CheckedChangedEventArgs>(executeWithArgs: checkedChangedAction);
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Action<object?, CheckedChangedEventArgs>? checkedChangedAction)
        where T : IRadioButton
    {
        radioButton.CheckedChangedEvent = new SyncEventCommand<CheckedChangedEventArgs>(executeWithFullArgs: checkedChangedAction);
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Func<Task>? checkedChangedAction)
        where T : IRadioButton
    {
        radioButton.CheckedChangedEvent = new AsyncEventCommand<CheckedChangedEventArgs>(execute: checkedChangedAction);
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Func<CheckedChangedEventArgs, Task>? checkedChangedAction)
        where T : IRadioButton
    {
        radioButton.CheckedChangedEvent = new AsyncEventCommand<CheckedChangedEventArgs>(executeWithArgs: checkedChangedAction);
        return radioButton;
    }

    public static T OnCheckedChanged<T>(this T radioButton, Func<object?, CheckedChangedEventArgs, Task>? checkedChangedAction)
        where T : IRadioButton
    {
        radioButton.CheckedChangedEvent = new AsyncEventCommand<CheckedChangedEventArgs>(executeWithFullArgs: checkedChangedAction);
        return radioButton;
    }
}

public static partial class RadioButtonStyles
{
    public static Action<IRadioButton>? Default { get; set; }
    public static Dictionary<string, Action<IRadioButton>> Themes { get; } = [];
}