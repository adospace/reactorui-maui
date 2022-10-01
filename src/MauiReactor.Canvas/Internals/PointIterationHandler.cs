﻿using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public class PointIterationHandler : CanvasNode, IDragInteractionHandler
    {
        private RectF _hitRect;

        public event EventHandler? Tap;
        public CanvasNode? Child { get; set; }

        public void CancelDrag()
        {
        }

        public void Drag(PointF[] touchPoints)
        {
        }

        public bool HitTest(PointF[] touchPoints)
        {
            return touchPoints.Any(_ => _hitRect.Contains(_));
        }

        public void StartDrag(PointF[] touchPoints)
        {
            Tap?.Invoke(this, EventArgs.Empty);
        }

        public void StopDrag(PointF[] touchPoints)
        {
        }

        protected override void OnDraw(DrawingContext context)
        {
            _hitRect = context.DirtyRect;

            Child?.Draw(context);

            context.DragInteractionHandlers.Add(this);

            base.OnDraw(context);
        }
    }
}