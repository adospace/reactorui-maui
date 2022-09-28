using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface ICanvasNode : IVisualNode
    {
        PropertyValue<Thickness>? Margin { get; set; }
    }

    public partial class CanvasNode<T> : VisualNode<T>, ICanvasNode where T : Internals.CanvasNode, new()
    {
        public CanvasNode()
        {

        }

        public CanvasNode(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Thickness>? ICanvasNode.Margin { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIGraphicsView = (ICanvasNode)this;

            SetPropertyValue(NativeControl, Internals.CanvasNode.MarginProperty, thisAsIGraphicsView.Margin);

            base.OnUpdate();
        }
    }

    public partial class CanvasNode : CanvasNode<Internals.CanvasNode>
    {
        public CanvasNode()
        {

        }

        public CanvasNode(Action<Internals.CanvasNode?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class CanvasNodeExtensions
    {
        public static T Margin<T>(this T node, Thickness value) where T : IBox
        {
            node.Margin = new PropertyValue<Thickness>(value);
            return node;
        }

        public static T Margin<T>(this T node, Func<Thickness> valueFunc) where T : IBox
        {
            node.Margin = new PropertyValue<Thickness>(valueFunc);
            return node;
        }
    }
}
