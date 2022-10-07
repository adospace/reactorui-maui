using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Numerics;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasNode : BindableObject
    {

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CanvasNode), true);

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

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

        }
    }

}