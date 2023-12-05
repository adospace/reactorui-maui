using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public class DrawableContext(ICanvas canvas, RectF dirtyRect)
    {
        public ICanvas Canvas { get; } = canvas;
        public RectF DirtyRect { get; } = dirtyRect;
    }

    public partial interface IGraphicsView
    {
        Action<ICanvas, RectF>? DrawAction { get; set; }
    }

    internal class DrawActionWrapper(Action<ICanvas, RectF> drawAction) : IDrawable
    {
        private readonly Action<ICanvas, RectF> _drawAction = drawAction;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _drawAction.Invoke(canvas, dirtyRect);
        }
    }

    public partial class GraphicsView<T>
    {
        Action<ICanvas, RectF>? IGraphicsView.DrawAction { get; set; }

        partial void OnReset()
        {
            var thisAsIGraphicsView = (IGraphicsView)this;
            thisAsIGraphicsView.DrawAction = null;
        }

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
