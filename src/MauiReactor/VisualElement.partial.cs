using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IVisualElement
    {
        Shapes.IGeometry? Clip { get; set; }   
    }

    public abstract partial class VisualElement<T>
    {
        Shapes.IGeometry? IVisualElement.Clip { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            var thisAsIVisualElement = (IVisualElement)this;

            if (thisAsIVisualElement.Clip == null)
            {
                return base.RenderChildren();
            }

            return base.RenderChildren().Concat(new[] { (VisualNode)thisAsIVisualElement.Clip });
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IVisualElement)this;

            if (widget == thisAsIVisualElement.Clip &&
                childNativeControl is Microsoft.Maui.Controls.Shapes.Geometry geometry)
            {
                NativeControl.Clip = geometry;
            }

            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IVisualElement)this;

            if (widget == thisAsIVisualElement.Clip &&
                childNativeControl is Microsoft.Maui.Controls.Shapes.Geometry)
            {
                NativeControl.Clip = null;
            }

            base.OnRemoveChild(widget, childNativeControl);
        }
    }

    public static partial class VisualElementExtensions
    {
        public static T Clip<T>(this T visualelement, Shapes.IGeometry geometry) where T : IVisualElement
        {
            visualelement.Clip = geometry;
            return visualelement;
        }
    }
}
