using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.ComponentModel.DataAnnotations;
using Microsoft.Maui.Graphics;
using System.Net;
using Microsoft.Maui.Primitives;
using System.Threading;
using System.Collections;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasView : Microsoft.Maui.Controls.GraphicsView, INodeContainer, ICanvasNodeParent
    {
        private IReadOnlyList<IDragInteractionHandler>? _dragInteractionHandlers;
        private IReadOnlyList<IHoverInteractionHandler>? _hoverInteractionHandlers;

        private IDragInteractionHandler? _currentDragHandler;
        private IHoverInteractionHandler? _currentHoverHandler;

        private class CanvasViewDrawable : IDrawable
        {
            private readonly CanvasView _owner;

            public CanvasViewDrawable(CanvasView owner)
            {
                _owner = owner;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                _owner.OnDraw(canvas, dirtyRect);
            }
        }

        public CanvasView()
        {
            Drawable = new CanvasViewDrawable(this);

            this.StartHoverInteraction += CanvasView_StartHoverInteraction;
            this.MoveHoverInteraction += CanvasView_MoveHoverInteraction;
            this.StartInteraction += CanvasView_StartInteraction;
            this.EndHoverInteraction += CanvasView_EndHoverInteraction;
            this.EndInteraction += CanvasView_EndInteraction;
            this.DragInteraction += CanvasView_DragInteraction;
            this.CancelInteraction += CanvasView_CancelInteraction;
        }

        private void CanvasView_StartInteraction(object? sender, TouchEventArgs e)
        {
            if (_dragInteractionHandlers != null)
            {
                foreach (var handler in _dragInteractionHandlers)
                {
                    if (handler.HitTest(e.Touches))
                    {
                        _currentDragHandler = handler;
                        _currentDragHandler.StartDrag(e.Touches);
                        break;
                    }
                }
            }
        }

        private void CanvasView_DragInteraction(object? sender, TouchEventArgs e)
        {
            _currentDragHandler?.Drag(e.Touches);
        }

        private void CanvasView_EndInteraction(object? sender, TouchEventArgs e)
        {
            _currentDragHandler?.StopDrag(e.Touches);
        }

        private void CanvasView_CancelInteraction(object? sender, EventArgs e)
        {
            _currentDragHandler?.CancelDrag();
            _currentHoverHandler?.CancelHover();
        }

        private void CanvasView_StartHoverInteraction(object? sender, TouchEventArgs e)
        {
            if (_hoverInteractionHandlers != null)
            {
                foreach (var handler in _hoverInteractionHandlers)
                {
                    if (handler.HitTest(e.Touches))
                    {
                        _currentHoverHandler = handler;
                        _currentHoverHandler.StartHover(e.Touches);
                        break;
                    }
                }
            }
        }

        private void CanvasView_MoveHoverInteraction(object? sender, TouchEventArgs e)
        {
            _currentHoverHandler?.MoveHover(e.Touches);
        }

        private void CanvasView_EndHoverInteraction(object? sender, EventArgs e)
        {
            _currentHoverHandler?.CancelHover();
        }

        private readonly List<CanvasNode> _children = new();

        public IReadOnlyList<CanvasNode> Children => _children;

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

        private void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            var context = new DrawingContext(canvas, dirtyRect);
            foreach (var child in Children)
            {
                child.Draw(context);
            }

            _dragInteractionHandlers = context.DragInteractionHandlers;
            _hoverInteractionHandlers = context.HoverInteractionHandlers;
        }

        public void RequestInvalidate()
        {
            Invalidate();
            
        }
    }
}