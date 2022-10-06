using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IDropShadow : ICanvasNode
    {
        PropertyValue<Color>? Color { get; set; }
        PropertyValue<SizeF>? Size { get; set; }
        PropertyValue<float>? Blur { get; set; }
    }

    public partial class DropShadow<T> : CanvasNode<T>, IDropShadow, IEnumerable where T : Internals.DropShadow, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public DropShadow()
        {

        }

        public DropShadow(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Color>? IDropShadow.Color { get; set; }
        PropertyValue<SizeF>? IDropShadow.Size { get; set; }
        PropertyValue<float>? IDropShadow.Blur { get; set; }

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

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (IDropShadow)this;

            SetPropertyValue(NativeControl, Internals.DropShadow.ColorProperty, thisAsIBorder.Color);
            SetPropertyValue(NativeControl, Internals.DropShadow.SizeProperty, thisAsIBorder.Size);
            SetPropertyValue(NativeControl, Internals.DropShadow.BlurProperty, thisAsIBorder.Blur);

            base.OnUpdate();
        }
    }

    public partial class DropShadow : DropShadow<Internals.DropShadow>
    {
        public DropShadow()
        {

        }

        public DropShadow(Action<Internals.DropShadow?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class DropShadowExtensions
    {
        public static T Color<T>(this T node, Color value) where T : IDropShadow
        {
            node.Color = new PropertyValue<Color>(value);
            return node;
        }

        public static T BackgColorroundColor<T>(this T node, Func<Color> valueFunc) where T : IDropShadow
        {
            node.Color = new PropertyValue<Color>(valueFunc);
            return node;
        }

        public static T Size<T>(this T node, SizeF value) where T : IDropShadow
        {
            node.Size = new PropertyValue<SizeF>(value);
            return node;
        }

        public static T Size<T>(this T node, float x, float y) where T : IDropShadow
        {
            node.Size = new PropertyValue<SizeF>(new SizeF(x, y));
            return node;
        }

        public static T Size<T>(this T node, Func<SizeF> valueFunc) where T : IDropShadow
        {
            node.Size = new PropertyValue<SizeF>(valueFunc);
            return node;
        }

        public static T Blur<T>(this T node, float value) where T : IDropShadow
        {
            node.Blur = new PropertyValue<float>(value);
            return node;
        }

        public static T Blur<T>(this T node, Func<float> valueFunc) where T : IDropShadow
        {
            node.Blur = new PropertyValue<float>(valueFunc);
            return node;
        }
    }
}
