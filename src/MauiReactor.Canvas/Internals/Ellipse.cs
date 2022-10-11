using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas.Internals
{
    public class Ellipse : CanvasVisualElement
    {
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(Ellipse), null);

        public Color? FillColor
        {
            get => (Color?)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(Ellipse), null);

        public Color? StrokeColor
        {
            get => (Color?)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        public static readonly BindableProperty StrokeSizeProperty = BindableProperty.Create(nameof(StrokeSize), typeof(float), typeof(Ellipse), 1.0f,
            coerceValue: (BindableObject bindable, object value) => Math.Max((float)value, 0.0f));

        public float StrokeSize
        {
            get => (float)GetValue(StrokeSizeProperty);
            set => SetValue(StrokeSizeProperty, value);
        }

        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        protected override void OnDraw(DrawingContext context)
        {
            context.Canvas.SaveState();

            if (FillColor != null)
            {
                context.Canvas.FillColor = FillColor;
                context.Canvas.FillEllipse(context.DirtyRect);
            }
            if (StrokeColor != null)
            {
                context.Canvas.StrokeColor = StrokeColor;
                context.Canvas.StrokeSize = StrokeSize;
                context.Canvas.DrawEllipse(context.DirtyRect);
            }

            context.Canvas.RestoreState();

            base.OnDraw(context);
        }

    }
}
