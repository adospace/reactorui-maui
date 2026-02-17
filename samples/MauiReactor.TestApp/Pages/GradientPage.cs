using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

enum GradientType
{
    Linear,

    Radial
}

class GradientPageState
{
    public GradientType GradientType { get; set; }

    public double Degree { get; set; } = 180;

    public double Radius { get; set; } = 0.5;
}

class GradientPage : Component<GradientPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage("Gradients Test Page")
        {
            new Grid("Auto, 400", "400")
            {
                new VStack(10)
                {
                    new Picker()
                        .ItemsSource(new []{ "Linear", "Radial" })
                        .SelectedIndex(State.GradientType == GradientType.Linear ? 0 : 1)
                        .OnSelectedIndexChanged(selectedIndex => SetState(s => s.GradientType = selectedIndex == 0 ? GradientType.Linear : GradientType.Radial)),

                    new Label(State.GradientType == GradientType.Linear ? State.Degree : State.Radius),

                    State.GradientType == GradientType.Linear ?
                    new Slider()
                        .Minimum(0)
                        .Maximum(360)
                        .OnValueChanged(v => SetState(s => s.Degree = (int)v))
                        .Value(State.Degree)
                    :
                    new Slider()
                        .Minimum(0.1)
                        .Maximum(3)
                        .OnValueChanged(v => SetState(s => s.Radius = v))
                        .Value(State.Radius)
                },

                new Border()
                    .GridRow(1)
                    .Background(
                        State.GradientType == GradientType.Linear ? 
                        new LinearGradient(State.Degree, Colors.White, Colors.Black) 
                        : 
                        new RadialGradient(State.Radius, Colors.White, Colors.Black))
            }
            .Margin(10)
        };
    }
}
