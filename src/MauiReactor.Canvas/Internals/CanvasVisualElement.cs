using Microsoft.Maui;
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
        public static readonly BindableProperty MarginProperty = BindableProperty.Create(nameof(Margin), typeof(ThicknessF), typeof(CanvasVisualElement), ThicknessF.Zero);

        public ThicknessF Margin
        {
            get => (ThicknessF)GetValue(MarginProperty);
            set => SetValue(MarginProperty, value);
        }

        public static readonly BindableProperty TranslationXProperty = BindableProperty.Create(nameof(TranslationX), typeof(float), typeof(CanvasVisualElement), 0.0f);

        public float TranslationX
        {
            get => (float)GetValue(TranslationXProperty);
            set => SetValue(TranslationXProperty, value);
        }

        public static readonly BindableProperty TranslationYProperty = BindableProperty.Create(nameof(TranslationY), typeof(float), typeof(CanvasVisualElement), 0.0f);

        public float TranslationY
        {
            get => (float)GetValue(TranslationYProperty);
            set => SetValue(TranslationYProperty, value);
        }

        public static readonly BindableProperty ScaleXProperty = BindableProperty.Create(nameof(ScaleX), typeof(float), typeof(CanvasVisualElement), 1.0f);

        public float ScaleX
        {
            get => (float)GetValue(ScaleXProperty);
            set => SetValue(ScaleXProperty, value);
        }

        public static readonly BindableProperty ScaleYProperty = BindableProperty.Create(nameof(ScaleY), typeof(float), typeof(CanvasVisualElement), 1.0f);

        public float ScaleY
        {
            get => (float)GetValue(ScaleYProperty);
            set => SetValue(ScaleYProperty, value);
        }

        public static readonly BindableProperty AnchorXProperty = BindableProperty.Create(nameof(AnchorX), typeof(float), typeof(CanvasVisualElement), 0.0f);

        public float AnchorX
        {
            get => (float)GetValue(AnchorXProperty);
            set => SetValue(AnchorXProperty, value);
        }

        public static readonly BindableProperty AnchorYProperty = BindableProperty.Create(nameof(AnchorY), typeof(float), typeof(CanvasVisualElement), 0.0f);

        public float AnchorY
        {
            get => (float)GetValue(AnchorYProperty);
            set => SetValue(AnchorYProperty, value);
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
            var oldRect = context.DirtyRect;
            if (!margin.IsEmpty)
            {
                context.DirtyRect = new RectF(
                    context.DirtyRect.X + margin.Left,
                    context.DirtyRect.Y + margin.Top,
                    context.DirtyRect.Width - (margin.Left + margin.Right),
                    context.DirtyRect.Height - (margin.Top + margin.Bottom)
                );
            }

            var restoreState = false;
            try
            {
                if (TranslationX != 0.0f || TranslationY != 0.0f)
                {
                    restoreState = true;
                    context.Canvas.SaveState();

                    context.Canvas.Translate(TranslationX, TranslationY);
                }

                if (ScaleX != 1.0f || ScaleY != 1.0f)
                {
                    if (!restoreState)
                    {
                        restoreState = true;
                        context.Canvas.SaveState();
                    }

                    context.Canvas.Translate(context.DirtyRect.Left + AnchorX * context.DirtyRect.Width, context.DirtyRect.Top + AnchorY * context.DirtyRect.Height);
                    context.Canvas.Scale(ScaleX, ScaleY);
                    context.Canvas.Translate(-(context.DirtyRect.Left + AnchorX * context.DirtyRect.Width), -(context.DirtyRect.Top + AnchorY * context.DirtyRect.Height));

                }

                if (Rotation != 0.0f)
                {
                    if (!restoreState)
                    {
                        restoreState = true;
                        context.Canvas.SaveState();
                    }

                    context.Canvas.Rotate(Rotation, (context.DirtyRect.Left + AnchorX * context.DirtyRect.Width), (context.DirtyRect.Top + AnchorY * context.DirtyRect.Height));
                }

                OnDraw(context);
            }
            finally
            {
                if (restoreState)
                {
                    context.Canvas.RestoreState();
                }
            }

            context.DirtyRect = oldRect;
        }
    }
}
