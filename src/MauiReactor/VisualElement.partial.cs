using MauiReactor.Internals;
using System.Linq;

namespace MauiReactor
{
    public partial interface IVisualElement
    {
        Shapes.IGeometry? Clip { get; set; }
        IShadow? Shadow { get; set; }
        int? ZIndex { get; set; }
    }

    public abstract partial class VisualElement<T>
    {
        Shapes.IGeometry? IVisualElement.Clip { get; set; }
        int? IVisualElement.ZIndex { get; set; }

        
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
                NativeControl.Shadow = null!;
            }

            base.OnRemoveChild(widget, childNativeControl);
        }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IVisualElement)this;

            if (thisAsIVisualElement.ZIndex != null)
            {
                NativeControl.ZIndex = thisAsIVisualElement.ZIndex.Value;
            }
        }
    }

    public static partial class VisualElementExtensions
    {
        public static T Clip<T>(this T visualelement, Shapes.IGeometry geometry) where T : IVisualElement
        {
            visualelement.Clip = geometry;
            return visualelement;
        }

        public static T ZIndex<T>(this T visualelement, int zIndex) where T : IVisualElement
        {
            visualelement.ZIndex = zIndex;
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
