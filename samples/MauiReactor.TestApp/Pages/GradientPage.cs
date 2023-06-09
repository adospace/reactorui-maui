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
}

class GradientPage : Component<GradientPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage("Gradients Test Page")
        {
            new Grid("Auto, *", "*")
            {
                new VStack(10)
                {
                    new Picker()
                        .ItemsSource(new []{ "Linear", "Radial" })
                        .SelectedIndex(State.GradientType == GradientType.Linear ? 0 : 1)
                        .OnSelectedIndexChanged(selectedIndex => SetState(s => s.GradientType = selectedIndex == 0 ? GradientType.Linear : GradientType.Radial)),

                    new Label(State.Degree),

                    State.GradientType == GradientType.Linear ?
                    new Slider()
                        .Minimum(0)
                        .Maximum(360)
                        .OnValueChanged(v => SetState(s => s.Degree = v))
                        .Value(State.Degree)
                    :
                    null
                },

                new Border()
                    .GridRow(1)
                    .Background(new LinearGradient(State.Degree, Colors.White, Colors.Black))
                    //.Background(new LinearGradient(State.Degree, 0xFF1f005c, 0xFF5b0060, 0xFF870160, 0xFFac255e, 0xFFca485c, 0xFFe16b5c, 0xFFf39060, 0xFFffb56b))
            }
            .Margin(10)
        };
    }
}
