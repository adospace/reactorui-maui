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
public partial interface IEllipseGeometry : Shapes.IGeometry
{
}

public partial class EllipseGeometry<T> : Shapes.Geometry<T>, IEllipseGeometry where T : Microsoft.Maui.Controls.Shapes.EllipseGeometry, new()
{
    public EllipseGeometry()
    {
        EllipseGeometryStyles.Default?.Invoke(this);
    }

    public EllipseGeometry(Action<T?> componentRefAction) : base(componentRefAction)
    {
        EllipseGeometryStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && EllipseGeometryStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public partial class EllipseGeometry : EllipseGeometry<Microsoft.Maui.Controls.Shapes.EllipseGeometry>
{
    public EllipseGeometry()
    {
    }

    public EllipseGeometry(Action<Microsoft.Maui.Controls.Shapes.EllipseGeometry?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class EllipseGeometryExtensions
{
    /*
    
        
    static object? SetCenter(object ellipseGeometry, RxAnimation animation)
        => ((IEllipseGeometry)ellipseGeometry).Center = ((RxPointAnimation)animation).CurrentValue();

    
    
    
    static object? SetRadiusX(object ellipseGeometry, RxAnimation animation)
        => ((IEllipseGeometry)ellipseGeometry).RadiusX = ((RxDoubleAnimation)animation).CurrentValue();

    
    
    
    static object? SetRadiusY(object ellipseGeometry, RxAnimation animation)
        => ((IEllipseGeometry)ellipseGeometry).RadiusY = ((RxDoubleAnimation)animation).CurrentValue();

    
    */
    public static T Center<T>(this T ellipseGeometry, Microsoft.Maui.Graphics.Point center, RxPointAnimation? customAnimation = null)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.Center = center;
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.CenterProperty, center);
        ellipseGeometry.AppendAnimatable(Microsoft.Maui.Controls.Shapes.EllipseGeometry.CenterProperty, customAnimation ?? new RxSimplePointAnimation(center));
        return ellipseGeometry;
    }

    public static T Center<T>(this T ellipseGeometry, Func<Microsoft.Maui.Graphics.Point> centerFunc)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.Center = new PropertyValue<Microsoft.Maui.Graphics.Point>(centerFunc);
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.CenterProperty, new PropertyValue<Microsoft.Maui.Graphics.Point>(centerFunc));
        return ellipseGeometry;
    }

    public static T Center<T>(this T ellipseGeometry, double x, double y)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.Center = new Microsoft.Maui.Graphics.Point(x, y);
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.CenterProperty, new Microsoft.Maui.Graphics.Point(x, y));
        return ellipseGeometry;
    }

    public static T RadiusX<T>(this T ellipseGeometry, double radiusX, RxDoubleAnimation? customAnimation = null)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.RadiusX = radiusX;
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusXProperty, radiusX);
        ellipseGeometry.AppendAnimatable(Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusXProperty, customAnimation ?? new RxDoubleAnimation(radiusX));
        return ellipseGeometry;
    }

    public static T RadiusX<T>(this T ellipseGeometry, Func<double> radiusXFunc)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.RadiusX = new PropertyValue<double>(radiusXFunc);
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusXProperty, new PropertyValue<double>(radiusXFunc));
        return ellipseGeometry;
    }

    public static T RadiusY<T>(this T ellipseGeometry, double radiusY, RxDoubleAnimation? customAnimation = null)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.RadiusY = radiusY;
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusYProperty, radiusY);
        ellipseGeometry.AppendAnimatable(Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusYProperty, customAnimation ?? new RxDoubleAnimation(radiusY));
        return ellipseGeometry;
    }

    public static T RadiusY<T>(this T ellipseGeometry, Func<double> radiusYFunc)
        where T : IEllipseGeometry
    {
        //ellipseGeometry.RadiusY = new PropertyValue<double>(radiusYFunc);
        ellipseGeometry.SetProperty(Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusYProperty, new PropertyValue<double>(radiusYFunc));
        return ellipseGeometry;
    }
}

public static partial class EllipseGeometryStyles
{
    public static Action<IEllipseGeometry>? Default { get; set; }
    public static Dictionary<string, Action<IEllipseGeometry>> Themes { get; } = [];
}