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

        public static readonly BindableProperty StrokeLineCapProperty = BindableProperty.Create(nameof(StrokeLineCap), typeof(LineCap), typeof(Ellipse), default(LineCap));

        public LineCap StrokeLineCap
        {
            get => (LineCap)GetValue(StrokeLineCapProperty);
            set => SetValue(StrokeLineCapProperty, value);
        }

        public static readonly BindableProperty StrokeDashPatternProperty = BindableProperty.Create(nameof(StrokeDashPattern), typeof(float[]), typeof(Ellipse), null);

        public float[]? StrokeDashPattern
        {
            get => (float[]?)GetValue(StrokeDashPatternProperty);
            set => SetValue(StrokeDashPatternProperty, value);
        }

        public static readonly BindableProperty StrokeDashOffsetProperty = BindableProperty.Create(nameof(StrokeDashOffset), typeof(float), typeof(Ellipse), default(float));

        public float StrokeDashOffset
        {
            get => (float)GetValue(StrokeDashOffsetProperty);
            set => SetValue(StrokeDashOffsetProperty, value);
        }

        public static readonly BindableProperty StrokeLineJoinProperty = BindableProperty.Create(nameof(StrokeLineJoin), typeof(LineJoin), typeof(Ellipse), default(LineJoin));

        public LineJoin StrokeLineJoin
        {
            get => (LineJoin)GetValue(StrokeLineJoinProperty);
            set => SetValue(StrokeLineJoinProperty, value);
        }

        public static readonly BindableProperty WindingProperty = BindableProperty.Create(nameof(Winding), typeof(WindingMode), typeof(Ellipse), null);

        public WindingMode Winding
        {
            get => (WindingMode)GetValue(WindingProperty);
            set => SetValue(WindingProperty, value);
        }

        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        protected override void OnDraw(DrawingContext context)
        {
            context.Canvas.SaveState();

            if (StrokeLineCap != default)
            {
                context.Canvas.StrokeLineCap = StrokeLineCap;
            }
            if (StrokeLineJoin != default)
            {
                context.Canvas.StrokeLineJoin = StrokeLineJoin;
            }
            if (StrokeDashPattern != null)
            {
                context.Canvas.StrokeDashPattern = StrokeDashPattern;
            }
            if (StrokeDashOffset > 0)
            {
                context.Canvas.StrokeDashOffset = StrokeDashOffset;
            }

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
