using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.Internals.GIFBitmap;

namespace MauiReactor.Canvas.Internals
{
    public class ClipRectangle : CanvasNode
    {
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadiusF), typeof(ClipRectangle), new CornerRadiusF());

        public CornerRadiusF CornerRadius
        {
            get => (CornerRadiusF)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public CanvasNode? Child { get; set; }

        protected override void OnDraw(DrawingContext context)
        {
            if (Child != null)
            {
                var rect = context.DirtyRect;
                context.Canvas.SaveState();

                if (!CornerRadius.IsZero)
                {
                    var cornerRadius = CornerRadius;
                    var path = new PathF();
                    path.AppendRoundedRectangle(rect.X, rect.Y, rect.Width, rect.Height, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomLeft, cornerRadius.BottomRight);
                    context.Canvas.ClipPath(path);
                }
                else
                {
                    context.Canvas.ClipRectangle(rect);
                }

                Child.Draw(context);
                context.Canvas.RestoreState();
            }

            base.OnDraw(context);
        }
    }
}
