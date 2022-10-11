using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasNode : BindableObject, INodeContainer, ICanvasNodeParent
    {
        private bool _invalidateRequested = false;
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CanvasNode), true);

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            RequestInvalidate();
            base.OnPropertyChanged(propertyName);
        }

        private readonly List<CanvasNode> _children = new();

        public IReadOnlyList<CanvasNode> Children => _children;

        public ICanvasNodeParent? Parent { get; internal set; }

        public void InsertChild(int index, CanvasNode child)
        {
            _children.Insert(index, child);
            child.Parent = this;

            OnChildAdded(child);
        }

        protected virtual void OnChildAdded(CanvasNode child)
        {
        }

        public void RemoveChild(CanvasNode child)
        {
            _children.Remove(child);

            if (child.Parent == this)
            {
                child.Parent = null;
            }

            OnChildRemoved(child);
        }

        protected virtual void OnChildRemoved(CanvasNode child)
        {

        }

        public void Draw(DrawingContext context)
        {
            if (IsVisible)
            {
                DrawOverride(context);
            }
        }

        protected virtual void DrawOverride(DrawingContext context)
        {
            OnDraw(context);
        }

        protected virtual void OnDraw(DrawingContext context)
        {
            _invalidateRequested = false;
        }        

        public void RequestInvalidate()
        {
            if (!_invalidateRequested)
            {
                Parent?.RequestInvalidate();
            }
        }
    }

}