using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Graphics
{
    public class Canvas : VisualNode
    {
        private class CanvasDrawable : IDrawable
        {
            public Canvas? Owner { get; set; }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.StrokeColor = Colors.Red;
                canvas.StrokeSize = 6;
                canvas.DrawLine(10, 10, 90, 100);
            }
        }

        private CanvasDrawable? _drawable;

        internal IDrawable GetDrawable()
        {
            if (_drawable == null)
            {
                _drawable = new CanvasDrawable() { Owner = this };
            }

            return _drawable;
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            ((Canvas)newNode)._drawable = _drawable;

            base.OnMigrated(newNode);
        }
    }
}
