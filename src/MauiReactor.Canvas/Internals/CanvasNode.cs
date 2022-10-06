using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Numerics;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasNode : BindableObject
    {
        //internal SizeF Measure(SizeF containerSize)
        //{
        //    var marginSize = new SizeF(Margin.Left + Margin.Right, Margin.Top + Margin.Bottom);
        //    var desideredSize = MeasureOverride(containerSize + marginSize);

        //    return desideredSize + marginSize;
        //}

        //protected virtual SizeF MeasureOverride(SizeF containerSize)
        //{
        //    return new SizeF();
        //}

        public void Draw(DrawingContext context)
        {
            DrawOverride(context);
        }

        protected virtual void DrawOverride(DrawingContext context)
        {
            OnDraw(context);
        }

        protected virtual void OnDraw(DrawingContext context)
        {

        }
    }

}