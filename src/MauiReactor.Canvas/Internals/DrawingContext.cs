using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;

namespace MauiReactor.Canvas.Internals
{
    public interface IDragInteractionHandler
    {
        bool HitTest(PointF[] touchPoints);

        void StartDrag(PointF[] touchPoints);

        void Drag(PointF[] touchPoints);

        void StopDrag(PointF[] touchPoints);

        void CancelDrag();
    }

    public interface IHoverInteractionHandler
    {
        bool HitTest(PointF[] touchPoints);

        void StartHover(PointF[] touchPoints);

        void MoveHover(PointF[] touchPoints);

        void CancelHover();
    }


    public class DrawingContext
    {
        internal DrawingContext(ICanvas canvas, RectF initialDirtyRect) 
        { 
            Canvas = canvas;
            DirtyRect = initialDirtyRect;
        }

        public ICanvas Canvas { get; }

        public RectF DirtyRect { get; set; }

        public List<IDragInteractionHandler> DragInteractionHandlers { get; } = new();

        public List<IHoverInteractionHandler> HoverInteractionHandlers { get; } = new();

        
    };

}