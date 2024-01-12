using MauiReactor;

namespace MauiReactor.AppShell.Pages;

class MainPageState
{
    public int Counter { get; set; }
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
     => ContentPage(
            ScrollView(
                VStack(
                    Image("dotnet_bot.png")
                        .HeightRequest(200)
                        .HCenter()
                        .Set(Microsoft.Maui.Controls.SemanticProperties.DescriptionProperty, "Cute dot net bot waving hi to you!"),

                    Label("Hello, World!")
                        .FontSize(32)
                        .HCenter(),

                    Label("Welcome to MauiReactor: MAUI with superpowers!")
                        .FontSize(18)
                        .HCenter(),

                    Button(State.Counter == 0 ? "Click me" : $"Clicked {State.Counter} times!")
                        .OnClicked(()=>SetState(s => s.Counter ++))
                        .HCenter()
                )
                .VCenter()
                .Spacing(25)
                .Padding(30, 0)
            )
        );
}