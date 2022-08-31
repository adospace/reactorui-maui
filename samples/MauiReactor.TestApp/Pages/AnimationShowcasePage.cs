using MauiReactor.Animations;
using System;

namespace MauiReactor.TestApp.Pages;

class AnimationShowcasePageState: IState
{
    public bool IsEnabled { get; set; }

    public bool IsPaused { get; set; }

    public bool IsLooping { get; set; }

    public int? IterationCount { get; set; } = 1;

    public double InitialDelay { get; set; }

    public double DoubleValue { get; set; }
}


class AnimationShowcasePage : Component<AnimationShowcasePageState>
{
    public override VisualNode Render()
    {
        System.Diagnostics.Debug.WriteLine("AnimationShowcasePage.Render()");
        return new ContentPage("Animation Showcase")
        {
            new Grid("Auto *", "*")
            {
                new Frame
                {
                    new VerticalStackLayout
                    {
                        new Button()
                            .Text(() => State.IsEnabled ? "Stop" : "Start")
                            .TextColor(Colors.White)
                            .OnClicked(()=> SetState(s => s.IsEnabled = !s.IsEnabled, false)),
                        new Button()
                            .Text(() => State.IsPaused ? "Resume" : "Pause")
                            .TextColor(Colors.White)
                            .OnClicked(()=> SetState(s => s.IsPaused = !s.IsPaused, false)),
                        new HorizontalStackLayout
                        {
                            new Label("Loop").VCenter(),
                            new Switch()
                                .IsToggled(() => State.IsLooping)
                                .OnToggled((sender, args)=> SetState(s => s.IsLooping = args.Value, false)),
                        }
                        .Spacing(5),
                        new HorizontalStackLayout
                        {
                            new Label("Repeat Forever").VCenter(),
                            new Switch()
                                .IsToggled(() => State.IterationCount == null)
                                .OnToggled((sender, args)=> SetState(s => s.IterationCount = args.Value ? null : 1, false)),
                        }
                        .Spacing(5),
                        new VerticalStackLayout
                        {
                            new Label("Initial Delay").VCenter(),
                            new Slider()
                                .Minimum(0)
                                .Maximum(4000)
                                .Value(()=>State.InitialDelay)
                                .OnValueChanged((sender, args)=> SetState(s => s.InitialDelay = args.NewValue, false)),
                        }
                        .Spacing(5),
                    }
                    .Spacing(10)
                },

                new Frame
                {
                    new VerticalStackLayout
                    {
                        new BoxView()
                            .BackgroundColor(Colors.Red)
                            .HeightRequest(10)
                            .WidthRequest(10)
                            .TranslationX(()=>State.DoubleValue)
                            .HStart()
                            ,
                    }
                }
                .GridRow(1),

                new AnimationController
                {
                    new DoubleAnimation()
                        .StartValue(0)
                        .TargetValue(300)
                        .Duration(TimeSpan.FromSeconds(2))
                        .Loop(() => State.IsLooping)
                        .IterationCount(() => State.IterationCount)
                        .InitialDelay(()=>State.InitialDelay)
                        .OnTick(v => SetState(s => s.DoubleValue = v, false))
                }
                .IsEnabled(()=>State.IsEnabled)
                .OnIsEnabledChanged((isEnabled)=>SetState(s => s.IsEnabled = isEnabled, false))
                .IsPaused(()=>State.IsPaused)
                .OnIsPausedChanged((isPaused)=>SetState(s => s.IsPaused = isPaused, false))
            }
        };
    }
}
