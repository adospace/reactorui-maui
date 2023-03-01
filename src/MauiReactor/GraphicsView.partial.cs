using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public class DrawableContext
    {
        public DrawableContext(ICanvas canvas, RectF dirtyRect)
        {
            Canvas = canvas;
            DirtyRect = dirtyRect;
        }

        public ICanvas Canvas { get; }
        public RectF DirtyRect { get; }
    }

    public partial interface IGraphicsView
    {
        Action<ICanvas, RectF>? DrawAction { get; set; }
    }

    internal class DrawActionWrapper : IDrawable
    {
        private readonly Action<ICanvas, RectF> _drawAction;

        public DrawActionWrapper(Action<ICanvas, RectF> drawAction)
        {
            _drawAction = drawAction;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _drawAction.Invoke(canvas, dirtyRect);
        }
    }

    public partial class GraphicsView<T>
    {
        Action<ICanvas, RectF>? IGraphicsView.DrawAction { get; set; }



        partial void OnEndUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIGraphicsView = (IGraphicsView)this;

            if (thisAsIGraphicsView.DrawAction != null)
            {
                NativeControl.SetPropertyValue(Microsoft.Maui.Controls.GraphicsView.DrawableProperty, new DrawActionWrapper(thisAsIGraphicsView.DrawAction));
            }
            else if (NativeControl.Drawable is DrawActionWrapper)
            {
                NativeControl.SetPropertyValue(Microsoft.Maui.Controls.GraphicsView.DrawableProperty, null);
            }

            NativeControl.Invalidate();
        }
    }

    public static partial class GraphicsViewExtensions
    {
        public static T OnDraw<T>(this T graphicsView, Action<ICanvas, RectF>? drawAction) where T : IGraphicsView
        {
            graphicsView.DrawAction = drawAction;
            return graphicsView;
        }

    }
}
