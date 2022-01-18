using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
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

            return base.RenderChildren().Concat(new[] { (VisualNode)thisAsIVisualElement.StrokeShape });
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IBorder)this;

            if (widget == thisAsIVisualElement.StrokeShape &&
                childNativeControl is Microsoft.Maui.Graphics.IShape shape)
            {
                NativeControl.StrokeShape = shape;
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
                childNativeControl is Microsoft.Maui.Graphics.IShape)
            {
                NativeControl.StrokeShape = null;
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
    }
}
