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
    public partial interface INodeContainer : ICanvasNode
    {
    }

    public abstract partial class NodeContainer<T> : CanvasNode<T>, INodeContainer, IEnumerable where T : Internals.NodeContainer, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public NodeContainer()
        {

        }

        public NodeContainer(Action<T?> componentRefAction)
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

        protected override void OnUpdate()
        {
            //Validate.EnsureNotNull(NativeControl);
            //var thisAsIBorder = (IGroup)this;


            base.OnUpdate();
        }
    }

    public static partial class NodeContainerExtensions
    {
    }
}
