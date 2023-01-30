using System;
namespace MauiReactor.TestApp.Pages;

class AnimationBasicsState
{
	public bool ToggleState { get; set; }
}

class AnimationBasics : Component<AnimationBasicsState>
{
    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new VStack(spacing: 10)
            {
                new Label("Moving Label")
                    .TranslationX(State.ToggleState ? -100 : +100)
                    .WithAnimation(duration: 100000)
                    ,

                new Button("Move")
                    .OnClicked(()=>SetState(s => s.ToggleState = !s.ToggleState))
            }
            .HCenter()
            .VCenter()
        };
    }
}

