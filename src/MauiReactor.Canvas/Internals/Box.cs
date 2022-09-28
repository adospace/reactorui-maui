using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Box : CanvasNode
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

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(Box), 0.0f,
            coerceValue: (BindableObject bindable, object value) => Math.Max((float)value, 0.0f));

        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty BorderSizeProperty = BindableProperty.Create(nameof(BorderSize), typeof(float), typeof(Box), 1.0f,
            coerceValue: (BindableObject bindable, object value) => Math.Max((float)value, 0.0f));

        public float BorderSize
        {
            get => (float)GetValue(BorderSizeProperty);
            set => SetValue(BorderSizeProperty, value);
        }

        public CanvasNode? Child { get; set; }

        protected override void OnDraw(DrawingContext context)
        {
            var canvas = context.Canvas;
            var dirtyRect = context.DirtyRect;

            var fillColor = BackgroundColor;
            var strokeColor = BorderColor;
            var corderRadius = CornerRadius;
            var borderSize = BorderSize;

            canvas.SaveState();

            if (corderRadius > 0.0f)
            {
                if (fillColor != null)
                {
                    canvas.FillColor = fillColor;
                    canvas.FillRoundedRectangle(dirtyRect, corderRadius);
                }

                Child?.Draw(context);

                if (strokeColor != null)
                {
                    canvas.StrokeColor = strokeColor;
                    canvas.StrokeSize = borderSize;
                    canvas.DrawRoundedRectangle(dirtyRect, corderRadius);
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