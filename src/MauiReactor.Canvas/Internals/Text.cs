using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Text : CanvasNode
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

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSizeProperty), typeof(float), typeof(Text), 12.0f,
            coerceValue: (bindableObject, value) => ((float)value) <= 0.0f ? 12.0f : (float)value);

        public float FontSize
        {
            get => (float)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty FontColorProperty = BindableProperty.Create(nameof(FontColorProperty), typeof(Color), typeof(Text), null);

        public Color? FontColor
        {
            get => (Color?)GetValue(FontColorProperty);
            set => SetValue(FontColorProperty, value);
        }

        public static readonly BindableProperty FontProperty = BindableProperty.Create(nameof(FontProperty), typeof(IFont), typeof(Text), Microsoft.Maui.Graphics.Font.Default);

        public IFont? Font
        {
            get => (IFont?)GetValue(FontProperty);
            set => SetValue(FontProperty, value);
        }

        protected override void OnDraw(DrawingContext context)
        {
            if (Value != null)
            {
                var canvas = context.Canvas;
                var dirtyRect = context.DirtyRect;

                canvas.SaveState();

                canvas.FontSize = FontSize;
                if (FontColor != null)
                {
                    canvas.FontColor = FontColor;
                }
                if (Font != null)
                {
                    canvas.Font = Font;
                }

                canvas.DrawString(Value, dirtyRect, HorizontalAlignment, VerticalAlignment);

                canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }

}