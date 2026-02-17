using MauiReactor.Internals;
using MauiReactor.Shapes;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Platform;

namespace MauiReactor;

public partial interface IBorder
{ 
    Shapes.IShape? StrokeShape { get; set; }
}


public partial class Border<T>
{
    Shapes.IShape? IBorder.StrokeShape { get; set; }


    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsIVisualElement = (IBorder)this;

        if (thisAsIVisualElement.StrokeShape == null)
        {
            return base.RenderChildren();
        }

        return base.RenderChildren().Concat([(VisualNode)thisAsIVisualElement.StrokeShape]);
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIBorder = (IBorder)this;

        if (widget == thisAsIBorder.StrokeShape &&
            childNativeControl is Microsoft.Maui.Graphics.IShape shape)
        {
            NativeControl.StrokeShape = shape;
            var shapeDepObject = (BindableObject)shape;
            shapeDepObject.SetValue(ShapeBindableProperties.ShapeOfBorderProperty, NativeControl);
        }
        else if (childNativeControl is View view)
        {
            NativeControl.Content = view;
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIVisualElement = (IBorder)this;

        if (widget == thisAsIVisualElement.StrokeShape &&
            childNativeControl is Microsoft.Maui.Graphics.IShape shape)
        {
            NativeControl.StrokeShape = null;
            var shapeDepObject = (BindableObject)shape;
            shapeDepObject.SetValue(ShapeBindableProperties.ShapeOfBorderProperty, null);
        }
        else if (childNativeControl is View)
        {
            NativeControl.Content = null;
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}

public static partial class BorderExtensions
{
    public static T StrokeShape<T>(this T border, Shapes.IShape shape) where T : IBorder
    {
        border.StrokeShape = shape;
        return border;
    }

    public static T StrokeCornerRadius<T>(this T border, CornerRadius cornerRadius) where T : IBorder
    {
        border.StrokeShape = new Shapes.RoundRectangle().CornerRadius(cornerRadius);
        return border;
    }

    public static T StrokeCornerRadius<T>(this T border, double uniformRadius) where T : IBorder
    {
        border.StrokeShape = new Shapes.RoundRectangle().CornerRadius(uniformRadius);
        return border;
    }

    public static T StrokeCornerRadius<T>(this T border, double topLeft, double topRight, double bottomLeft, double bottomRight) where T : IBorder
    {
        border.StrokeShape = new Shapes.RoundRectangle().CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
        return border;
    }
}
