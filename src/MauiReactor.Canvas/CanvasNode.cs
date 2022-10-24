using MauiReactor.Internals;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections;
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
        PropertyValue<int>? ZIndex { get; set; }
    }

    public partial class CanvasNode<T> : VisualNode<T>, ICanvasNode, IEnumerable where T : Internals.CanvasNode, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();
        
        public CanvasNode()
        {

        }

        public CanvasNode(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

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
                NativeControl.InsertChild(widget.ChildIndex, node);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.RemoveChild(node);
            }

            base.OnRemoveChild(widget, childControl);
        }

        PropertyValue<bool>? ICanvasNode.IsVisible { get; set; }
        PropertyValue<int>? ICanvasNode.ZIndex { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICanvasNode = (ICanvasNode)this;

            SetPropertyValue(NativeControl, Internals.CanvasNode.IsVisibleProperty, thisAsICanvasNode.IsVisible); 
            SetPropertyValue(NativeControl, Internals.CanvasNode.ZIndexProperty, thisAsICanvasNode.ZIndex); 

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

        public static T ZIndex<T>(this T node, int value) where T : ICanvasNode
        {
            node.ZIndex = new PropertyValue<int>(value);
            return node;
        }

        public static T ZIndex<T>(this T node, Func<int> valueFunc) where T : ICanvasNode
        {
            node.ZIndex = new PropertyValue<int>(valueFunc);
            return node;
        }
    }
}
