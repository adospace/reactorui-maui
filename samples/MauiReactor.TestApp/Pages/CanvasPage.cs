using MauiReactor.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages
{
    internal class CanvasPage : Component
    {
        public override VisualNode Render()
        {
            return new ContentPage("Canvas Sample")
            {
                new Grid("*,Auto", "*")
                {
                    new GraphicsView
                    {
                        new Canvas()
                    },
                    new Button()
                        .GridRow(1)
                        .OnClicked(()=>Invalidate())
                },
            };
        }
    }
}
