using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class DropShadow : CanvasNode
    {
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(SizeF), typeof(DropShadow), new SizeF());

        public SizeF Size
        {
            get => (SizeF)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public static readonly BindableProperty BlurProperty = BindableProperty.Create(nameof(Blur), typeof(float), typeof(DropShadow), 0.0f);

        public float Blur
        {
            get => (float)GetValue(BlurProperty);
            set => SetValue(BlurProperty, value);
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(DropShadow), Colors.Black);

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public CanvasNode? Child => Children.Count > 0 ? Children[0] : null;

        protected override void OnDraw(DrawingContext context)
        {
            var canvas = context.Canvas;

            if (Child != null)
            {
                canvas.SaveState();

                canvas.SetShadow(Size, Blur, Color);

                Child.Draw(context);

                canvas.RestoreState();
            }

            base.OnDraw(context);
        }

    }
}
