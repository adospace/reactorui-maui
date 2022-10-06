using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IAlign : ICanvasNode
    {
        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? HorizontalAlignment { get; set; }
        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? VerticalAlignment { get; set; }
        PropertyValue<float>? Width { get; set; }
        PropertyValue<float>? Height { get; set; }
    }

    public partial class Align<T> : CanvasNode<T>, IAlign, IEnumerable where T : Internals.Align, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public Align()
        {

        }

        public Align(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? IAlign.HorizontalAlignment { get; set; }
        PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>? IAlign.VerticalAlignment { get; set; }
        PropertyValue<float>? IAlign.Width { get; set; }
        PropertyValue<float>? IAlign.Height { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasVisualElement node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasVisualElement node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIAlign = (IAlign)this;

            SetPropertyValue(NativeControl, Internals.Align.HorizontalAlignmentProperty, thisAsIAlign.HorizontalAlignment);
            SetPropertyValue(NativeControl, Internals.Align.VerticalAlignmentProperty, thisAsIAlign.VerticalAlignment);
            SetPropertyValue(NativeControl, Internals.Align.WidthProperty, thisAsIAlign.Width);
            SetPropertyValue(NativeControl, Internals.Align.HeightProperty, thisAsIAlign.Height);

            base.OnUpdate();
        }
    }

    public partial class Align : Align<Internals.Align>
    {
        public Align()
        {

        }

        public Align(Action<Internals.Align?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class AlignExtensions
    {
        public static T HorizontalAlignment<T>(this T node, Microsoft.Maui.Primitives.LayoutAlignment value) where T : IAlign
        {
            node.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(value);
            return node;
        }

        public static T HStart<T>(this T view) where T : IAlign
        {
            view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Start);
            return view;
        }

        public static T HCenter<T>(this T view) where T : IAlign
        {
            view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Center);
            return view;
        }

        public static T HEnd<T>(this T view) where T : IAlign
        {
            view.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.End);
            return view;
        }

        public static T HorizontalAlignment<T>(this T node, Func<Microsoft.Maui.Primitives.LayoutAlignment> valueFunc) where T : IAlign
        {
            node.HorizontalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(valueFunc);
            return node;
        }

        public static T VerticalAlignment<T>(this T node, Microsoft.Maui.Primitives.LayoutAlignment value) where T : IAlign
        {
            node.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(value);
            return node;
        }


        public static T VStart<T>(this T view) where T : IAlign
        {
            view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Start);
            return view;
        }

        public static T VCenter<T>(this T view) where T : IAlign
        {
            view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.Center);
            return view;
        }

        public static T VEnd<T>(this T view) where T : IAlign
        {
            view.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(Microsoft.Maui.Primitives.LayoutAlignment.End);
            return view;
        }


        public static T VerticalAlignment<T>(this T node, Func<Microsoft.Maui.Primitives.LayoutAlignment> valueFunc) where T : IAlign
        {
            node.VerticalAlignment = new PropertyValue<Microsoft.Maui.Primitives.LayoutAlignment>(valueFunc);
            return node;
        }

        public static T Width<T>(this T node, float value) where T : IAlign
        {
            node.Width = new PropertyValue<float>(value);
            return node;
        }

        public static T Width<T>(this T node, Func<float> valueFunc) where T : IAlign
        {
            node.Width = new PropertyValue<float>(valueFunc);
            return node;
        }

        public static T Height<T>(this T node, float value) where T : IAlign
        {
            node.Height = new PropertyValue<float>(value);
            return node;
        }

        public static T Height<T>(this T node, Func<float> valueFunc) where T : IAlign
        {
            node.Height = new PropertyValue<float>(valueFunc);
            return node;
        }
    }
}
