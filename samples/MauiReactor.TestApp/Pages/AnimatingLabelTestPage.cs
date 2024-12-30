using MauiReactor.Animations;
using MauiReactor.TestApp.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class AnimatingLabelTestPageState
{
    public int Counter { get; set; }
}

class AnimatingLabelTestPage : Component<AnimatingLabelTestPageState>
{
    public override VisualNode Render()
        => ContentPage(
                ScrollView(
                    VStack(
                        new AnimatingLabel()
                            .Text($"Clicked {State.Counter} times!"),

                        Button("Click me!")
                            .OnClicked(() => SetState(s => s.Counter++))
                            .HCenter(),

                        Button("Toggle Theme", AppTheme.ToggleCurrentAppTheme)
                            .HCenter()
                )
                .VCenter()
                .Spacing(25)
                .Padding(30, 0)
            )
        );
}

class AnimatinLabelState
{
    public bool AnimationCompleted { get; set; } = true;

    public string CurrentText = string.Empty;

    public string NewText = string.Empty;

    public Dictionary<int, double> CharactersOffsets = [];
}

partial class AnimatingLabel : Component<AnimatinLabelState>
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
            HStack(
                State.CurrentText.Select(RenderCharToTop).ToArray()
            ),
            HStack(
                State.NewText.Select(RenderCharFromBottom).ToArray()
            ),

            Grid()
                .Background(new LinearGradient(0,
                    (Theme.IsDarkTheme ? AppTheme.DarkBackground : AppTheme.LightBackground, 0.0f),
                    (Colors.Transparent, 0.2f),
                    (Colors.Transparent, 0.8f),
                    (Theme.IsDarkTheme ? AppTheme.DarkBackground : AppTheme.LightBackground, 1.0f))),

            new AnimationController
            {
                Enumerable.Range(0, State.NewText.Length)
                    .Select(index =>
                        new ParallelAnimation
                        {
                            new DoubleAnimation()
                                .StartValue(0.0)
                                .TargetValue(1.0)
                                .Duration(400)
                                .Easing(ExtendedEasing.OutQuint)
                                .OnTick(v => SetState(s => s.CharactersOffsets[index] = v, false))
                        }
                        .InitialDelay(Math.Sin(index / (double)State.NewText.Length * Math.PI / 2) * 1000))
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
        .IsClippedToBounds(true)
        .HCenter();
    }

    Label RenderCharFromBottom(char character, int index)
    {
        return Label($"{character}")
            .FontSize(36)
            .TranslationY(() => (1.0-State.CharactersOffsets.GetValueOrDefault(index, 0.0)) * -20.0)
            .Opacity(() => State.CharactersOffsets.GetValueOrDefault(index, 0.0))
            .ScaleY(() => State.CharactersOffsets.GetValueOrDefault(index, 0.0));
    }

    Label RenderCharToTop(char character, int index)
    {
        return Label($"{character}")
            .FontSize(36)
            .TranslationY(() => State.CharactersOffsets.GetValueOrDefault(index, 0.0) * 20.0)
            .Opacity(() => 1.0 - State.CharactersOffsets.GetValueOrDefault(index, 1.0))
            .ScaleY(() => 1.0 - State.CharactersOffsets.GetValueOrDefault(index, 1.0));
    }

}
