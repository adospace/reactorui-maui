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
public partial interface IShadow : IElement
{
    object? Radius { get; set; }

    object? Opacity { get; set; }

    object? Brush { get; set; }

    object? Offset { get; set; }
}

public partial class Shadow<T> : Element<T>, IShadow where T : Microsoft.Maui.Controls.Shadow, new()
{
    public Shadow()
    {
        ShadowStyles.Default?.Invoke(this);
    }

    public Shadow(Action<T?> componentRefAction) : base(componentRefAction)
    {
        ShadowStyles.Default?.Invoke(this);
    }

    object? IShadow.Radius { get; set; }

    object? IShadow.Opacity { get; set; }

    object? IShadow.Brush { get; set; }

    object? IShadow.Offset { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsIShadow = (IShadow)this;
        thisAsIShadow.Radius = null;
        thisAsIShadow.Opacity = null;
        thisAsIShadow.Brush = null;
        thisAsIShadow.Offset = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIShadow = (IShadow)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shadow.RadiusProperty, thisAsIShadow.Radius);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shadow.OpacityProperty, thisAsIShadow.Opacity);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shadow.BrushProperty, thisAsIShadow.Brush);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shadow.OffsetProperty, thisAsIShadow.Offset);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ShadowStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }
}

public partial class Shadow : Shadow<Microsoft.Maui.Controls.Shadow>
{
    public Shadow()
    {
    }

    public Shadow(Action<Microsoft.Maui.Controls.Shadow?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class ShadowExtensions
{
    static object? SetOffset(object shadow, RxAnimation animation) => ((IShadow)shadow).Offset = ((RxPointAnimation)animation).CurrentValue();
    public static T Radius<T>(this T shadow, float radius)
        where T : IShadow
    {
        shadow.Radius = radius;
        return shadow;
    }

    public static T Radius<T>(this T shadow, Func<float> radiusFunc)
        where T : IShadow
    {
        shadow.Radius = new PropertyValue<float>(radiusFunc);
        return shadow;
    }

    public static T Opacity<T>(this T shadow, float opacity)
        where T : IShadow
    {
        shadow.Opacity = opacity;
        return shadow;
    }

    public static T Opacity<T>(this T shadow, Func<float> opacityFunc)
        where T : IShadow
    {
        shadow.Opacity = new PropertyValue<float>(opacityFunc);
        return shadow;
    }

    public static T Brush<T>(this T shadow, Microsoft.Maui.Controls.Brush brush)
        where T : IShadow
    {
        shadow.Brush = brush;
        return shadow;
    }

    public static T Brush<T>(this T shadow, Func<Microsoft.Maui.Controls.Brush> brushFunc)
        where T : IShadow
    {
        shadow.Brush = new PropertyValue<Microsoft.Maui.Controls.Brush>(brushFunc);
        return shadow;
    }

    public static T Offset<T>(this T shadow, Microsoft.Maui.Graphics.Point offset, RxPointAnimation? customAnimation = null)
        where T : IShadow
    {
        shadow.Offset = offset;
        shadow.AppendAnimatable(Microsoft.Maui.Controls.Shadow.OffsetProperty, customAnimation ?? new RxSimplePointAnimation(offset), SetOffset);
        return shadow;
    }

    public static T Offset<T>(this T shadow, Func<Microsoft.Maui.Graphics.Point> offsetFunc)
        where T : IShadow
    {
        shadow.Offset = new PropertyValue<Microsoft.Maui.Graphics.Point>(offsetFunc);
        return shadow;
    }

    public static T Offset<T>(this T shadow, double x, double y)
        where T : IShadow
    {
        shadow.Offset = new Microsoft.Maui.Graphics.Point(x, y);
        return shadow;
    }
}

public static partial class ShadowStyles
{
    public static Action<IShadow>? Default { get; set; }
    public static Dictionary<string, Action<IShadow>> Themes { get; } = [];
}