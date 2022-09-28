using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.ComponentModel.DataAnnotations;
using Microsoft.Maui.Graphics;
using System.Net;
using Microsoft.Maui.Primitives;
using System.Threading;
using System.Collections;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace MauiReactor.Canvas.Internals
{
    public class CanvasView : Microsoft.Maui.Controls.GraphicsView
    {
        private class CanvasViewDrawable : IDrawable
        {
            private readonly CanvasView _owner;

            public CanvasViewDrawable(CanvasView owner)
            {
                _owner = owner;
            }

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                _owner.OnDraw(canvas, dirtyRect);
            }
        }

        public CanvasView()
        {
            Drawable = new CanvasViewDrawable(this);
        }

        private void OnDraw(ICanvas canvas, RectF dirtyRect)
        {
            var context = new DrawingContext(canvas, dirtyRect);
            foreach (var child in Children)
            {
                child.Draw(context);
            }
        }

        public List<CanvasNode> Children { get; } = new();

    }
}