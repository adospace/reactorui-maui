using MauiReactor.Animations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiReactor.TestApp.Pages;

class AnimationShowcasePageState 
{
    public bool IsEnabled { get; set; }

    public bool IsPaused { get; set; }

    public bool IsLooping { get; set; }

    public int? IterationCount { get; set; } = 1;

    public double InitialDelay { get; set; }

    public int SelectedEasingIndex { get; set; }

    public double DoubleValue { get; set; }
    public double DoubleValueSequence { get; set; }
    public double DoubleValueParallel { get; set; }
    public Point PointValueCubicBezier { get; set; }

    public List<(string Name, Easing Easing)> Easings { get; set; }
        = new()
        {
            ("Linear", Easing.Linear ),
            ("SinOut", Easing.SinOut ),
            ("SinIn", Easing.SinIn ),
            ("SinInOut", Easing.SinInOut ),
            ("CubicIn", Easing.CubicIn ),
            ("CubicOut", Easing.CubicOut ),
            ("CubicInOut", Easing.CubicInOut ),
            ("BounceOut", Easing.BounceOut ),
            ("BounceIn", Easing.BounceIn ),
            ("SpringIn", Easing.SpringIn ),
            ("SpringOut", Easing.SpringOut ),
        };    
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
                        new VerticalStackLayout
                        {
                            new Label("Easing").VCenter(),
                            new Picker()
                                .Title("Easing Function")
                                .ItemsSource(State.Easings.Select(_=>_.Name))
                                .SelectedIndex(()=> State.SelectedEasingIndex)
                                .OnSelectedIndexChanged((index)=> SetState(s => s.SelectedEasingIndex = index, false))
                            ,
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
                        new BoxView()
                            .BackgroundColor(Colors.Blue)
                            .HeightRequest(10)
                            .WidthRequest(10)
                            .TranslationX(()=>State.DoubleValueSequence)
                            .HStart()
                            ,
                        new BoxView()
                            .BackgroundColor(Colors.Yellow)
                            .HeightRequest(10)
                            .WidthRequest(10)
                            .TranslationX(()=>State.DoubleValueParallel)
                            .HStart()
                            ,
                        new BoxView()
                            .BackgroundColor(Colors.DarkViolet)
                            .HeightRequest(10)
                            .WidthRequest(10)
                            .TranslationX(()=>State.PointValueCubicBezier.X)
                            .TranslationY(()=>State.PointValueCubicBezier.Y)
                            .HStart()
                            ,
                    }
                    .Spacing(20)
                }
                .GridRow(1),

                new AnimationController
                {
                    new SequenceAnimation
                    {
                        new DoubleAnimation()
                            .StartValue(0)
                            .TargetValue(300)
                            .Duration(TimeSpan.FromSeconds(2.5))
                            .Easing(() => State.Easings[State.SelectedEasingIndex == -1 ? 0 : State.SelectedEasingIndex].Easing)
                            .OnTick(v => SetState(s => s.DoubleValue = v, false)),

                        new DoubleAnimation()
                            .StartValue(0)
                            .TargetValue(300)
                            .Duration(TimeSpan.FromSeconds(1.5))
                            .OnTick(v => SetState(s => s.DoubleValueSequence = v, false)),
                    }
                    .Loop(() => State.IsLooping)
                    .IterationCount(() => State.IterationCount)
                    .InitialDelay(()=>State.InitialDelay),

                    new SequenceAnimation
                    {
                        new DoubleAnimation()
                            .StartValue(0)
                            .TargetValue(300)
                            .Duration(TimeSpan.FromSeconds(2))                            
                            .OnTick(v => SetState(s => s.DoubleValueParallel = v, false)),

                        new CubicBezierPathAnimation()
                            .StartPoint(new Point(0,100))
                            .EndPoint(new Point(300,100))
                            .ControlPoint1(new Point(0,0))
                            .ControlPoint2(new Point(300,200))
                            .OnTick(v => SetState(s => s.PointValueCubicBezier = v, false)),

                        new QuadraticBezierPathAnimation()
                            .StartPoint(new Point(300,100))
                            .EndPoint(new Point(0,100))
                            .ControlPoint(new Point(150,200))
                            .OnTick(v => SetState(s => s.PointValueCubicBezier = v, false)),

                    }
                    .Loop(() => State.IsLooping)
                    .IterationCount(() => State.IterationCount)
                    .InitialDelay(()=>State.InitialDelay)
                }
                .IsEnabled(()=>State.IsEnabled)
                .OnIsEnabledChanged((isEnabled)=>SetState(s => s.IsEnabled = isEnabled, false))
                .IsPaused(()=>State.IsPaused)
                .OnIsPausedChanged((isPaused)=>SetState(s => s.IsPaused = isPaused, false))
            }
        };
    }
}
