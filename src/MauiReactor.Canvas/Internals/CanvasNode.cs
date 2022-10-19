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

        private readonly List<CanvasNode> _children = new();

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CanvasNode), true);
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public IReadOnlyList<CanvasNode> Children => _children;

        public ICanvasNodeParent? Parent { get; internal set; }

        public RectF Bounds { get; private set; }

        protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            //System.Diagnostics.Debug.WriteLine($"OnPropertyChanged(propertyName={propertyName})");
            RequestInvalidate();
            base.OnPropertyChanged(propertyName);
        }

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
            Bounds = context.DirtyRect;

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
                _invalidateRequested = true;
                Parent?.RequestInvalidate();
            }
        }
    }



    public static class CanvasNodeNativeExtentsions
    {
        public static Rect BoundsToScreenSize(this CanvasNode canvasNode)
        {
            if (canvasNode.Parent is VisualElement parentVisualElement)
            {
                var parentFrameToScreenSize = parentVisualElement.BoundsToScreenSize();
                return new Rect(parentFrameToScreenSize.Left + canvasNode.Bounds.Left, parentFrameToScreenSize.Top + canvasNode.Bounds.Top, canvasNode.Bounds.Width, canvasNode.Bounds.Height);
            }
            else if (canvasNode.Parent is CanvasNode parentCanvasNode)
            {
                var parentFrameToScreenSize = parentCanvasNode.BoundsToScreenSize();
                return new Rect(parentFrameToScreenSize.Left + canvasNode.Bounds.Left, parentFrameToScreenSize.Top + canvasNode.Bounds.Top, canvasNode.Bounds.Width, canvasNode.Bounds.Height);
            }

            return canvasNode.Bounds;
        }

        public static Rect BoundsToCanvasView(this CanvasNode canvasNode)
        {
            if (canvasNode.Parent is CanvasNode parentCanvasNode)
            {
                var parentFrameToScreenSize = parentCanvasNode.BoundsToScreenSize();
                return new Rect(parentFrameToScreenSize.Left + canvasNode.Bounds.Left, parentFrameToScreenSize.Top + canvasNode.Bounds.Top, canvasNode.Bounds.Width, canvasNode.Bounds.Height);
            }

            return canvasNode.Bounds;
        }
    }

}