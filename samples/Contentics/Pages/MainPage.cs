using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contentics.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;

namespace Contentics.Pages;

enum PageEnum
{
    Home,

    Assets,

    Calendar,

    Community,

    Events,
}

class MainPageState : IState
{
    public PageEnum CurrentPage { get; set; }
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid("268, *, 72", "*")
            {
                RenderTopPanel(),

                RenderTabBar()
            }
        }
        .BackgroundColor(ThemeBrushes.Background);
    }

    VisualNode RenderTopPanel()
    {
        return new Grid("*", "*")
        {
            new Image("top.png")
                .Aspect(Aspect.AspectFill),

            new CanvasView
            {
                new Group
                {
                    new ClipRectangle
                    {
                        new Picture("Contentics.Resources.Images.photo1.png")
                    }
                    .CornerRadius(16),

                    new Align()
                    {
                        new Ellipse()
                            .StrokeColor(ThemeBrushes.Purple)
                            .FillColor(ThemeBrushes.Green)
                            .StrokeSize(5)
                    }
                    .VEnd()
                    .HStart()
                    .Width(13)
                    .Height(13)
                }
            }
            .Background(Colors.Transparent)
            .HeightRequest(48)
            .WidthRequest(48)
            .HCenter()
            .VStart()
            .Margin(0,72,0,0)


        }
        .GridRow(0);
    }
    

    VisualNode RenderTabBar()
    {
        ImageButton createButton(PageEnum page, int column) =>
            new ImageButton()
                .Aspect(Aspect.Center)
                .Source(() => State.CurrentPage != page ? $"{page.ToString().ToLowerInvariant()}.png" : $"{page.ToString().ToLowerInvariant()}_on.png")
                .GridColumn(column)
                .OnClicked(() => SetState(s => s.CurrentPage = page))
                .Padding(0,0,0,5)
                ;

        return new Grid("*", "*")
        {
            new CanvasView()
            {
                new Group
                {
                    new DropShadow
                    {
                        new Box()
                            .CornerRadius(24,24,0,0)
                            .BackgroundColor (ThemeBrushes.White)
                    }
                    .Color(ThemeBrushes.DarkShadow)
                    .Size(0, -8)
                    .Blur(32)
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
        }
        .GridRow(2);
    }
}
