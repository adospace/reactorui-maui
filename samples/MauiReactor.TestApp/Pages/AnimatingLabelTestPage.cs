using MauiReactor.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public bool Show { get; set; }

    public double Height { get; set; }
}

partial class AnimatingLabel : Component<AnimatinLabelState>
{
    [Prop]
    string _text = string.Empty;

    protected override void OnMountedOrPropsChanged()
    {
        State.Show = false;
        SetState(s => s.Show = true, 2000);

        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return
            HStack(
                _text.Select(RenderChar).ToArray()
            )
        .HCenter();
    }

    VisualNode RenderChar(char character, int index)
    {
        return Grid(
            Label($"{character}")
                .FontSize(24)
                .TranslationY(State.Show ? 0.0 : -15)
                .Opacity(State.Show ? 1.0 : 0.0)
                .WithAnimation(
                    duration: State.Show ? 0 : 700,
                    easing: ExtendedEasing.OutQuint,
                    initialDelay: Math.Sin((index / (double)_text.Length) * Math.PI / 2) * 1000)
                ,
            Label($"{character}")
                .FontSize(24)
                .TranslationY(State.Show ? 15 : 0.0)
                .Opacity(State.Show ? 0.0 : 1.0)
                .WithAnimation(
                    duration: State.Show ? 0 : 700,
                    easing: ExtendedEasing.OutQuint,
                    initialDelay: Math.Sin((index / (double)_text.Length) * Math.PI / 2) * 1000)
            );
    }

}
