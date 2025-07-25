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
public partial interface IBorder : IView
{
}

public partial class Border<T> : View<T>, IBorder where T : Microsoft.Maui.Controls.Border, new()
{
    public Border(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        BorderStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && BorderStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class Border : Border<Microsoft.Maui.Controls.Border>
{
    public Border(Action<Microsoft.Maui.Controls.Border?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public Border(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class BorderExtensions
{
    public static T Padding<T>(this T border, Microsoft.Maui.Thickness padding, RxThicknessAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.Padding = padding;
        border.SetProperty(Microsoft.Maui.Controls.Border.PaddingProperty, padding);
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(padding));
        return border;
    }

    public static T Padding<T>(this T border, Func<Microsoft.Maui.Thickness> paddingFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.PaddingProperty, new PropertyValue<Microsoft.Maui.Thickness>(paddingFunc, componentWithState));
        return border;
    }

    public static T Padding<T>(this T border, double leftRight, double topBottom, RxThicknessAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.Padding = new Thickness(leftRight, topBottom);
        border.SetProperty(Microsoft.Maui.Controls.Border.PaddingProperty, new Thickness(leftRight, topBottom));
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(leftRight, topBottom)));
        return border;
    }

    public static T Padding<T>(this T border, double uniformSize, RxThicknessAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.Padding = new Thickness(uniformSize);
        border.SetProperty(Microsoft.Maui.Controls.Border.PaddingProperty, new Thickness(uniformSize));
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(uniformSize)));
        return border;
    }

    public static T Padding<T>(this T border, double left, double top, double right, double bottom, RxThicknessAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.Padding = new Thickness(left, top, right, bottom);
        border.SetProperty(Microsoft.Maui.Controls.Border.PaddingProperty, new Thickness(left, top, right, bottom));
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.PaddingProperty, customAnimation ?? new RxSimpleThicknessAnimation(new Thickness(left, top, right, bottom)));
        return border;
    }

    public static T Stroke<T>(this T border, Microsoft.Maui.Controls.Brush stroke)
        where T : IBorder
    {
        //border.Stroke = stroke;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeProperty, stroke);
        return border;
    }

    public static T Stroke<T>(this T border, Func<Microsoft.Maui.Controls.Brush> strokeFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeProperty, new PropertyValue<Microsoft.Maui.Controls.Brush>(strokeFunc, componentWithState));
        return border;
    }

    public static T StrokeThickness<T>(this T border, double strokeThickness, RxDoubleAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.StrokeThickness = strokeThickness;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeThicknessProperty, strokeThickness);
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.StrokeThicknessProperty, customAnimation ?? new RxDoubleAnimation(strokeThickness));
        return border;
    }

    public static T StrokeThickness<T>(this T border, Func<double> strokeThicknessFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeThicknessProperty, new PropertyValue<double>(strokeThicknessFunc, componentWithState));
        return border;
    }

    public static T StrokeDashArray<T>(this T border, Microsoft.Maui.Controls.DoubleCollection strokeDashArray)
        where T : IBorder
    {
        //border.StrokeDashArray = strokeDashArray;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeDashArrayProperty, strokeDashArray);
        return border;
    }

    public static T StrokeDashArray<T>(this T border, Func<Microsoft.Maui.Controls.DoubleCollection> strokeDashArrayFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeDashArrayProperty, new PropertyValue<Microsoft.Maui.Controls.DoubleCollection>(strokeDashArrayFunc, componentWithState));
        return border;
    }

    public static T StrokeDashOffset<T>(this T border, double strokeDashOffset, RxDoubleAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.StrokeDashOffset = strokeDashOffset;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeDashOffsetProperty, strokeDashOffset);
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.StrokeDashOffsetProperty, customAnimation ?? new RxDoubleAnimation(strokeDashOffset));
        return border;
    }

    public static T StrokeDashOffset<T>(this T border, Func<double> strokeDashOffsetFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeDashOffsetProperty, new PropertyValue<double>(strokeDashOffsetFunc, componentWithState));
        return border;
    }

    public static T StrokeLineCap<T>(this T border, Microsoft.Maui.Controls.Shapes.PenLineCap strokeLineCap)
        where T : IBorder
    {
        //border.StrokeLineCap = strokeLineCap;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeLineCapProperty, strokeLineCap);
        return border;
    }

    public static T StrokeLineCap<T>(this T border, Func<Microsoft.Maui.Controls.Shapes.PenLineCap> strokeLineCapFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeLineCapProperty, new PropertyValue<Microsoft.Maui.Controls.Shapes.PenLineCap>(strokeLineCapFunc, componentWithState));
        return border;
    }

    public static T StrokeLineJoin<T>(this T border, Microsoft.Maui.Controls.Shapes.PenLineJoin strokeLineJoin)
        where T : IBorder
    {
        //border.StrokeLineJoin = strokeLineJoin;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeLineJoinProperty, strokeLineJoin);
        return border;
    }

    public static T StrokeLineJoin<T>(this T border, Func<Microsoft.Maui.Controls.Shapes.PenLineJoin> strokeLineJoinFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeLineJoinProperty, new PropertyValue<Microsoft.Maui.Controls.Shapes.PenLineJoin>(strokeLineJoinFunc, componentWithState));
        return border;
    }

    public static T StrokeMiterLimit<T>(this T border, double strokeMiterLimit, RxDoubleAnimation? customAnimation = null)
        where T : IBorder
    {
        //border.StrokeMiterLimit = strokeMiterLimit;
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeMiterLimitProperty, strokeMiterLimit);
        border.AppendAnimatable(Microsoft.Maui.Controls.Border.StrokeMiterLimitProperty, customAnimation ?? new RxDoubleAnimation(strokeMiterLimit));
        return border;
    }

    public static T StrokeMiterLimit<T>(this T border, Func<double> strokeMiterLimitFunc, IComponentWithState? componentWithState = null)
        where T : IBorder
    {
        border.SetProperty(Microsoft.Maui.Controls.Border.StrokeMiterLimitProperty, new PropertyValue<double>(strokeMiterLimitFunc, componentWithState));
        return border;
    }
}

public static partial class BorderStyles
{
    public static Action<IBorder>? Default { get; set; }
    public static Dictionary<string, Action<IBorder>> Themes { get; } = [];
}