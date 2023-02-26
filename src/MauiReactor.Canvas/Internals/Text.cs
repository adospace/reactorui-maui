using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace MauiReactor.Canvas.Internals
{
    public class TextMeasureEventArgs
    {
        public TextMeasureEventArgs(SizeF size)
        {
            Size = size;
        }

        public SizeF Size { get; }
    }

    public class Text : CanvasVisualElement
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(string), typeof(Text), null);

        public string? Value
        {
            get => (string?)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty VerticalAlignmentProperty = BindableProperty.Create(nameof(VerticalAlignment), typeof(VerticalAlignment), typeof(Text), VerticalAlignment.Top);

        public VerticalAlignment VerticalAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }

        public static readonly BindableProperty HorizontalAlignmentProperty = BindableProperty.Create(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(Text), Microsoft.Maui.Graphics.HorizontalAlignment.Left);

        public HorizontalAlignment HorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(Text), 12.0f,
            coerceValue: (bindableObject, value) => ((float)value) <= 0.0f ? 12.0f : (float)value);

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty FontColorProperty = BindableProperty.Create(nameof(FontColor), typeof(Color), typeof(Text), null);

        public Color? FontColor
        {
            get => (Color?)GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }

        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(Text), null);

        public string? FontName
        {
            get => (string?)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        public static readonly BindableProperty FontWeightProperty = BindableProperty.Create(nameof(FontWeight), typeof(int), typeof(Text), FontWeights.Normal);

        public int FontWeight
        {
            get => (int)GetValue(FontWeightProperty);
            set => SetValue(FontWeightProperty, value);
        }

        public static readonly BindableProperty FontStyleProperty = BindableProperty.Create(nameof(FontStyle), typeof(FontStyleType), typeof(Text), FontStyleType.Normal);

        public FontStyleType FontStyle
        {
            get => (FontStyleType)GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        public static readonly BindableProperty FlowProperty = BindableProperty.Create(nameof(Flow), typeof(TextFlow), typeof(Text), TextFlow.ClipBounds);

        public TextFlow Flow
        {
            get => (TextFlow)GetValue(FlowProperty);
            set => SetValue(FlowProperty, value);
        }

        public event EventHandler<TextMeasureEventArgs>? Measure;

        protected override void OnDraw(DrawingContext context)
        {
            if (!string.IsNullOrWhiteSpace(Value))
            {
                var canvas = context.Canvas;
                var dirtyRect = context.DirtyRect;

                canvas.SaveState();

                canvas.FontSize = FontSize;

                if (FontColor != null)
                {
                    canvas.FontColor = FontColor;
                }

                var font = Font.Default;
                if (FontName != null || FontWeight != FontWeights.Normal || FontStyle != FontStyleType.Normal)
                {
                    font = new Font(FontName, FontWeight, FontStyle);
                }

                canvas.Font = font;

                if (Measure != null)
                {
                    var measure = canvas.GetStringSize(Value, font, FontSize, HorizontalAlignment, VerticalAlignment);
                    Measure(this, new TextMeasureEventArgs(measure));
                }

                canvas.DrawString(Value, dirtyRect, HorizontalAlignment, VerticalAlignment, Flow);

                canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }

}