using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestAppWindow.Pages;

class MainPageState
{
    public int Counter { get; set; }
}

class MainWindow : Component<MainPageState>
{
    MauiControls.Page? _containerPage;

    public override VisualNode Render()
    {
        return Window(
            ContentPage(pageRef => _containerPage = pageRef,
                ScrollView(
                    VStack(                        
                        Image("dotnet_bot.png")
                            .HeightRequest(200)
                            .HCenter()
                            .Set(MauiControls.SemanticProperties.DescriptionProperty, "Cute dot net bot waving hi to you!"),

                        Label("Hello, World!")
                            .FontSize(32)
                            .HCenter(),

                        Label("Welcome to MauiReactor: MAUI with superpowers!",
                            MenuFlyout(
                                MenuFlyoutItem("Black").OnClicked(() => OnMenuClicked("Black")),
                                MenuFlyoutSubItem(
                                    MenuFlyoutItem("Blue").OnClicked(() => OnMenuClicked("Black")),
                                    MenuFlyoutItem("Green").OnClicked(() => OnMenuClicked("Green")),
                                    MenuFlyoutItem("Red").OnClicked(() => OnMenuClicked("Red"))
                                    )
                                    .Text("Light"),
                                MenuFlyoutItem("White")
                                    .IconImageSource(new MauiControls.FontImageSource
                                    {
                                        Glyph = "\u23F9",
                                        FontFamily = "Arial"
                                    })
                                    .OnClicked(() => OnMenuClicked("White"))
                                )
                            )
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
            )
        )
        .Title("My Window Title")
        .OnSizeChanged(OnSizeChanged)
        ;
    }

    private async Task OnMenuClicked(string color)
    {
        await _containerPage.DisplayAlertAsync("Main Window", $"You clicked {color} color", "OK");
    }

    private void OnSizeChanged(object sender, EventArgs args)
    {
        var window = ((MauiControls.Window)sender);
        System.Diagnostics.Debug.WriteLine($"Window size changed to {window.Width}x{window.Height}");
        System.Diagnostics.Debug.WriteLine($"Window position changed to {window.X},{window.Y}");
    }
}