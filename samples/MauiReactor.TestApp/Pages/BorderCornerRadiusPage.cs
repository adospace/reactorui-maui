using MauiReactor.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class BorderCornerRadiusPageState
{
    public int Counter { get; set; }

    public double CornerRadius { get; set; } = 5;
}

class BorderCornerRadiusPage : Component<BorderCornerRadiusPageState>
{
    public override VisualNode Render()
    {
        return ContentPage(
        [
            VStack(
            [
                Button()
                    .Text("Increase corner radius")
                    .OnClicked(()=>SetState(s => s.CornerRadius+=15))
                    .HCenter(),

                Border()
                    .StrokeShape(
                        RoundRectangle()
                            .CornerRadius(new CornerRadius(State.CornerRadius))
                            .WithAnimation(duration: 1000))
                    .BackgroundColor(Colors.Red)
                    .HeightRequest(200)
                    .WidthRequest(200),
            ])
            .VCenter()
            .Spacing(25)
            .Padding(30, 0)
        ])
        .Title("Test Border Corner");
    }

}