using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Canvas.Internals
{
    public class Picture : CanvasVisualElement
    {
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(Microsoft.Maui.Graphics.IImage), typeof(Picture), null);

        public Microsoft.Maui.Graphics.IImage? Source
        {
            get => (Microsoft.Maui.Graphics.IImage?)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        
        public static readonly BindableProperty AspectProperty = BindableProperty.Create(nameof(Source), typeof(Aspect), typeof(Picture), Aspect.AspectFit);

        public Aspect Aspect
        {
            get => (Aspect)GetValue(AspectProperty);
            set => SetValue(AspectProperty, value);
        }

        protected override void OnDraw(DrawingContext context)
        {
            if (Source != null)
            {
                var canvas = context.Canvas;
                var dirtyRect = context.DirtyRect;

                canvas.SaveState();

                if (Aspect == Aspect.Fill)
                {
                    canvas.DrawImage(Source, dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
                }
                else
                {
                    float scale = 1;

                    switch (Aspect)
                    {
                        case Aspect.AspectFit:
                            scale = Math.Min(dirtyRect.Width / Source.Width, dirtyRect.Height / Source.Height);
                            break;

                        case Aspect.AspectFill:
                            scale = Math.Max(dirtyRect.Width / Source.Width, dirtyRect.Height / Source.Height);
                            break;
                    }

                    SizeF imageResultingSize = new SizeF(Source.Width * scale, Source.Height * scale);

                    RectF display = new RectF(
                        (dirtyRect.Width - imageResultingSize.Width) / 2, 
                        (dirtyRect.Height - imageResultingSize.Height) / 2, 
                        imageResultingSize.Width, 
                        imageResultingSize.Height);

                    canvas.ClipRectangle(dirtyRect);
                    canvas.DrawImage(Source, dirtyRect.X + display.X, dirtyRect.Y + display.Y, display.Width, display.Height);

                }


                canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }

}