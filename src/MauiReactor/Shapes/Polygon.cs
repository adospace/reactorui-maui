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
public partial interface IPolygon : Shapes.IShape
{
}

public sealed partial class Polygon : Shapes.Shape<Microsoft.Maui.Controls.Shapes.Polygon>, IPolygon
{
    public Polygon(Action<Microsoft.Maui.Controls.Shapes.Polygon?>? componentRefAction = null) : base(componentRefAction)
    {
        PolygonStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && PolygonStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
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

public static partial class PolygonExtensions
{
    public static T Points<T>(this T polygon, Microsoft.Maui.Controls.PointCollection points)
        where T : IPolygon
    {
        //polygon.Points = points;
        polygon.SetProperty(Microsoft.Maui.Controls.Shapes.Polygon.PointsProperty, points);
        return polygon;
    }

    public static T Points<T>(this T polygon, Func<Microsoft.Maui.Controls.PointCollection> pointsFunc)
        where T : IPolygon
    {
        //polygon.Points = new PropertyValue<Microsoft.Maui.Controls.PointCollection>(pointsFunc);
        polygon.SetProperty(Microsoft.Maui.Controls.Shapes.Polygon.PointsProperty, new PropertyValue<Microsoft.Maui.Controls.PointCollection>(pointsFunc));
        return polygon;
    }

    public static T FillRule<T>(this T polygon, Microsoft.Maui.Controls.Shapes.FillRule fillRule)
        where T : IPolygon
    {
        //polygon.FillRule = fillRule;
        polygon.SetProperty(Microsoft.Maui.Controls.Shapes.Polygon.FillRuleProperty, fillRule);
        return polygon;
    }

    public static T FillRule<T>(this T polygon, Func<Microsoft.Maui.Controls.Shapes.FillRule> fillRuleFunc)
        where T : IPolygon
    {
        //polygon.FillRule = new PropertyValue<Microsoft.Maui.Controls.Shapes.FillRule>(fillRuleFunc);
        polygon.SetProperty(Microsoft.Maui.Controls.Shapes.Polygon.FillRuleProperty, new PropertyValue<Microsoft.Maui.Controls.Shapes.FillRule>(fillRuleFunc));
        return polygon;
    }
}

public static partial class PolygonStyles
{
    public static Action<IPolygon>? Default { get; set; }
    public static Dictionary<string, Action<IPolygon>> Themes { get; } = [];
}