using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class GraphicsViewState : IState
{
    public double Rotation { get; set; }
}

class GraphicsViewPage : Component<GraphicsViewState>
{
    public override VisualNode Render()
    {
        return new ContentPage("GraphicsView Sample")
        {
            new Grid("Auto, *", "*")
            {
                new Slider()
                    .Minimum(0)
                    .Maximum(360)
                    .Value(State.Rotation)
                    .OnValueChanged((s,e)=>SetState(s => s.Rotation = e.NewValue))
                    ,
                new GraphicsView()
                    .GridRow(1)
                    .OnDraw(OnDraw)
            }
        };
    }

    private void OnDraw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Rotate((float)State.Rotation, dirtyRect.Center.X, dirtyRect.Center.Y);
        canvas.FontColor = Colors.Red;
        canvas.FontSize = 24;
        canvas.DrawString("GraphicsView", dirtyRect, HorizontalAlignment.Center, VerticalAlignment.Center);
    }
}
