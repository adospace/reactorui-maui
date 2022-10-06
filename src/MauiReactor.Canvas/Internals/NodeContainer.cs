using System.Collections.Generic;

namespace MauiReactor.Canvas.Internals
{
    public abstract class NodeContainer : CanvasVisualElement
    {
        private readonly List<CanvasNode> _children = new();

        public IReadOnlyList<CanvasNode> Children => _children;

        public void InsertChild(int index, CanvasNode child)
        {
            _children.Insert(index, child);

            OnChildAdded(child);
        }

        protected virtual void OnChildAdded(CanvasNode child)
        {
        }

        public void RemoveChild(CanvasNode child)
        {
            _children.Remove(child);

            OnChildRemoved(child);
        }

        protected virtual void OnChildRemoved(CanvasNode child)
        {
            
        }
    }

}