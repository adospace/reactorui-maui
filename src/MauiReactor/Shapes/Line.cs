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
namespace MauiReactor.Shapes;
public partial interface ILine : Shapes.IShape
{
    object? X1 { get; set; }

    object? Y1 { get; set; }

    object? X2 { get; set; }

    object? Y2 { get; set; }
}

public sealed partial class Line : Shapes.Shape<Microsoft.Maui.Controls.Shapes.Line>, ILine
{
    public Line()
    {
    }

    public Line(Action<Microsoft.Maui.Controls.Shapes.Line?> componentRefAction) : base(componentRefAction)
    {
    }

    object? ILine.X1 { get; set; }

    object? ILine.Y1 { get; set; }

    object? ILine.X2 { get; set; }

    object? ILine.Y2 { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsILine = (ILine)this;
        thisAsILine.X1 = null;
        thisAsILine.Y1 = null;
        thisAsILine.X2 = null;
        thisAsILine.Y2 = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsILine = (ILine)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Line.X1Property, thisAsILine.X1);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Line.Y1Property, thisAsILine.Y1);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Line.X2Property, thisAsILine.X2);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Line.Y2Property, thisAsILine.Y2);
        base.OnUpdate();
        OnEndUpdate();
    }

    protected override void OnAnimate()
    {
        OnBeginAnimate();
        var thisAsILine = (ILine)this;
        AnimateProperty(Microsoft.Maui.Controls.Shapes.Line.X1Property, thisAsILine.X1);
        AnimateProperty(Microsoft.Maui.Controls.Shapes.Line.Y1Property, thisAsILine.Y1);
        AnimateProperty(Microsoft.Maui.Controls.Shapes.Line.X2Property, thisAsILine.X2);
        AnimateProperty(Microsoft.Maui.Controls.Shapes.Line.Y2Property, thisAsILine.Y2);
        base.OnAnimate();
        OnEndAnimate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
}

public static partial class LineExtensions
{
    static void SetX1(object line, RxAnimation animation) => ((ILine)line).X1 = ((RxDoubleAnimation)animation).CurrentValue();
    static void SetY1(object line, RxAnimation animation) => ((ILine)line).Y1 = ((RxDoubleAnimation)animation).CurrentValue();
    static void SetX2(object line, RxAnimation animation) => ((ILine)line).X2 = ((RxDoubleAnimation)animation).CurrentValue();
    static void SetY2(object line, RxAnimation animation) => ((ILine)line).Y2 = ((RxDoubleAnimation)animation).CurrentValue();
    public static T X1<T>(this T line, double x1, RxDoubleAnimation? customAnimation = null)
        where T : ILine
    {
        line.X1 = x1;
        line.AppendAnimatable(Microsoft.Maui.Controls.Shapes.Line.X1Property, customAnimation ?? new RxDoubleAnimation(x1), SetX1);
        return line;
    }

    public static T X1<T>(this T line, Func<double> x1Func)
        where T : ILine
    {
        line.X1 = new PropertyValue<double>(x1Func);
        return line;
    }

    public static T Y1<T>(this T line, double y1, RxDoubleAnimation? customAnimation = null)
        where T : ILine
    {
        line.Y1 = y1;
        line.AppendAnimatable(Microsoft.Maui.Controls.Shapes.Line.Y1Property, customAnimation ?? new RxDoubleAnimation(y1), SetY1);
        return line;
    }

    public static T Y1<T>(this T line, Func<double> y1Func)
        where T : ILine
    {
        line.Y1 = new PropertyValue<double>(y1Func);
        return line;
    }

    public static T X2<T>(this T line, double x2, RxDoubleAnimation? customAnimation = null)
        where T : ILine
    {
        line.X2 = x2;
        line.AppendAnimatable(Microsoft.Maui.Controls.Shapes.Line.X2Property, customAnimation ?? new RxDoubleAnimation(x2), SetX2);
        return line;
    }

    public static T X2<T>(this T line, Func<double> x2Func)
        where T : ILine
    {
        line.X2 = new PropertyValue<double>(x2Func);
        return line;
    }

    public static T Y2<T>(this T line, double y2, RxDoubleAnimation? customAnimation = null)
        where T : ILine
    {
        line.Y2 = y2;
        line.AppendAnimatable(Microsoft.Maui.Controls.Shapes.Line.Y2Property, customAnimation ?? new RxDoubleAnimation(y2), SetY2);
        return line;
    }

    public static T Y2<T>(this T line, Func<double> y2Func)
        where T : ILine
    {
        line.Y2 = new PropertyValue<double>(y2Func);
        return line;
    }
}