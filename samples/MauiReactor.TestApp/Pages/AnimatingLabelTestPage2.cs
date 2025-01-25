using MauiReactor.Animations;
using MauiReactor.TestApp.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class AnimatingLabelTestPage2State
{
    public int Counter { get; set; }
}

class AnimatingLabelTestPage2 : Component<AnimatingLabelTestPage2State>
{
    static string[] _lines = [
        "MauiReactor 3 is out!",
        "support for .NET 9",
        "iOS AOT by default",
        "async events",
        "..and more!",
        "Check it out!"
    ];

    public override VisualNode Render()
        => ContentPage(
            VStack(
                new AnimatingLabel2()
                    .Text(_lines[State.Counter % _lines.Length]),

                Button("Click me!")
                    .OnClicked(() => SetState(s => s.Counter++))
                    .HCenter(),

                Button("Toggle Theme", AppTheme.ToggleCurrentAppTheme)
                    .HCenter()
            )
            .VCenter()
            .Spacing(25)
            .Padding(30, 0)
        );
}

class AnimatinLabel2State
{
    public bool AnimationCompleted { get; set; } = true;

    public string CurrentText = string.Empty;

    public string NewText = string.Empty;

    public double TextOffset;
}

partial class AnimatingLabel2 : Component<AnimatinLabel2State>
{
    [Prop]
    string _text = string.Empty;

    protected override void OnMountedOrPropsChanged()
    {
        State.NewText = _text;
        State.AnimationCompleted = false;

        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return Grid(

            RenderCharFromBottom(State.NewText),
            RenderCharToTop(State.CurrentText),

            Grid()
                .Background(new LinearGradient(0,
                    (Theme.IsDarkTheme ? AppTheme.DarkBackground : AppTheme.LightBackground, 0.0f),
                    (Colors.Transparent, 0.2f),
                    (Colors.Transparent, 0.8f),
                    (Theme.IsDarkTheme ? AppTheme.DarkBackground : AppTheme.LightBackground, 1.0f))),

            new AnimationController
            {
                new ParallelAnimation
                {
                    new DoubleAnimation()
                        .StartValue(1.0)
                        .TargetValue(0.0)
                        .Duration(400)
                        .Easing(ExtendedEasing.OutQuint)
                        .OnTick(v => SetState(s => s.TextOffset = v, false))
                }
            }
            .IsEnabled(!State.AnimationCompleted)
            .OnIsEnabledChanged((isEnabled) => SetState(s =>
            {
                if (!isEnabled)
                {
                    s.AnimationCompleted = true;
                    s.CurrentText = s.NewText;
                }
            }))
        )
        .IsClippedToBounds(true);
    }

    Label RenderCharToTop(string text)
    {
        return Label(text)
            .HorizontalTextAlignment(TextAlignment.Center)
            .FontSize(36)
            .TranslationY(() => (1.0 - State.TextOffset) * -20.0)
            .Opacity(() => State.TextOffset)
            .ScaleY(() => State.TextOffset);
    }

    Label RenderCharFromBottom(string text)
    {
        return Label(text)
            .HorizontalTextAlignment(TextAlignment.Center)
            .FontSize(36)
            .TranslationY(() => State.TextOffset * 20.0)
            .Opacity(() => 1.0 - State.TextOffset)
            .ScaleY(() => 1.0 - State.TextOffset);
    }

}
