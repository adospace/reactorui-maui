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
    public override VisualNode Render()
    {
        return new Window
        {
            new ContentPage
            {
                new ScrollView
                {
                    new VerticalStackLayout
                    {
                        new Image("dotnet_bot.png")
                            .HeightRequest(200)
                            .HCenter()
                            .Set(Microsoft.Maui.Controls.SemanticProperties.DescriptionProperty, "Cute dot net bot waving hi to you!"),

                        new Label("Hello, World!")
                            .FontSize(32)
                            .HCenter(),

                        new Label("Welcome to MauiReactor: MAUI with superpowers!")
                            .FontSize(18)
                            .HCenter(),

                        new Button(State.Counter == 0 ? "Click me" : $"Clicked {State.Counter} times!")
                            .OnClicked(()=>SetState(s => s.Counter ++))
                            .HCenter()
                    }
                    .VCenter()
                    .Spacing(25)
                    .Padding(30, 0)
                }
            }
            .WindowTitle("My Window Title")
        }
        .Title("My Window Title")
        .OnSizeChanged(OnSizeChanged)
        ;
    }

    private void OnSizeChanged(object sender, EventArgs args)
    {
        var window = ((MauiControls.Window)sender);
        System.Diagnostics.Debug.WriteLine($"Window size changed to {window.Width}x{window.Height}");
        System.Diagnostics.Debug.WriteLine($"Window position changed to {window.X},{window.Y}");
    }
}