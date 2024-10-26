﻿using MauiReactor.Animations;
using MauiReactor.TestApp.Resources.Styles;
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
                        new AnimatingLabel()
                            .Text($"Clicked {State.Counter} times!"),
                        new AnimatingLabel()
                            .Text($"Clicked {State.Counter} times!"),
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
    public bool Show { get; set; } = true;

    public double Height { get; set; }

    public string CurrentText = string.Empty;
}

partial class AnimatingLabel : Component<AnimatinLabelState>
{
    [Prop]
    string _text = string.Empty;

    protected override void OnMountedOrPropsChanged()
    {
        if (State.Show && _text != State.CurrentText)
        {
            State.Show = false;
            SetState(s =>
            {
                s.Show = true;
                s.CurrentText = _text;
            }, 2400);
        }

        base.OnMountedOrPropsChanged();
    }

    public override VisualNode Render()
    {
        return Grid(
            HStack(
                State.CurrentText.Select(RenderCharToTop).ToArray()
            ),
            HStack(
                _text.Select(RenderCharFromBottom).ToArray()
            ),

            Grid()
                .Background(new LinearGradient(0, 
                    (AppTheme.IsDarkTheme ? AppTheme.DarkBackground : AppTheme.LightBackground, 0.0f), 
                    (Colors.Transparent, 0.2f), 
                    (Colors.Transparent, 0.8f), 
                    (AppTheme.IsDarkTheme ? AppTheme.DarkBackground : AppTheme.LightBackground, 1.0f)))
        )
        .IsClippedToBounds(true)
        .HCenter();
    }

    Label RenderCharToTop(char character, int index)
    {
        return Label($"{character}")
            .FontSize(24)
            .TranslationY(State.Show ? 0.0 : -15)
            .Opacity(State.Show ? 1.0 : 0.0)
            .ScaleY(State.Show ? 1.0 : 0.0)
            .WithAnimation(
                duration: State.Show ? 0 : 700,
                easing: ExtendedEasing.OutQuint,
                initialDelay: Math.Sin((index / (double)_text.Length) * Math.PI / 2) * 1000);
    }

    Label RenderCharFromBottom(char character, int index)
    {
        return Label($"{character}")
            .FontSize(24)
            .TranslationY(State.Show ? 15 : 0.0)
            .Opacity(State.Show ? 0.0 : 1.0)
            .ScaleY(State.Show ? 0.0 : 1.0)
            .WithAnimation(
                duration: State.Show ? 0 : 700,
                easing: ExtendedEasing.OutQuint,
                initialDelay: Math.Sin((index / (double)_text.Length) * Math.PI / 2) * 1000);
    }

}
