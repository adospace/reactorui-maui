using System;
using System.Linq;
using Contentics.Models;
using Contentics.Resources.Styles;
using MauiReactor;
using MauiReactor.Canvas;
using MauiReactor.Compatibility;

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
        return new Grid("164, Auto, Auto, Auto, *", "*")
        {
            RenderTopPanel(),
            
            RenderSearchBox(),

            RenderTopics(),

            RenderCategories(),

            RenderEvents()

        }
        .Margin(0,0,0,88);
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
            }
            .BackgroundColor(Colors.Transparent),

            new Entry()
                .BackgroundColor(ThemeBrushes.White)
                .PlaceholderColor(ThemeBrushes.Grey100)
                .Placeholder("Search for event")
                .TextColor(ThemeBrushes.Grey100)
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
                        new Image($"{title.ToLowerInvariant().Replace("-", string.Empty)}.png")
                            .HeightRequest(16)
                            .HeightRequest(16)
                            .VCenter()
                            .HCenter()
                    }
                    .Padding(0)
                    .CornerRadius(16)
                    .HeightRequest(32)
                    .WidthRequest(32)
                    .BackgroundColor(ThemeBrushes.Grey)
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
            .CornerRadius(16)
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
                .TextColor(()=>State.Category == category ? ThemeBrushes.Dark : ThemeBrushes.Grey100);
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

    VisualNode RenderEvents() => new EventsComponent();

}

class EventsComponent : Component
{
    private MauiControls.ScrollView? _scrollViewRef;

    public override VisualNode Render()
    {
        return new ScrollView(scrollViewRef => _scrollViewRef = scrollViewRef)
        {
            new VStack(8)
            {
                EventModel.Featured.Select((eventModel, index) => RenderEventItem(eventModel, index))
            }
        }
        .Orientation(ScrollOrientation.Vertical)
        .GridRow(4)
        .Padding(16, 0);
    }

    VisualNode RenderEventItem(EventModel eventModel, int index)
    {
        return new EventComponent()
            .Model(eventModel)
            .OnEventSelected(()=> OnEventSelected(eventModel, index));
    }

    private async void OnEventSelected(EventModel eventModel, int index)
    {
        if (_scrollViewRef != null && Navigation != null)
        {
            var scrollViewBounds = _scrollViewRef.BoundsToScreenSize();
            var scrollViewY = _scrollViewRef.ScrollY;
            var sourceRect = new Rect(
                new Point(scrollViewBounds.X + 32, 
                    /*ScrollView ScreenBound.Y*/ scrollViewBounds.Y
                    /*ScrollView Padding*/ + 16
                    /*ElementPosition inside the scoll view */ + index * 324
                    /*Spacing*/ + index * 8
                    /*Scroll position*/ - scrollViewY),

                new Size(scrollViewBounds.Width - 64, 133));

            await Navigation.PushAsync<EventDetails, EventDetailsPageProps>(props => 
            {
                props.Model = eventModel;
                props.SourceRect = sourceRect;
            });
        }
    }
}

class EventComponent : Component
{
    private EventModel? _model;
    private Action? _selectedAction;

    public EventComponent Model(EventModel model)
    {
        _model = model;
        return this;
    }

    public EventComponent OnEventSelected(Action? selectedAction)
    {
        _selectedAction = selectedAction;
        return this;
    }

    public override VisualNode Render()
    {
        return new CanvasView
        {
            RenderInternal()
        }
        .BackgroundColor(Colors.Transparent)
        .HeightRequest(324)
        ;
    }

    VisualNode? RenderInternal()
    {
        if (_model == null)
        {
            return null;
        }

        return new Box
        {
            new Column("133, 80, 32, *")
            {

                new PointIterationHandler
                {
                    new ClipRectangle()
                    {
                        new Picture($"Contentics.Resources.Images.{_model.ImageSource}")
                            .Aspect(Aspect.Fill)
                    }
                    .CornerRadius(8),
                }
                .OnTap(_selectedAction),

                new Row("*, 24")
                {
                    new Text(_model.Title)
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
                        new Picture($"Contentics.Resources.Images.{_model.AvatarImage}")
                    }
                    .VCenter()
                    .Height(32),

                    new Column()
                    {
                        new Text($"{_model.Author}")
                            .FontSize(12)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .FontColor(ThemeBrushes.Dark),

                        new Text($"{_model.Date.ToLongDateString()}")
                            .FontSize(12)
                            .VerticalAlignment(VerticalAlignment.Center)
                            .FontColor(ThemeBrushes.Grey100)
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
                        .FontColor(ThemeBrushes.Grey100)
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
                                    .HorizontalAlignment(HorizontalAlignment.Left)
                                    .Margin(10,0,0,0)
                            }
                            .Margin(32,0)
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
                    .OnTap(_selectedAction)
                }
                .Margin(0,16,0,0)

            }
        }
        .Padding(16)
        .CornerRadius(16)
        .BackgroundColor(ThemeBrushes.White);
    }
}

