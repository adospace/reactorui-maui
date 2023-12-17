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
public partial interface IPicker : IView
{
    PropertyValue<Microsoft.Maui.Graphics.Color>? TextColor { get; set; }

    PropertyValue<double>? CharacterSpacing { get; set; }

    PropertyValue<string>? Title { get; set; }

    PropertyValue<Microsoft.Maui.Graphics.Color>? TitleColor { get; set; }

    PropertyValue<int>? SelectedIndex { get; set; }

    PropertyValue<string>? FontFamily { get; set; }

    PropertyValue<double>? FontSize { get; set; }

    PropertyValue<Microsoft.Maui.Controls.FontAttributes>? FontAttributes { get; set; }

    PropertyValue<bool>? FontAutoScalingEnabled { get; set; }

    PropertyValue<Microsoft.Maui.TextAlignment>? HorizontalTextAlignment { get; set; }

    PropertyValue<Microsoft.Maui.TextAlignment>? VerticalTextAlignment { get; set; }

    Action? SelectedIndexChangedAction { get; set; }

    Action<object?, EventArgs>? SelectedIndexChangedActionWithArgs { get; set; }
}

public partial class Picker<T> : View<T>, IPicker where T : Microsoft.Maui.Controls.Picker, new()
{
    public Picker()
    {
    }

    public Picker(Action<T?> componentRefAction) : base(componentRefAction)
    {
    }

    PropertyValue<Microsoft.Maui.Graphics.Color>? IPicker.TextColor { get; set; }

    PropertyValue<double>? IPicker.CharacterSpacing { get; set; }

    PropertyValue<string>? IPicker.Title { get; set; }

    PropertyValue<Microsoft.Maui.Graphics.Color>? IPicker.TitleColor { get; set; }

    PropertyValue<int>? IPicker.SelectedIndex { get; set; }

    PropertyValue<string>? IPicker.FontFamily { get; set; }

    PropertyValue<double>? IPicker.FontSize { get; set; }

    PropertyValue<Microsoft.Maui.Controls.FontAttributes>? IPicker.FontAttributes { get; set; }

    PropertyValue<bool>? IPicker.FontAutoScalingEnabled { get; set; }

    PropertyValue<Microsoft.Maui.TextAlignment>? IPicker.HorizontalTextAlignment { get; set; }

    PropertyValue<Microsoft.Maui.TextAlignment>? IPicker.VerticalTextAlignment { get; set; }

    Action? IPicker.SelectedIndexChangedAction { get; set; }

    Action<object?, EventArgs>? IPicker.SelectedIndexChangedActionWithArgs { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsIPicker = (IPicker)this;
        thisAsIPicker.TextColor = null;
        thisAsIPicker.CharacterSpacing = null;
        thisAsIPicker.Title = null;
        thisAsIPicker.TitleColor = null;
        thisAsIPicker.SelectedIndex = null;
        thisAsIPicker.FontFamily = null;
        thisAsIPicker.FontSize = null;
        thisAsIPicker.FontAttributes = null;
        thisAsIPicker.FontAutoScalingEnabled = null;
        thisAsIPicker.HorizontalTextAlignment = null;
        thisAsIPicker.VerticalTextAlignment = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIPicker = (IPicker)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.TextColorProperty, thisAsIPicker.TextColor);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.CharacterSpacingProperty, thisAsIPicker.CharacterSpacing);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.TitleProperty, thisAsIPicker.Title);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.TitleColorProperty, thisAsIPicker.TitleColor);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.SelectedIndexProperty, thisAsIPicker.SelectedIndex);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.FontFamilyProperty, thisAsIPicker.FontFamily);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.FontSizeProperty, thisAsIPicker.FontSize);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.FontAttributesProperty, thisAsIPicker.FontAttributes);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.FontAutoScalingEnabledProperty, thisAsIPicker.FontAutoScalingEnabled);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.HorizontalTextAlignmentProperty, thisAsIPicker.HorizontalTextAlignment);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Picker.VerticalTextAlignmentProperty, thisAsIPicker.VerticalTextAlignment);
        base.OnUpdate();
        OnEndUpdate();
    }

    protected override void OnAnimate()
    {
        OnBeginAnimate();
        var thisAsIPicker = (IPicker)this;
        AnimateProperty(Microsoft.Maui.Controls.Picker.CharacterSpacingProperty, thisAsIPicker.CharacterSpacing);
        AnimateProperty(Microsoft.Maui.Controls.Picker.FontSizeProperty, thisAsIPicker.FontSize);
        base.OnAnimate();
        OnEndAnimate();
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
        var thisAsIPicker = (IPicker)this;
        if (thisAsIPicker.SelectedIndexChangedAction != null || thisAsIPicker.SelectedIndexChangedActionWithArgs != null)
        {
            NativeControl.SelectedIndexChanged += NativeControl_SelectedIndexChanged;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var thisAsIPicker = (IPicker)this;
        thisAsIPicker.SelectedIndexChangedAction?.Invoke();
        thisAsIPicker.SelectedIndexChangedActionWithArgs?.Invoke(sender, e);
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.SelectedIndexChanged -= NativeControl_SelectedIndexChanged;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }
}

public partial class Picker : Picker<Microsoft.Maui.Controls.Picker>
{
    public Picker()
    {
    }

    public Picker(Action<Microsoft.Maui.Controls.Picker?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class PickerExtensions
{
    public static T TextColor<T>(this T picker, Microsoft.Maui.Graphics.Color textColor)
        where T : IPicker
    {
        picker.TextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(textColor);
        return picker;
    }

    public static T TextColor<T>(this T picker, Func<Microsoft.Maui.Graphics.Color> textColorFunc)
        where T : IPicker
    {
        picker.TextColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(textColorFunc);
        return picker;
    }

    public static T CharacterSpacing<T>(this T picker, double characterSpacing, RxDoubleAnimation? customAnimation = null)
        where T : IPicker
    {
        picker.CharacterSpacing = new PropertyValue<double>(characterSpacing);
        picker.AppendAnimatable(Microsoft.Maui.Controls.Picker.CharacterSpacingProperty, customAnimation ?? new RxDoubleAnimation(characterSpacing), v => picker.CharacterSpacing = new PropertyValue<double>(v.CurrentValue()));
        return picker;
    }

    public static T CharacterSpacing<T>(this T picker, Func<double> characterSpacingFunc)
        where T : IPicker
    {
        picker.CharacterSpacing = new PropertyValue<double>(characterSpacingFunc);
        return picker;
    }

    public static T Title<T>(this T picker, string title)
        where T : IPicker
    {
        picker.Title = new PropertyValue<string>(title);
        return picker;
    }

    public static T Title<T>(this T picker, Func<string> titleFunc)
        where T : IPicker
    {
        picker.Title = new PropertyValue<string>(titleFunc);
        return picker;
    }

    public static T TitleColor<T>(this T picker, Microsoft.Maui.Graphics.Color titleColor)
        where T : IPicker
    {
        picker.TitleColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(titleColor);
        return picker;
    }

    public static T TitleColor<T>(this T picker, Func<Microsoft.Maui.Graphics.Color> titleColorFunc)
        where T : IPicker
    {
        picker.TitleColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(titleColorFunc);
        return picker;
    }

    public static T SelectedIndex<T>(this T picker, int selectedIndex)
        where T : IPicker
    {
        picker.SelectedIndex = new PropertyValue<int>(selectedIndex);
        return picker;
    }

    public static T SelectedIndex<T>(this T picker, Func<int> selectedIndexFunc)
        where T : IPicker
    {
        picker.SelectedIndex = new PropertyValue<int>(selectedIndexFunc);
        return picker;
    }

    public static T FontFamily<T>(this T picker, string fontFamily)
        where T : IPicker
    {
        picker.FontFamily = new PropertyValue<string>(fontFamily);
        return picker;
    }

    public static T FontFamily<T>(this T picker, Func<string> fontFamilyFunc)
        where T : IPicker
    {
        picker.FontFamily = new PropertyValue<string>(fontFamilyFunc);
        return picker;
    }

    public static T FontSize<T>(this T picker, double fontSize, RxDoubleAnimation? customAnimation = null)
        where T : IPicker
    {
        picker.FontSize = new PropertyValue<double>(fontSize);
        picker.AppendAnimatable(Microsoft.Maui.Controls.Picker.FontSizeProperty, customAnimation ?? new RxDoubleAnimation(fontSize), v => picker.FontSize = new PropertyValue<double>(v.CurrentValue()));
        return picker;
    }

    public static T FontSize<T>(this T picker, Func<double> fontSizeFunc)
        where T : IPicker
    {
        picker.FontSize = new PropertyValue<double>(fontSizeFunc);
        return picker;
    }

    public static T FontAttributes<T>(this T picker, Microsoft.Maui.Controls.FontAttributes fontAttributes)
        where T : IPicker
    {
        picker.FontAttributes = new PropertyValue<Microsoft.Maui.Controls.FontAttributes>(fontAttributes);
        return picker;
    }

    public static T FontAttributes<T>(this T picker, Func<Microsoft.Maui.Controls.FontAttributes> fontAttributesFunc)
        where T : IPicker
    {
        picker.FontAttributes = new PropertyValue<Microsoft.Maui.Controls.FontAttributes>(fontAttributesFunc);
        return picker;
    }

    public static T FontAutoScalingEnabled<T>(this T picker, bool fontAutoScalingEnabled)
        where T : IPicker
    {
        picker.FontAutoScalingEnabled = new PropertyValue<bool>(fontAutoScalingEnabled);
        return picker;
    }

    public static T FontAutoScalingEnabled<T>(this T picker, Func<bool> fontAutoScalingEnabledFunc)
        where T : IPicker
    {
        picker.FontAutoScalingEnabled = new PropertyValue<bool>(fontAutoScalingEnabledFunc);
        return picker;
    }

    public static T HorizontalTextAlignment<T>(this T picker, Microsoft.Maui.TextAlignment horizontalTextAlignment)
        where T : IPicker
    {
        picker.HorizontalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(horizontalTextAlignment);
        return picker;
    }

    public static T HorizontalTextAlignment<T>(this T picker, Func<Microsoft.Maui.TextAlignment> horizontalTextAlignmentFunc)
        where T : IPicker
    {
        picker.HorizontalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(horizontalTextAlignmentFunc);
        return picker;
    }

    public static T VerticalTextAlignment<T>(this T picker, Microsoft.Maui.TextAlignment verticalTextAlignment)
        where T : IPicker
    {
        picker.VerticalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(verticalTextAlignment);
        return picker;
    }

    public static T VerticalTextAlignment<T>(this T picker, Func<Microsoft.Maui.TextAlignment> verticalTextAlignmentFunc)
        where T : IPicker
    {
        picker.VerticalTextAlignment = new PropertyValue<Microsoft.Maui.TextAlignment>(verticalTextAlignmentFunc);
        return picker;
    }

    public static T OnSelectedIndexChanged<T>(this T picker, Action? selectedIndexChangedAction)
        where T : IPicker
    {
        picker.SelectedIndexChangedAction = selectedIndexChangedAction;
        return picker;
    }

    public static T OnSelectedIndexChanged<T>(this T picker, Action<object?, EventArgs>? selectedIndexChangedActionWithArgs)
        where T : IPicker
    {
        picker.SelectedIndexChangedActionWithArgs = selectedIndexChangedActionWithArgs;
        return picker;
    }
}