using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Box : CanvasVisualElement
    {
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

        public CanvasVisualElement? Child { get; set; }

        protected override void OnDraw(DrawingContext context)
        {
            var canvas = context.Canvas;
            var dirtyRect = context.DirtyRect;

            var fillColor = BackgroundColor;
            var strokeColor = BorderColor;
            var cornerRadius = CornerRadius;
            var borderSize = BorderSize;

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

            base.OnDraw(context);
        }
    }

}