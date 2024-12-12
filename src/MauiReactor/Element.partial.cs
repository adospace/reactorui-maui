using System.Collections;


namespace MauiReactor
{
    public partial interface IElement : IVisualNodeWithAttachedProperties
    {
        void Add(params IEnumerable<VisualNode?>? nodes);
    }

    public partial class Element<T> : IEnumerable
    {

        protected readonly List<VisualNode> _internalChildren = [];

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

        public void Add(params IEnumerable<VisualNode?>? nodes)
        {
            if (nodes is null)
            {
                return;
            }

            foreach (var node in nodes)
            {
                if (node != null)
                {
                    OnChildAdd(node);
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
                OnChildAdd(visualNode);
            }
            else if (genericNode is IEnumerable nodes)
            {
                AddChildren(nodes.Cast<VisualNode>());
            }
            else
            {
                throw new NotSupportedException($"Unable to add value of type '{genericNode.GetType()}' under {typeof(T)}");
            }
        }

        public void AddChildren(IEnumerable<VisualNode?> children)
        {
            foreach (var child in children)
            {
                if (child != null)
                {
                    OnChildAdd(child);
                }
            }
        }

        protected virtual void OnChildAdd(VisualNode node)
        {
            _internalChildren.Add(node);
            OnChildAdded(node);
        }

        protected virtual void OnChildAdded(VisualNode node)
        { }
    }
}