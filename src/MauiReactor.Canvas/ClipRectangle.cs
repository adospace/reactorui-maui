using MauiReactor.Canvas.Internals;
using MauiReactor.Internals;
using Microsoft.Maui;
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
    public partial interface IClipRectangle : ICanvasNode
    {
        PropertyValue<CornerRadiusF>? CornerRadius { get; set; }
    }

    public partial class ClipRectangle<T> : CanvasNode<T>, IClipRectangle, IEnumerable where T : Internals.ClipRectangle, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public ClipRectangle()
        {

        }

        public ClipRectangle(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<CornerRadiusF>? IClipRectangle.CornerRadius { get; set; }

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
            var thisAsIBorder = (IClipRectangle)this;

            SetPropertyValue(NativeControl, Internals.ClipRectangle.CornerRadiusProperty, thisAsIBorder.CornerRadius);

            base.OnUpdate();
        }
    }

    public partial class ClipRectangle : ClipRectangle<Internals.ClipRectangle>
    {
        public ClipRectangle()
        {

        }

        public ClipRectangle(Action<Internals.ClipRectangle?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ClipRectangleExtensions
    {
        public static T CornerRadius<T>(this T node, CornerRadiusF value) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(value);
            return node;
        }

        public static T CornerRadius<T>(this T node, float topLeft, float topRight, float bottomRight, float bottomLeft) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(new CornerRadiusF(topLeft, topRight, bottomRight, bottomLeft));
            return node;
        }

        public static T CornerRadius<T>(this T node, float uniformSize) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(new CornerRadiusF(uniformSize));
            return node;
        }

        public static T CornerRadius<T>(this T node, Func<CornerRadiusF> valueFunc) where T : IClipRectangle
        {
            node.CornerRadius = new PropertyValue<CornerRadiusF>(valueFunc);
            return node;
        }
    }

}
