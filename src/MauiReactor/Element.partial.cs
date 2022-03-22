using System.Collections;


namespace MauiReactor
{
    public partial interface IElement : IVisualNode
    {
        void SetAttachedProperty(BindableProperty property, object value);

    }

    public partial class Element<T> : IEnumerable
    {

        protected readonly List<VisualNode> _internalChildren = new();

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
                foreach (var node in nodes.Cast<VisualNode>())
                {
                    Add(node);
                }
            }
            else
            {
                throw new NotSupportedException($"Unable to add value of type '{genericNode.GetType()}' under {typeof(T)}");
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

    public static partial class ElementExtensions
    {

    }
}