using MauiReactor.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class AnimationShowcasePageState: IState
{
    public double DoubleValue { get; set; }
}


class AnimationShowcasePage : Component<AnimationShowcasePageState>
{
    public override VisualNode Render()
    {
        return new ContentPage("Animation Showcase")
        {
            new VerticalStackLayout
            {
                new Label(() => State.DoubleValue.ToString()),

                new AnimationController
                {
                    new DoubleAnimation()
                        .StartValue(0)
                        .TargetValue(100)
                        .Duration(TimeSpan.FromSeconds(10))
                        .OnTick(v => SetState(s => s.DoubleValue = v))
                }
                .IsEnabled(true)
            }
            .VCenter()
            .HCenter()
            .Spacing(20)
        };
    }
}
