using System.Collections;


namespace MauiReactor
{
    public partial interface IElement : IVisualNodeWithAttachedProperties
    {
        //void SetAttachedProperty(BindableProperty property, object value);

    }

    public partial class Element<T> : IEnumerable
    {

        protected readonly List<VisualNode> _internalChildren = [];

        partial void OnReset()
        {
            _internalChildren.Clear();
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

        public void Add(params VisualNode?[]? nodes)
        {
            if (nodes is null)
            {
                return;
                //throw new ArgumentNullException(nameof(nodes));
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
                Add(visualNode);
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
            foreach (var child in children.Cast<VisualNode>())
            {
                Add(child);
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