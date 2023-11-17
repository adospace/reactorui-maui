using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiReactor.TestApp.Pages;


public class Bug179StartPage : Component
{
    public override VisualNode Render() => new NavigationPage()
    {
        new ContentPage()
        {
            new Button("Move To Main Page")
                .OnClicked(async () => await Navigation!.PushAsync<Bug179MainPage>())
        }
    };
}

public class Bug179MainPageState
{
    public bool IsLabelVisible { get; set; }
}

public class Bug179MainPage : Component<Bug179MainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage()
        {
            new StackLayout()
            {
                new Label()
                    .Text("Peek a Boo")
                    .IsVisible(State.IsLabelVisible),

                new Button()
                    .Text("Toggle Label")
                    .OnClicked(() => SetState(state => state.IsLabelVisible = !state.IsLabelVisible)),

                new Button("Open Child Page")
                    .OnClicked(async () => await Navigation!.PushModalAsync<Bug179ChildPage>())
            }
            .VCenter()
            .HCenter()
        }
        .Title("Main Page");
    }
}

public class Bug179ChildPage : Component
{
    public override VisualNode Render() => new NavigationPage()
    {
        new ContentPage()
        {
            new Button("Back")
                .VCenter()
                .HCenter()
                .OnClicked(async () => await Navigation!.PopModalAsync())
        }
        .Title("Child Page")
    }
    .OniOS(page => page.Set(MauiControls.PlatformConfiguration.iOSSpecific.Page.ModalPresentationStyleProperty, MauiControls.PlatformConfiguration.iOSSpecific.UIModalPresentationStyle.FormSheet));
}