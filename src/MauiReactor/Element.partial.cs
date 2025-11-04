using System.Collections;


namespace MauiReactor
{
    public partial interface IElement : IVisualNodeWithAttachedProperties
    {
        void AddChildren(params IEnumerable<VisualNode?>? nodes);
    }

    public partial class Element<T> : IElement, IEnumerable
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

        public void AddChildren(params IEnumerable<VisualNode?>? children)
        {
            if (children is null)
            {
                return;
            }

            if (children is VisualNode visualNode)
            {
                OnChildAdd(visualNode);
            }
            else
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        OnChildAdd(child);
                    }
                }
            }
        }

        public void AddChildren(params IEnumerable<object?>? children)
        {
            if (children is null)
            {
                return;
            }

            if (children is VisualNode visualNode)
            {
                OnChildAdd(visualNode);
            }
            else
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        if (child is VisualNode vn)
                        {
                            OnChildAdd(vn);
                        }
                        else if (child is IEnumerable nodes)
                        {
                            AddChildren(nodes);
                        }
                        else
                        {
                            throw new NotSupportedException($"Unable to add value of type '{child.GetType()}' under {typeof(T)}");
                        }
                    }
                }
            }
        }

        private void AddChildren(IEnumerable children)
        {
            if (children is null)
            {
                return;
            }

            if (children is VisualNode visualNode)
            {
                OnChildAdd(visualNode);
            }
            else
            {
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        if (child is VisualNode vn)
                        {
                            OnChildAdd(vn);
                        }
                        else if (child is IEnumerable nodes)
                        {
                            AddChildren(nodes);
                        }
                        else
                        {
                            throw new NotSupportedException($"Unable to add value of type '{child.GetType()}' under {typeof(T)}");
                        }
                    }
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