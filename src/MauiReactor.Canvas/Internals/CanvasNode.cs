using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasNode : BindableObject
    {
        public static readonly BindableProperty MarginProperty = BindableProperty.Create(nameof(Margin), typeof(ThicknessF), typeof(CanvasNode), new ThicknessF());

        public ThicknessF Margin
        {
            get => (ThicknessF)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
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
            if (!Margin.IsEmpty)
            {
                OnDraw(context with 
                { 
                    DirtyRect = new RectF(
                        context.DirtyRect.X + (float)Margin.Left,
                        context.DirtyRect.Y + (float)Margin.Top,
                        context.DirtyRect.Width - (float)(Margin.Left + Margin.Right),
                        context.DirtyRect.Height - (float)(Margin.Top + Margin.Bottom)
                    )
                });
            }
            else
            {
                OnDraw(context);
            }
        }

        protected virtual void OnDraw(DrawingContext context)
        {

        }
    }

}