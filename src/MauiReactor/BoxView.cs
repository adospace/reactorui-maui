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
public partial interface IBoxView : IView
{
    object? Color { get; set; }

    object? CornerRadius { get; set; }
}

public partial class BoxView<T> : View<T>, IBoxView where T : Microsoft.Maui.Controls.BoxView, new()
{
    public BoxView()
    {
        BoxViewStyles.Default?.Invoke(this);
    }

    public BoxView(Action<T?> componentRefAction) : base(componentRefAction)
    {
        BoxViewStyles.Default?.Invoke(this);
    }

    object? IBoxView.Color { get; set; }

    object? IBoxView.CornerRadius { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsIBoxView = (IBoxView)this;
        thisAsIBoxView.Color = null;
        thisAsIBoxView.CornerRadius = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIBoxView = (IBoxView)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BoxView.ColorProperty, thisAsIBoxView.Color);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.BoxView.CornerRadiusProperty, thisAsIBoxView.CornerRadius);
        base.OnUpdate();
        OnEndUpdate();
    }

    protected override void OnAnimate()
    {
        OnBeginAnimate();
        var thisAsIBoxView = (IBoxView)this;
        AnimateProperty(Microsoft.Maui.Controls.BoxView.CornerRadiusProperty, thisAsIBoxView.CornerRadius);
        base.OnAnimate();
        OnEndAnimate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && BoxViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }
}

public partial class BoxView : BoxView<Microsoft.Maui.Controls.BoxView>
{
    public BoxView()
    {
    }

    public BoxView(Action<Microsoft.Maui.Controls.BoxView?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class BoxViewExtensions
{
    static void SetCornerRadius(object boxView, RxAnimation animation) => ((IBoxView)boxView).CornerRadius = ((RxCornerRadiusAnimation)animation).CurrentValue();
    public static T Color<T>(this T boxView, Microsoft.Maui.Graphics.Color color)
        where T : IBoxView
    {
        boxView.Color = color;
        return boxView;
    }

    public static T Color<T>(this T boxView, Func<Microsoft.Maui.Graphics.Color> colorFunc)
        where T : IBoxView
    {
        boxView.Color = new PropertyValue<Microsoft.Maui.Graphics.Color>(colorFunc);
        return boxView;
    }

    public static T CornerRadius<T>(this T boxView, Microsoft.Maui.CornerRadius cornerRadius, RxCornerRadiusAnimation? customAnimation = null)
        where T : IBoxView
    {
        boxView.CornerRadius = cornerRadius;
        boxView.AppendAnimatable(Microsoft.Maui.Controls.BoxView.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusAnimation(cornerRadius), SetCornerRadius);
        return boxView;
    }

    public static T CornerRadius<T>(this T boxView, Func<Microsoft.Maui.CornerRadius> cornerRadiusFunc)
        where T : IBoxView
    {
        boxView.CornerRadius = new PropertyValue<Microsoft.Maui.CornerRadius>(cornerRadiusFunc);
        return boxView;
    }

    public static T CornerRadius<T>(this T boxView, double uniformRadius, RxCornerRadiusAnimation? customAnimation = null)
        where T : IBoxView
    {
        boxView.CornerRadius = new CornerRadius(uniformRadius);
        boxView.AppendAnimatable(Microsoft.Maui.Controls.BoxView.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusAnimation(new CornerRadius(uniformRadius)), SetCornerRadius);
        return boxView;
    }

    public static T CornerRadius<T>(this T boxView, double topLeft, double topRight, double bottomLeft, double bottomRight, RxCornerRadiusAnimation? customAnimation = null)
        where T : IBoxView
    {
        boxView.CornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
        boxView.AppendAnimatable(Microsoft.Maui.Controls.BoxView.CornerRadiusProperty, customAnimation ?? new RxSimpleCornerRadiusAnimation(new CornerRadius(topLeft, topRight, bottomLeft, bottomRight)), SetCornerRadius);
        return boxView;
    }
}

public static partial class BoxViewStyles
{
    public static Action<IBoxView>? Default { get; set; }
    public static Dictionary<string, Action<IBoxView>> Themes { get; } = [];
}