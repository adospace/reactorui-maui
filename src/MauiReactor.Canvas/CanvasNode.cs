using MauiReactor.Internals;
using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface ICanvasNode : IVisualNode
    {
        PropertyValue<bool>? IsVisible { get; set; }
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

        PropertyValue<bool>? ICanvasNode.IsVisible { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICanvasNode = (ICanvasNode)this;

            SetPropertyValue(NativeControl, Internals.CanvasVisualElement.IsVisibleProperty, thisAsICanvasNode.IsVisible);

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
        public static T IsVisible<T>(this T node, bool value) where T : ICanvasNode
        {
            node.IsVisible = new PropertyValue<bool>(value);
            return node;
        }

        public static T IsVisible<T>(this T node, Func<bool> valueFunc) where T : ICanvasNode
        {
            node.IsVisible = new PropertyValue<bool>(valueFunc);
            return node;
        }


    }
}
