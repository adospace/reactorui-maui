using MauiReactor.Internals;
using System.Linq;

namespace MauiReactor
{
    public partial interface IVisualElement
    {
        Shapes.IGeometry? Clip { get; set; }
        IShadow? Shadow { get; set; }
    }

    public abstract partial class VisualElement<T>
    {
        Shapes.IGeometry? IVisualElement.Clip { get; set; }
        
        IShadow? IVisualElement.Shadow { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            var thisAsIVisualElement = (IVisualElement)this;

            var children = base.RenderChildren();

            if (thisAsIVisualElement.Clip != null)
            {
                children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.Clip });
            }

            if (thisAsIVisualElement.Shadow != null)
            {
                children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.Shadow });
            }

            return children;
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
            else if (widget == thisAsIVisualElement.Shadow &&
                childNativeControl is Microsoft.Maui.Controls.Shadow shadow)
            {
                NativeControl.Shadow = shadow;
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
            else if (widget == thisAsIVisualElement.Shadow &&
                childNativeControl is Microsoft.Maui.Controls.Shadow)
            {
                NativeControl.Shadow = null;
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

        public static T Shadow<T>(this T visualelement, IShadow shadow) where T : IVisualElement
        {
            visualelement.Shadow = shadow;
            return visualelement;
        }
    }
    
    public class VisualStateNamedGroup
    {
        public const string Common = "CommonStates";
    }
}
