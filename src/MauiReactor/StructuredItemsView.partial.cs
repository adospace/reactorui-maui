using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IStructuredItemsView
    {
        IItemsLayout? ItemsLayout { get; set; }

        VisualNode? Header { get; set; }
        VisualNode? Footer { get; set; }

    }
    public partial class StructuredItemsView<T>
    {
        IItemsLayout? IStructuredItemsView.ItemsLayout { get; set; }

        VisualNode? IStructuredItemsView.Header { get; set; }
        VisualNode? IStructuredItemsView.Footer { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            var thisAsIVisualElement = (IStructuredItemsView)this;

            var children = base.RenderChildren();

            if (thisAsIVisualElement.ItemsLayout != null)
            {
                children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.ItemsLayout });
            }
            if (thisAsIVisualElement.Header != null)
            {
                children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.Header });
            }
            if (thisAsIVisualElement.Footer != null)
            {
                children = children.Concat(new[] { (VisualNode)thisAsIVisualElement.Footer });
            }

            return children;
        }


        protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IStructuredItemsView)this;

            if (widget == thisAsIVisualElement.ItemsLayout &&
                childNativeControl is Microsoft.Maui.Controls.IItemsLayout itemsLayout)
            {
                NativeControl.ItemsLayout = itemsLayout;
            }
            else if (widget == thisAsIVisualElement.Header)
            {
                NativeControl.Header = childNativeControl;
            }
            else if (widget == thisAsIVisualElement.Footer)
            {
                NativeControl.Footer = childNativeControl;
            }


            base.OnAddChild(widget, childNativeControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIVisualElement = (IStructuredItemsView)this;

            if (widget == thisAsIVisualElement.ItemsLayout &&
                childNativeControl is Microsoft.Maui.Controls.IItemsLayout)
            {
                NativeControl.ItemsLayout = null;
            }
            else if (widget == thisAsIVisualElement.Header)
            {
                NativeControl.Header = null;
            }
            else if (widget == thisAsIVisualElement.Footer)
            {
                NativeControl.Footer = null;
            }

            base.OnRemoveChild(widget, childNativeControl);
        }
    }

    public static partial class StructuredItemsViewExtensions
    {
        public static T ItemsLayout<T>(this T structureditemsview, IItemsLayout itemsLayout) where T : IStructuredItemsView
        {
            structureditemsview.ItemsLayout = itemsLayout;
            return structureditemsview;
        }

        public static T Header<T>(this T structureditemsview, VisualNode header) where T : IStructuredItemsView
        {
            structureditemsview.Header = header;
            return structureditemsview;
        }

        public static T Footer<T>(this T structureditemsview, VisualNode footer) where T : IStructuredItemsView
        {
            structureditemsview.Footer = footer;
            return structureditemsview;
        }
    }
}
