using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Shapes;

internal static class ShapeBindableProperties
{
    public static BindableProperty ShapeOfBorderProperty = BindableProperty.Create(nameof(ShapeOfBorderProperty), typeof(Microsoft.Maui.Controls.Border), typeof(ShapeBindableProperties), null);
}

public abstract partial class Shape<T>
{
    protected override void OnUpdate()
    {
        base.OnUpdate();
        Validate.EnsureNotNull(NativeControl);

        //HACK: properties changes on the shape are not reflected to the border
        //https://github.com/adospace/reactorui-maui/issues/32

        var parentBorder = (Microsoft.Maui.Controls.Border?)NativeControl.GetValue(ShapeBindableProperties.ShapeOfBorderProperty);
        if (parentBorder != null && parentBorder.StrokeShape == NativeControl)
        {
            var tmp = parentBorder.StrokeShape;
            parentBorder.StrokeShape = null;
            parentBorder.StrokeShape = tmp;
        }
    }

    partial void OnEndAnimate()
    {
        Validate.EnsureNotNull(NativeControl);

        //HACK: properties changes on the shape are not reflected to the border
        //https://github.com/adospace/reactorui-maui/issues/32
        var parentBorder = (Microsoft.Maui.Controls.Border?)NativeControl.GetValue(ShapeBindableProperties.ShapeOfBorderProperty);
        if (parentBorder != null && parentBorder.StrokeShape == NativeControl)
        {
            var tmp = parentBorder.StrokeShape;
            parentBorder.StrokeShape = null;
            parentBorder.StrokeShape = tmp;
        }
    }
}
