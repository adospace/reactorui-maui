using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Align : CanvasNode
    {
        public static readonly BindableProperty WidthProperty = BindableProperty.Create(nameof(Width), typeof(float), typeof(Align), float.NaN,
            coerceValue: (BindableObject bindable, object value) => float.IsNaN((float)value) ? float.NaN : Math.Max(((float)value), 0.0f));

        public float Width
        {
            get => (float)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public static readonly BindableProperty HeightProperty = BindableProperty.Create(nameof(Height), typeof(float), typeof(Align), float.NaN,
            coerceValue: (BindableObject bindable, object value) => float.IsNaN((float)value) ? float.NaN : Math.Max(((float)value), 0.0f));

        public float Height
        {
            get => (float)GetValue(HeightProperty);
            set => SetValue(HeightProperty, value);
        }

        public static readonly BindableProperty HorizontalAlignmentProperty = BindableProperty.Create(nameof(HorizontalAlignment), typeof(Microsoft.Maui.Primitives.LayoutAlignment),  typeof(Align), Microsoft.Maui.Primitives.LayoutAlignment.Fill);

        public Microsoft.Maui.Primitives.LayoutAlignment HorizontalAlignment
        {
            get => (Microsoft.Maui.Primitives.LayoutAlignment)GetValue(HorizontalAlignmentProperty);
            set => SetValue(HorizontalAlignmentProperty, value);
        }

        public static readonly BindableProperty VerticalAlignmentProperty = BindableProperty.Create(nameof(VerticalAlignment), typeof(Microsoft.Maui.Primitives.LayoutAlignment), typeof(Align), Microsoft.Maui.Primitives.LayoutAlignment.Fill);

        public Microsoft.Maui.Primitives.LayoutAlignment VerticalAlignment
        {
            get => (Microsoft.Maui.Primitives.LayoutAlignment)GetValue(VerticalAlignmentProperty);
            set => SetValue(VerticalAlignmentProperty, value);
        }

        public CanvasNode? Child { get; set; }

        protected override void OnDraw(DrawingContext context)
        {
            if (Child != null)
            {
                if (HorizontalAlignment != Microsoft.Maui.Primitives.LayoutAlignment.Fill &&
                    !float.IsNaN(Width))
                {
                    switch (HorizontalAlignment)
                    {
                        case Microsoft.Maui.Primitives.LayoutAlignment.Start:
                            context.DirtyRect = new RectF(
                                context.DirtyRect.X,
                                context.DirtyRect.Y,
                                Width,
                                context.DirtyRect.Height);
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.Center:
                            context.DirtyRect = new RectF(
                                context.DirtyRect.X + (context.DirtyRect.Width - Width) / 2.0f,
                                context.DirtyRect.Y,
                                Width,
                                context.DirtyRect.Height);
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.End:
                            context.DirtyRect = new RectF(
                                context.DirtyRect.X + (context.DirtyRect.Width - Width),
                                context.DirtyRect.Y,
                                Width,
                                context.DirtyRect.Height);
                            break;
                    }
                }

                if (VerticalAlignment != Microsoft.Maui.Primitives.LayoutAlignment.Fill &&
                    !float.IsNaN(Height))
                {
                    switch (VerticalAlignment)
                    {
                        case Microsoft.Maui.Primitives.LayoutAlignment.Start:
                            context.DirtyRect = new RectF(
                                context.DirtyRect.X,
                                context.DirtyRect.Y,
                                context.DirtyRect.Width,
                                Height);
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.Center:
                            context.DirtyRect = new RectF(
                                context.DirtyRect.X,
                                context.DirtyRect.Y + (context.DirtyRect.Height - Height) / 2.0f,
                                context.DirtyRect.Width,
                                Height);
                            break;
                        case Microsoft.Maui.Primitives.LayoutAlignment.End:
                            context.DirtyRect = new RectF(
                                context.DirtyRect.X,
                                context.DirtyRect.Y + (context.DirtyRect.Height - Height),
                                context.DirtyRect.Width,
                                Height);
                            break;
                    }
                }


                Child.Draw(context);
            }

            base.OnDraw(context);
        }
    }

}