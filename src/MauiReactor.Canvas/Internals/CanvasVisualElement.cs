using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasVisualElement : CanvasNode
    {
        public static readonly BindableProperty MarginProperty = BindableProperty.Create(nameof(Margin), typeof(ThicknessF), typeof(CanvasVisualElement), new ThicknessF());

        public ThicknessF Margin
        {
            get => (ThicknessF)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }

        public static readonly BindableProperty TranslationProperty = BindableProperty.Create(nameof(Translation), typeof(Vector2), typeof(CanvasVisualElement), Vector2.Zero);

        public Vector2 Translation
        {
            get => (Vector2)GetValue(TranslationProperty);
            set => SetValue(TranslationProperty, value);
        }

        public static readonly BindableProperty ScaleProperty = BindableProperty.Create(nameof(Scale), typeof(Vector2), typeof(CanvasVisualElement), Vector2.One);

        public Vector2 Scale
        {
            get => (Vector2)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        public static readonly BindableProperty RotationProperty = BindableProperty.Create(nameof(Rotation), typeof(float), typeof(CanvasVisualElement), 0.0f);

        public float Rotation
        {
            get => (float)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }


        protected override sealed void DrawOverride(DrawingContext context)
        {
            var margin = Margin;
            if (!margin.IsEmpty)
            {
                context.DirtyRect = new RectF(
                    context.DirtyRect.X + (float)margin.Left,
                    context.DirtyRect.Y + (float)margin.Top,
                    context.DirtyRect.Width - (float)(margin.Left + margin.Right),
                    context.DirtyRect.Height - (float)(margin.Top + margin.Bottom)
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
                    if (!restoreState)
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
    }
}
