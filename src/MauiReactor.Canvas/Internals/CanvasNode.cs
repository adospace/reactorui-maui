using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Numerics;

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

        public static readonly BindableProperty TranslationProperty = BindableProperty.Create(nameof(Translation), typeof(Vector2), typeof(CanvasNode), Vector2.Zero);

        public Vector2 Translation
        {
            get => (Vector2)GetValue(TranslationProperty);
            set => SetValue(TranslationProperty, value);
        }

        public static readonly BindableProperty ScaleProperty = BindableProperty.Create(nameof(Scale), typeof(Vector2), typeof(CanvasNode), Vector2.One);

        public Vector2 Scale
        {
            get => (Vector2)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public static readonly BindableProperty RotationProperty = BindableProperty.Create(nameof(Rotation), typeof(float), typeof(CanvasNode), 0.0f);

        public float Rotation
        {
            get => (float)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
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
                context.DirtyRect = new RectF(
                    context.DirtyRect.X + (float)Margin.Left,
                    context.DirtyRect.Y + (float)Margin.Top,
                    context.DirtyRect.Width - (float)(Margin.Left + Margin.Right),
                    context.DirtyRect.Height - (float)(Margin.Top + Margin.Bottom)
                );
            }

            var restoreState = false;
            try
            {
                if (Translation != Vector2.Zero)
                {
                    restoreState = true;
                    context.Canvas.SaveState();

                    context.Canvas.Translate(Translation.X, Translation.Y);
                }

                if (Scale != Vector2.One)
                {
                    if(!restoreState)
                    {
                        restoreState = true;
                        context.Canvas.SaveState();
                    }

                    context.Canvas.Scale(Scale.X, Scale.Y);
                }

                if (Rotation != 0.0f)
                {
                    if (!restoreState)
                    {
                        restoreState = true;
                        context.Canvas.SaveState();
                    }

                    context.Canvas.Rotate(Rotation);
                }

                OnDraw(context);
            }
            finally
            {
                if (restoreState)
                {
                    context.Canvas.ResetState(); 
                }
            }


        }

        protected virtual void OnDraw(DrawingContext context)
        {

        }
    }

}