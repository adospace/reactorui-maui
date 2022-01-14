using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public abstract partial class Layout<T> : IEnumerable
    {
        private readonly List<VisualNode> _internalChildren = new List<VisualNode>();

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.IView control)
            {
                NativeControl.Children.Insert(widget.ChildIndex, control);
            }
            else
            {
                throw new InvalidOperationException($"Type '{childControl.GetType()}' not supported under '{GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Children.Remove((Microsoft.Maui.IView)childControl);

            base.OnRemoveChild(widget, childControl);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                    OnChildAdded(node);
                }
            }
        }

        public void Add(object? genericNode)
        {
            if (genericNode == null)
            {
                return;
            }

            if (genericNode is VisualNode visualNode)
            {
                _internalChildren.Add(visualNode);
                OnChildAdded(visualNode);
            }
            else if (genericNode is IEnumerable nodes)
            {
                foreach (var node in nodes.Cast<VisualNode>())
                {
                    _internalChildren.Add(node);
                    OnChildAdded(node);
                }
            }
            else
            {
                throw new NotSupportedException($"Unable to add value of type '{genericNode.GetType()}' under {typeof(T)}");
            }
        }

        protected virtual void OnChildAdded(VisualNode child)
        { }
    }
}
