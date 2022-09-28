using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public record DrawingContext(ICanvas Canvas, RectF DirtyRect);

}