using Microsoft.Maui.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using System;
using System.Collections.Generic;

namespace MauiReactor.SkiaCanvas.Internals
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
        internal DrawingContext(SKSurface surface, SKSizeI size) 
        {
            Surface = surface;
            Size = size;
        }

        public SKSurface Surface { get; }
        public SKSizeI Size { get; }

        public List<IDragInteractionHandler> DragInteractionHandlers { get; } = new();

        public List<IHoverInteractionHandler> HoverInteractionHandlers { get; } = new();        
    }
}