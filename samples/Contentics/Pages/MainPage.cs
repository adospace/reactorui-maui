using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contentics.Models;
using Contentics.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;
using MauiReactor.Compatibility;
using Microsoft.Maui.Devices;

namespace Contentics.Pages;

enum PageEnum
{
    Home,

    Events,

    Community,

    Assets,

    Calendar
}


class MainPageState : IState
{
    public PageEnum CurrentPage { get; set; } = PageEnum.Home;
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid("268, *, 92", "*")
            {
                new ScrollView
                {
                    new Grid("360, 400", "*")
                    {
                        RenderNewsPanel(),
                        RenderTasksPanel()
                    }
                }
                .Padding(0,14,0,14)
                .Margin(0,-14,0,0)
                .GridRow(1)
                .GridRowSpan(2),

                RenderTopPanel(),

                RenderSearchBox(),

                RenderTabBar()
            }
        }
        .BackgroundColor(ThemeBrushes.Background);
    }

    VisualNode RenderTopPanel()
    {
        return new Grid
        {
            new CanvasView
            {
                new Picture("Contentics.Resources.Images.top.png")
                    .Aspect(Aspect.Fill),

                new Align
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
                                .StrokeColor(ThemeBrushes.Purple10)
                                .FillColor(ThemeBrushes.Green)
                                .StrokeSize(5)
                        }
                        .VEnd()
                        .HStart()
                        .Width(13)
                        .Height(13)
                    }
                }
                .Height(48)
                .Width(48)
                .HCenter()
                .VStart()
                .Margin(0,72,0,0),

                new Align
                {
                    new Column("23, *")
                    {
                        new Text("Welcome back,")
                            .FontColor(ThemeBrushes.Purple30)
                            .FontSize(18)
                            .HorizontalAlignment(HorizontalAlignment.Center),

                        new Text("Artur")
                            .FontColor(ThemeBrushes.White)
                            .FontSize(32)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            .VerticalAlignment(VerticalAlignment.Top)
                    }
                }
                .Height(61)
                .VEnd()
                .Margin(0,0,0,56)
            }
            .VStart()
            .HeightRequest(253)
            .Background(Colors.Transparent),
            
            new ImageButton("notify.png")
                .VStart()
                .HStart()
                .Margin(17, 44, 0, 0),

            new ImageButton("menu.png")
                .VStart()
                .HEnd()
                .Margin(0, 44, 17, 0)

        }
        .GridRow(0);
    }

    VisualNode RenderSearchBox()
    {
        return new Grid("72", "*")
        {
            new CanvasView
            {
                new Align
                {
                    new DropShadow
                    {
                        new Box
                        {
                            new Align
                            {
                                new Picture("Contentics.Resources.Images.search_small.png")
                            }
                            .Width(32)
                            .Height(32)
                            .HStart()
                            .Margin(16,20,0,20)
                        }
                        .CornerRadius(12)
                        .BackgroundColor(ThemeBrushes.White)
                        .Margin(24,0,24,0)
                    }
                    .Color(ThemeBrushes.DarkShadow10)
                    .Size(0, 8)
                    .Blur(16)
                }
                .Margin(0,0,0,18)
            }
            .VStart()
            .HeightRequest(72)
            .BackgroundColor(Colors.Transparent),

            new Entry()
                .BackgroundColor(ThemeBrushes.White)
                .PlaceholderColor(ThemeBrushes.Gray100)
                .Placeholder("Search in app")
                .TextColor(ThemeBrushes.Gray100)                
                .Margin(48 + 32, 6, 48, 24)
        }
        .Margin(0,221,0,0)

        .GridRow(0);
    }
    
    VisualNode RenderNewsPanel()
    {
        return new Grid("42 *", "*")
        {
            new Label("Latest news")
                .TextColor(ThemeBrushes.Dark)
                .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                .FontSize(16)
                .Margin(16,0),

            new Label("See all")
                .TextColor(ThemeBrushes.Gray100)
                .FontSize(16)
                .HEnd()
                .Margin(16,0),


            new ScrollView
            {
                new HStack(8)
                {
                    Models.NewsModel.Latest.Select(RenderNewsItem)
                }
            }
            .Orientation(ScrollOrientation.Horizontal)
            .VStart()
            .GridRow(1)
            .Padding(16,0)

        }
        .Margin(0, 16, 0, 0);
    }

    VisualNode RenderNewsItem(NewsModel newsItem)
    {
        return new CanvasView
        {
            new Box
            {
                new Column("166, 56, 24")
                {
                    new ClipRectangle
                    {
                        new Picture($"Contentics.Resources.Images.{newsItem.ImageSource}")
                    }
                    .CornerRadius(8),

                    new Text(newsItem.Title)
                        .FontWeight(FontWeights.Bold)
                        .FontColor(ThemeBrushes.Dark)
                        .FontSize(14)
                        .Margin(0,16),

                    new Row("16, *")
                    {
                        new Align
                        {
                            new ClipRectangle
                            {
                                new Picture($"Contentics.Resources.Images.{newsItem.AgentAvatar}")
                            }
                            .CornerRadius(24)
                        }
                        .VCenter()
                        .Height(16),

                        new Text($"{newsItem.AgentName} {newsItem.Date.ToLongDateString()}")
                            .Margin(8, 0)
                            .FontSize(12)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .FontColor(ThemeBrushes.Gray100)
                    }
                    
                }
            }
            .Padding(16)
            .CornerRadius(16)
            .BackgroundColor(ThemeBrushes.White)
        }
        .BackgroundColor(Colors.Transparent)
        .HeightRequest(277)
        .WidthRequest(298);
    }

    VisualNode RenderTasksPanel()
    {
        return new Grid("42 *", "*")
        {
            new Label("Upcoming tasks")
                .TextColor(ThemeBrushes.Dark)
                .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                .FontSize(16)
                .Margin(16,0),

            new Label("See all")
                .TextColor(ThemeBrushes.Gray100)
                .FontSize(16)
                .HEnd()
                .Margin(16,0),


            new VStack(8)
            {
                TaskModel.All.Select(RenderTaskItem)
            }
            .VStart()
            .GridRow(1)
            .Padding(16,0)

        }
        .GridRow(1)
        .Margin(0, 0, 0, 0)
        ;
    }

    VisualNode RenderTaskItem(TaskModel item)
    {
        return new CanvasView
        {
            new Box
            {
                new Row("32, *, 35")
                {
                    new Ellipse()
                        .StrokeColor(item.CircleColor)
                        .StrokeSize(2)
                        .Margin(16,19,0,19),

                    new Text(item.Title)
                        .Margin(16,0,0,0)
                        .VerticalAlignment(VerticalAlignment.Center),


                    new Picture("Contentics.Resources.Images.chevron_right.png")
                        .Margin(0,19,19,19)

                }
            }
            .Padding(16)
            .CornerRadius(16)
            .BackgroundColor(item.BackgroundColor)
        }
        .BackgroundColor(Colors.Transparent)
        .HeightRequest(54);
    }

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
                new Align
                {
                    new DropShadow
                    {
                        new Box()
                            .CornerRadius(24,24,0,0)
                            .BackgroundColor (ThemeBrushes.White)
                            
                    }
                    .Color(ThemeBrushes.DarkShadow)
                    .Size(0, -8)
                    .Blur(32),
                }
                .Margin(0,20,0,0),

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

            new Grid("72", "* * * * *")
            {
                createButton(PageEnum.Home, 0),
                createButton(PageEnum.Events, 1),
                createButton(PageEnum.Community, 2),
                createButton(PageEnum.Assets, 3),
                createButton(PageEnum.Calendar, 4)
            }
            .VEnd()
        }
        .GridRow(2);
    }
}
