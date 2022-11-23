using MauiReactor.Animations;
using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.Box))]
public partial class Box { }

//public partial interface IBox : ICanvasVisualElement
//{
//    PropertyValue<Color?>? BackgroundColor { get; set; }
//    PropertyValue<Paint?>? Background { get; set; }
//    PropertyValue<Color?>? BorderColor { get; set; }
//    PropertyValue<CornerRadiusF>? CornerRadius { get; set; }
//    PropertyValue<float>? BorderSize { get; set; }
//    PropertyValue<ThicknessF>? Padding { get; set; }
//}

//public partial class Box<T> : CanvasVisualElement<T>, IBox where T : Internals.Box, new()
//{
//    public Box()
//    {

//    }

//    public Box(Action<T?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }

//    PropertyValue<Color?>? IBox.BackgroundColor { get; set; }
//    PropertyValue<Paint?>? IBox.Background { get; set; }
//    PropertyValue<Color?>? IBox.BorderColor { get; set; }
//    PropertyValue<CornerRadiusF>? IBox.CornerRadius { get; set; }
//    PropertyValue<float>? IBox.BorderSize { get; set; }
//    PropertyValue<ThicknessF>? IBox.Padding { get; set; }

//    protected override void OnUpdate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIBorder = (IBox)this;

//        SetPropertyValue(NativeControl, Internals.Box.BackgroundColorProperty, thisAsIBorder.BackgroundColor);
//        SetPropertyValue(NativeControl, Internals.Box.BackgroundProperty, thisAsIBorder.Background);
//        SetPropertyValue(NativeControl, Internals.Box.BorderColorProperty, thisAsIBorder.BorderColor);
//        SetPropertyValue(NativeControl, Internals.Box.CornerRadiusProperty, thisAsIBorder.CornerRadius);
//        SetPropertyValue(NativeControl, Internals.Box.BorderSizeProperty, thisAsIBorder.BorderSize);
//        SetPropertyValue(NativeControl, Internals.Box.PaddingProperty, thisAsIBorder.Padding);

//        base.OnUpdate();
//    }

//    protected override void OnAnimate()
//    {
//        Validate.EnsureNotNull(NativeControl);
//        var thisAsIBorder = (IBox)this;

//        SetPropertyValue(NativeControl, Internals.Box.BackgroundColorProperty, thisAsIBorder.BackgroundColor);
//        SetPropertyValue(NativeControl, Internals.Box.BorderColorProperty, thisAsIBorder.BorderColor);
//        SetPropertyValue(NativeControl, Internals.Box.CornerRadiusProperty, thisAsIBorder.CornerRadius);
//        SetPropertyValue(NativeControl, Internals.Box.BorderSizeProperty, thisAsIBorder.BorderSize);
//        SetPropertyValue(NativeControl, Internals.Box.PaddingProperty, thisAsIBorder.Padding);

//        base.OnAnimate();
//    }
//}

//public partial class Box : Box<Internals.Box>
//{
//    public Box()
//    {

//    }

//    public Box(Action<Internals.Box?> componentRefAction)
//        : base(componentRefAction)
//    {

//    }
//}

//public static partial class BoxExtensions
//{
//    public static T BackgroundColor<T>(this T node, Color? value, RxColorAnimation? customAnimation = null) where T : IBox
//    {
//        node.BackgroundColor = new PropertyValue<Color?>(value);
//        if (value != null)
//        {
//            node.AppendAnimatable(Internals.Box.BackgroundColorProperty, customAnimation ?? new RxSimpleColorAnimation(value), v => node.BackgroundColor = new PropertyValue<Color?>(v.CurrentValue()));
//        }
//        return node;
//    }

//    public static T BackgroundColor<T>(this T node, Func<Color?> valueFunc) where T : IBox
//    {
//        node.BackgroundColor = new PropertyValue<Color?>(valueFunc);
//        return node;
//    }

//    public static T Background<T>(this T node, Paint? value, RxColorAnimation? customAnimation = null) where T : IBox
//    {
//        node.Background = new PropertyValue<Paint?>(value);
//        return node;
//    }

//    public static T Background<T>(this T node, Func<Paint?> valueFunc) where T : IBox
//    {
//        node.Background = new PropertyValue<Paint?>(valueFunc);
//        return node;
//    }

//    public static T BorderColor<T>(this T node, Color? value, RxColorAnimation? customAnimation = null) where T : IBox
//    {
//        node.BorderColor = new PropertyValue<Color?>(value);
//        if (value != null)
//        {
//            node.AppendAnimatable(Internals.Box.BorderColorProperty, customAnimation ?? new RxSimpleColorAnimation(value), v => node.BorderColor = new PropertyValue<Color?>(v.CurrentValue()));
//        }
//        return node;
//    }

//    public static T BorderColor<T>(this T node, Func<Color?> valueFunc) where T : IBox
//    {
//        node.BorderColor = new PropertyValue<Color?>(valueFunc);
//        return node;
//    }

//    public static T BorderSize<T>(this T node, float value, RxFloatAnimation? customAnimation = null) where T : IBox
//    {
//        node.BorderSize = new PropertyValue<float>(value);
//        node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxFloatAnimation(value), v => node.BorderSize = new PropertyValue<float>(v.CurrentValue()));
//        return node;
//    }

//    public static T BorderSize<T>(this T node, Func<float> valueFunc) where T : IBox
//    {
//        node.BorderSize = new PropertyValue<float>(valueFunc);
//        return node;
//    }

//    public static T CornerRadius<T>(this T node, float value, RxCornerRadiusFAnimation? customAnimation = null) where T : IBox
//    {
//        node.CornerRadius = new PropertyValue<CornerRadiusF>(new CornerRadiusF(value));
//        node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusFAnimation(new CornerRadiusF(value)), v => node.CornerRadius = new PropertyValue<CornerRadiusF>(v.CurrentValue()));
//        return node;
//    }

//    public static T CornerRadius<T>(this T node, Func<float> valueFunc) where T : IBox
//    {
//        node.CornerRadius = new PropertyValue<CornerRadiusF>(()=> new CornerRadiusF(valueFunc()));
//        return node;
//    }

//    public static T CornerRadius<T>(this T node, CornerRadiusF value, RxCornerRadiusFAnimation? customAnimation = null) where T : IBox
//    {
//        node.CornerRadius = new PropertyValue<CornerRadiusF>(value);
//        node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusFAnimation(value), v => node.CornerRadius = new PropertyValue<CornerRadiusF>(v.CurrentValue()));
//        return node;
//    }

//    public static T CornerRadius<T>(this T node, float topLeft, float topRight, float bottomRight, float bottomLeft, RxCornerRadiusFAnimation? customAnimation = null) where T : IBox
//    {
//        node.CornerRadius = new PropertyValue<CornerRadiusF>(new CornerRadiusF(topLeft, topRight, bottomRight, bottomLeft));
//        node.AppendAnimatable(Internals.Box.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusFAnimation(new CornerRadiusF(topLeft, topRight, bottomRight, bottomLeft)), v => node.CornerRadius = new PropertyValue<CornerRadiusF>(v.CurrentValue()));
//        return node;
//    }

//    public static T CornerRadius<T>(this T node, Func<CornerRadiusF> valueFunc) where T : IBox
//    {
//        node.CornerRadius = new PropertyValue<CornerRadiusF>(valueFunc);
//        return node;
//    }

//    public static T Padding<T>(this T node, ThicknessF value, RxThicknessFAnimation? customAnimation = null) where T : IBox
//    {
//        node.Padding = new PropertyValue<ThicknessF>(value);
//        node.AppendAnimatable(Internals.Box.PaddingProperty, customAnimation ?? new RxSimpleThicknessFAnimation(value), v => node.Padding = new PropertyValue<ThicknessF>(v.CurrentValue()));
//        return node;
//    }

//    public static T Padding<T>(this T node, float topLeft, float topRight, float bottomRight, float bottomLeft, RxThicknessFAnimation? customAnimation = null) where T : IBox
//    {
//        node.Padding = new PropertyValue<ThicknessF>(new ThicknessF(topLeft, topRight, bottomRight, bottomLeft));
//        node.AppendAnimatable(Internals.Box.PaddingProperty, customAnimation ?? new RxSimpleThicknessFAnimation(new ThicknessF(topLeft, topRight, bottomRight, bottomLeft)), v => node.Padding = new PropertyValue<ThicknessF>(v.CurrentValue()));
//        return node;
//    }

//    public static T Padding<T>(this T node, float value, RxThicknessFAnimation? customAnimation = null) where T : IBox
//    {
//        node.Padding = new PropertyValue<ThicknessF>(new ThicknessF(value));
//        node.AppendAnimatable(Internals.Box.PaddingProperty, customAnimation ?? new RxSimpleThicknessFAnimation(new ThicknessF(value)), v => node.Padding = new PropertyValue<ThicknessF>(v.CurrentValue()));
//        return node;
//    }

//    public static T Padding<T>(this T node, Func<ThicknessF> valueFunc) where T : IBox
//    {
//        node.Padding = new PropertyValue<ThicknessF>(valueFunc);
//        return node;
//    }


//}
