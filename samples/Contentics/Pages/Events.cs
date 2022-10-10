using Contentics.Models;
using Contentics.Resources.Styles;
using MauiReactor;
using MauiReactor.Compatibility;
using MauiReactor.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contentics.Pages;

enum EventsCategory
{
    Featured,

    Upcoming,

    Trending,

    Popular
}

class EventsPageState : IState
{
    public EventsCategory Category { get; set; }
}

class Events : Component<EventsPageState>
{
    public override VisualNode Render()
    {
        return new Grid("164, Auto, Auto, Auto *", "*")
        {
            RenderTopPanel(),
            
            RenderSearchBox(),

            RenderTopics(),

            RenderCategories(),

            RenderEvents()
        };
    }

    VisualNode RenderTopPanel()
    {
        return new Grid
        {
            new CanvasView
            {
                new Box()
                    .CornerRadius(0, 0, 24, 24)
                    .BackgroundColor(ThemeBrushes.White),

                new Text("Events")
                    .Margin(32)
                    .FontColor(ThemeBrushes.Dark)
                    .FontSize(32)
                    .FontWeight(FontWeights.Bold)
                    .HorizontalAlignment(HorizontalAlignment.Center)
                    .VerticalAlignment(VerticalAlignment.Bottom)
            }
            .VStart()
            .HeightRequest(164)
            .Background(Colors.Transparent),

            new ImageButton("notify_black.png")
                .VStart()
                .HStart()
                .Margin(17, 44, 0, 0),

            new ImageButton("menu_black.png")
                .VStart()
                .HEnd()
                .Margin(0, 44, 17, 0)

        }
        .GridRow(0);
    }

    VisualNode RenderSearchBox()
    {
        return new Grid("56", "*")
        {
            new CanvasView
            {
                new DropShadow
                {
                    new Box
                    {
                        new Align
                        {
                            new Picture("Contentics.Resources.Images.search_small.png")
                        }
                        .Width(16)
                        .Height(16)
                        .HStart()
                        .Margin(16,20,0,20)
                    }
                    .CornerRadius(12)
                    .BackgroundColor(ThemeBrushes.White)
                    .Margin(16,0,16,0)
                }
            },

            new Entry()
                .BackgroundColor(ThemeBrushes.White)
                .PlaceholderColor(ThemeBrushes.Gray100)
                .Placeholder("Search for event")
                .TextColor(ThemeBrushes.Gray100)
                .Margin(new Thickness(48 + 16,8))
        }
        .Margin(0,16)
        .GridRow(1);
    }

    VisualNode RenderTopics()
    {
        var renderTopicItem = (string title) => 
        {
            return new Frame
            {
                new HStack(spacing: 8)
                {
                    new Frame
                    {
                        new Image($"{title.Replace("-", string.Empty)}.png")
                            .HeightRequest(16)
                            .HeightRequest(16)
                            .VCenter()
                            .HCenter()
                    }
                    .Padding(0)
                    .CornerRadius(99)
                    .HeightRequest(32)
                    .WidthRequest(32)
                    .BackgroundColor(ThemeBrushes.Gray)
                    .HasShadow(false),

                    new Label(title)
                        .VerticalTextAlignment(TextAlignment.Center)
                        .FontSize(16)
                        .FontAttributes(MauiControls.FontAttributes.Bold)
                        .Margin(0,0,4,0)
                }
            }
            .Padding(8)
            .HasShadow(false)
            .BorderColor(ThemeBrushes.White)
            .CornerRadius(99)
            .BackgroundColor(ThemeBrushes.White);
        };

        return new ScrollView
        {
            new HStack(spacing: 8)
            {
                renderTopicItem("Marketing"),
                renderTopicItem("Productivity"),
                renderTopicItem("E-commerce")
            }
        }
        .HeightRequest(48)
        .Orientation(ScrollOrientation.Horizontal)
        .GridRow(2)
        .Padding(16,0);
    }

    VisualNode RenderCategories()
    {
        var renderCategoryItem = (EventsCategory category) =>
        {
            return new Label(category)
                .FontSize(16)
                .FontAttributes(Microsoft.Maui.Controls.FontAttributes.Bold)
                .TextColor(()=>State.Category == category ? ThemeBrushes.Dark : ThemeBrushes.Gray100);
        };

        return new ScrollView
        {
            new HStack(spacing: 32)
            {
                renderCategoryItem(EventsCategory.Featured),
                renderCategoryItem(EventsCategory.Upcoming),
                renderCategoryItem(EventsCategory.Trending),
                renderCategoryItem(EventsCategory.Popular),
            }
        }
        .HeightRequest(20)
        .Orientation(ScrollOrientation.Horizontal)
        .GridRow(3)
        .Margin(16, 32, 16, 16);
    }

    VisualNode RenderEvents()
    {
        return new ScrollView
        {
            new VStack(8)
            {
                Models.EventModel.Featured.Select(RenderEventItem)
            }
        }
        .Orientation(ScrollOrientation.Vertical)
        .VStart()
        .GridRow(4)
        .Padding(16, 0);
    }

    VisualNode RenderEventItem(EventModel newsItem)
    {
        return new CanvasView
        {
            new Box
            {
                new Column("133, 80, 32, *")
                {
                    new ClipRectangle
                    {
                        new Picture($"Contentics.Resources.Images.{newsItem.ImageSource}")
                            .Aspect(Aspect.Fill)
                    }
                    .CornerRadius(8),

                    new Row("*, 24")
                    {
                        new Text(newsItem.Title)
                            .FontWeight(FontWeights.Bold)
                            .FontColor(ThemeBrushes.Dark)
                            .FontSize(16),

                        new Align
                        {
                            new Picture($"Contentics.Resources.Images.fav.png")
                        }
                        .Height(24)
                        .VStart()
                    }
                    .Margin(0,16,0,24),

                    new Row("32, *, 70, 28")
                    {
                        new Align
                        {
                            new Picture($"Contentics.Resources.Images.{newsItem.AvatarImage}")
                        }
                        .VCenter()
                        .Height(32),

                        new Column()
                        {
                            new Text($"{newsItem.Author}")
                                .FontSize(12)
                                .VerticalAlignment(VerticalAlignment.Center)
                                .FontColor(ThemeBrushes.Dark),

                            new Text($"{newsItem.Date.ToLongDateString()}")
                                .FontSize(12)
                                .VerticalAlignment(VerticalAlignment.Center)
                                .FontColor(ThemeBrushes.Gray100)
                        }
                        .Margin(8, 0),

                        new Align
                        {
                            new Group
                            {
                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo1_circle.png")
                                }
                                .HStart()
                                .Width(30),
                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo2_circle.png")
                                }
                                .HStart()
                                .Width(30)
                                .Margin(19,0,0,0),
                                new Align
                                {
                                    new Picture($"Contentics.Resources.Images.photo3_circle.png")
                                }
                                .HStart()
                                .Width(30)
                                .Margin(19+19,0,0,0)
                            }
                        }
                        .VCenter()
                        .Height(30),

                        new Text("+32")
                            .FontColor(ThemeBrushes.Gray100)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .HorizontalAlignment(HorizontalAlignment.Right)
                    },

                    new Row
                    {
                        new PointIterationHandler
                        {
                            new Box
                            { 
                                new Row("12, *")
                                {
                                    new Picture($"Contentics.Resources.Images.fav_small.png"),

                                    new Text("Interested")
                                        .FontWeight(FontWeights.Bold)
                                        .FontSize(15)
                                        .FontColor(ThemeBrushes.Purple10)
                                        .VerticalAlignment(VerticalAlignment.Center)
                                        .Margin(10,0,0,0)   
                                }
                                .Margin(32,9)
                            }
                            .CornerRadius(8)
                            .BackgroundColor(ThemeBrushes.Purple50)
                            .Margin(0,0,4,0)
                        },
                        new PointIterationHandler
                        {
                            new Box
                            {
                                new Text("Join to event")
                                    .FontWeight(FontWeights.Bold)
                                    .FontSize(15)
                                    .FontColor(ThemeBrushes.White)
                                    .VerticalAlignment(VerticalAlignment.Center)
                                    .HorizontalAlignment(HorizontalAlignment.Center)
                            }
                            .CornerRadius(8)
                            .BackgroundColor(ThemeBrushes.Purple10)
                            .Margin(4,0,0,0)
                        }
                    }
                    .Margin(0,16,0,0)   

                }
            }
            .Padding(16)
            .CornerRadius(16)
            .BackgroundColor(ThemeBrushes.White)
        }
        .HeightRequest(324)
        //.WidthRequest(298)
        ;
    }

}
