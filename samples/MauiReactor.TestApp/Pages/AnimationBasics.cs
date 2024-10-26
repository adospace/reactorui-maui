using Microsoft.Maui.Controls;
using System;
namespace MauiReactor.TestApp.Pages;

class AnimationBasicsState
{
	public bool ToggleState { get; set; }
}

class AnimationBasics : Component<AnimationBasicsState>
{
    private MauiControls.Label? _labeRef;

    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new VStack(spacing: 10)
            {
                new Label(labelRef => _labeRef = labelRef)
                    .Text("Moving Label")
                    .HStart()
                    .TranslationX(State.ToggleState ? 0 : 200)
                    .WithAnimation(duration: 1000)
                    ,

                new Button("Move")
                    .OnClicked(()=>SetState(s => s.ToggleState = !s.ToggleState))
                    //.OnClicked(async ()=>
                    //{
                    //    State.ToggleState = !State.ToggleState;
                    //    if (_labeRef != null)
                    //    {
                    //        await _labeRef.TranslateTo(State.ToggleState ? 0 : 200, 0, 1000);
                    //    }                        
                    //})
                    .HCenter()
            }
            .WidthRequest(400)
            .HCenter()
            .VCenter()
        };
    }
}

