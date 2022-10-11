using System;
using System.Linq;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Box : CanvasVisualElement
    {
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(ThicknessF), typeof(Box), new ThicknessF());

        public ThicknessF Padding
        {
            get => (ThicknessF)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Box), null);

        public Color? BackgroundColor
        {
            get => (Color?)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Box), null);

        public Color? BorderColor
        {
            get => (Color?)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadiusF), typeof(Box), new CornerRadiusF());

        public CornerRadiusF CornerRadius
        {
            get => (CornerRadiusF)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty BorderSizeProperty = BindableProperty.Create(nameof(BorderSize), typeof(float), typeof(Box), 1.0f,
            coerceValue: (BindableObject bindable, object value) => Math.Max((float)value, 0.0f));

        public float BorderSize
        {
            get => (float)GetValue(BorderSizeProperty);
            set => SetValue(BorderSizeProperty, value);
        }

        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        protected override void OnDraw(DrawingContext context)
        {
            var canvas = context.Canvas;
            var dirtyRect = context.DirtyRect;

            var fillColor = BackgroundColor;
            var strokeColor = BorderColor;
            var cornerRadius = CornerRadius;
            var borderSize = BorderSize;

            var padding = Padding;
            var oldRect = context.DirtyRect;
            if (!padding.IsEmpty)
            {
                context.DirtyRect = new RectF(
                    context.DirtyRect.X + (float)padding.Left,
                    context.DirtyRect.Y + (float)padding.Top,
                    context.DirtyRect.Width - (float)(padding.Left + padding.Right),
                    context.DirtyRect.Height - (float)(padding.Top + padding.Bottom)
                );
            }

            canvas.SaveState();

            if (!cornerRadius.IsZero)
            {
                if (fillColor != null)
                {
                    canvas.FillColor = fillColor; 
                    
                    if (cornerRadius.UniformSize())
                    {
                        canvas.FillRoundedRectangle(dirtyRect, cornerRadius.TopLeft);
                    }
                    else
                    {
                        canvas.FillRoundedRectangle(dirtyRect, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomLeft, cornerRadius.BottomRight);
                    }                    
                }

                Child?.Draw(context);

                if (strokeColor != null)
                {
                    canvas.StrokeColor = strokeColor;
                    canvas.StrokeSize = borderSize;
                    if (cornerRadius.UniformSize())
                    {
                        canvas.DrawRoundedRectangle(dirtyRect, cornerRadius.TopLeft);
                    }
                    else
                    {
                        canvas.DrawRoundedRectangle(dirtyRect, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomLeft, cornerRadius.BottomRight);
                    }
                }
            }
            else
            {
                if (fillColor != null)
                {
                    canvas.FillColor = fillColor;
                    canvas.FillRectangle(dirtyRect);
                }

                Child?.Draw(context);

                if (strokeColor != null)
                {
                    canvas.StrokeColor = strokeColor;
                    canvas.StrokeSize = borderSize;
                    canvas.DrawRectangle(dirtyRect);
                }
            }

            canvas.RestoreState();

            context.DirtyRect = oldRect;

            base.OnDraw(context);
        }
    }

}