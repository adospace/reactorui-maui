using System;
using System.Linq;
using Contentics.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;

namespace Contentics.Pages;

enum PageEnum
{
    Home,

    Events,

    Community,

    Assets,

    Calendar
}


class MainPageState
{
    public PageEnum CurrentPage { get; set; } = PageEnum.Home;
}

class Main : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new NavigationPage
        {
            new ContentPage
            {
                new Grid("*", "*")
                {
                    RenderPage(),

                    RenderTabBar()
                }
            }
            .Set(MauiControls.NavigationPage.HasNavigationBarProperty, false)
            .BackgroundColor(ThemeBrushes.Background)
        };
    }

    VisualNode RenderPage() => State.CurrentPage switch
    {
        PageEnum.Home => new Home(),
        PageEnum.Events => new Events(),
        PageEnum.Community => new Community(),
        PageEnum.Assets => new Assets(),
        PageEnum.Calendar => new Calendar(),
        _ => throw new NotImplementedException(),
    };

    VisualNode RenderTabBar()
    {
        ImageButton createButton(PageEnum page, int column) =>
            new ImageButton()
                .Aspect(Aspect.Center)
                .Source(() => State.CurrentPage != page ? $"{page.ToString().ToLowerInvariant()}.png" : $"{page.ToString().ToLowerInvariant()}_on.png")
                .GridColumn(column)
                .OnClicked(() => SetState(s => s.CurrentPage = page))
                .Margin(State.CurrentPage == page ? new Thickness(0,0,0,10) : new Thickness())
                .WithAnimation(Easing.BounceOut, 300)
                ;

        return new Grid("*", "*")
        {
            new CanvasView()
            {
                new DropShadow
                {
                    new Box()
                        .Margin(0,20,0,0)
                        .CornerRadius(24,24,0,0)
                        .BackgroundColor (ThemeBrushes.White)
                }
                .Color(ThemeBrushes.DarkShadow)
                .Size(0, -8)
                .Blur(32),

                new Row("* * * * *")
                {
                    Enum.GetValues(typeof(PageEnum))
                        .Cast<PageEnum>()
                        .Select(page =>
                            new Align()
                            {
                                new Ellipse()
                                    .FillColor(ThemeBrushes.Purple10)
                            }
                            .IsVisible(State.CurrentPage == page)
                            .Width(4)
                            .Height(4)
                            .VEnd()
                            .HCenter()
                            .Margin(16)
                        )
                        .ToArray()
                }
                
            }
            .BackgroundColor(Colors.Transparent)
            .GridRow(1),

            new Grid("*", "* * * * *")
            {
                createButton(PageEnum.Home, 0),
                createButton(PageEnum.Events, 1),
                createButton(PageEnum.Community, 2),
                createButton(PageEnum.Assets, 3),
                createButton(PageEnum.Calendar, 4)
            }
            .Padding(0,20,0,0)
        }
        .VEnd()
        .HeightRequest(92);
    }
}
