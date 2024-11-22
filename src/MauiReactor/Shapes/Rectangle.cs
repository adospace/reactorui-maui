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
public partial interface IRectangle : Shapes.IShape
{
    object? RadiusX { get; set; }

    object? RadiusY { get; set; }
}

public sealed partial class Rectangle : Shapes.Shape<Microsoft.Maui.Controls.Shapes.Rectangle>, IRectangle
{
    public Rectangle()
    {
        RectangleStyles.Default?.Invoke(this);
    }

    public Rectangle(Action<Microsoft.Maui.Controls.Shapes.Rectangle?> componentRefAction) : base(componentRefAction)
    {
        RectangleStyles.Default?.Invoke(this);
    }

    object? IRectangle.RadiusX { get; set; }

    object? IRectangle.RadiusY { get; set; }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIRectangle = (IRectangle)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Rectangle.RadiusXProperty, thisAsIRectangle.RadiusX);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Rectangle.RadiusYProperty, thisAsIRectangle.RadiusY);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && RectangleStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public static partial class RectangleExtensions
{
    static object? SetRadiusX(object rectangle, RxAnimation animation) => ((IRectangle)rectangle).RadiusX = ((RxDoubleAnimation)animation).CurrentValue();
    static object? SetRadiusY(object rectangle, RxAnimation animation) => ((IRectangle)rectangle).RadiusY = ((RxDoubleAnimation)animation).CurrentValue();
    public static T RadiusX<T>(this T rectangle, double radiusX, RxDoubleAnimation? customAnimation = null)
        where T : IRectangle
    {
        rectangle.RadiusX = radiusX;
        rectangle.AppendAnimatable(Microsoft.Maui.Controls.Shapes.Rectangle.RadiusXProperty, customAnimation ?? new RxDoubleAnimation(radiusX), SetRadiusX);
        return rectangle;
    }

    public static T RadiusX<T>(this T rectangle, Func<double> radiusXFunc)
        where T : IRectangle
    {
        rectangle.RadiusX = new PropertyValue<double>(radiusXFunc);
        return rectangle;
    }

    public static T RadiusY<T>(this T rectangle, double radiusY, RxDoubleAnimation? customAnimation = null)
        where T : IRectangle
    {
        rectangle.RadiusY = radiusY;
        rectangle.AppendAnimatable(Microsoft.Maui.Controls.Shapes.Rectangle.RadiusYProperty, customAnimation ?? new RxDoubleAnimation(radiusY), SetRadiusY);
        return rectangle;
    }

    public static T RadiusY<T>(this T rectangle, Func<double> radiusYFunc)
        where T : IRectangle
    {
        rectangle.RadiusY = new PropertyValue<double>(radiusYFunc);
        return rectangle;
    }
}

public static partial class RectangleStyles
{
    public static Action<IRectangle>? Default { get; set; }
    public static Dictionary<string, Action<IRectangle>> Themes { get; } = [];
}