using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;

namespace MauiReactor.Calendar.Pages;

class MainPageState : IState
{
    public DateOnly? Date { get; set; }

    public bool IsCalendarVisible { get; set; }
}

class MainPage : Component<MainPageState>
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new Grid
            {
                new VerticalStackLayout
                {
                    new Button(State.Date == null ? "Select Date" : State.Date.Value.ToString())
                        .OnClicked(()=>SetState(s => s.IsCalendarVisible = true))
                        .HCenter()
                }
                .VCenter(),

                new Calendar(),
            }
        };
    }
}

class CalendarState : IState 
{
    
}

class Calendar : Component<CalendarState>
{
    private DateOnly? _date;
    private bool _isVisible;

    public Calendar Date(DateOnly? date)
    {
        _date = date;
        return this;
    }

    public Calendar IsVisible(bool isVisible)
    {
        _isVisible = isVisible;
        return this;
    }

    public override VisualNode Render()
    {
        return new Frame
        {
            new Grid("Auto * Auto", "334")
            {
                new Grid("32", "32 * 32")
                {
                    Theme.ImageButton("left.png"),


                    Theme.ImageButton("right.png").GridColumn(2)
                }
                //.BackgroundColor(Colors.Yellow)
                .Margin(24),

                new GraphicsView()
                    .HeightRequest(256)
                    .WidthRequest(286)
                    .OnDraw(OnDraw)
                    .GridRow(1),

                new HorizontalScrollView
                {
                    new HorizontalStackLayout
                    {
                        Theme.Button("Today"),
                        Theme.Button("Next Week"),
                        Theme.Button("Next Month"),
                    }
                    .Spacing(11)
                }
                .Margin(24)
                .GridRow(2)
            }
            .WidthRequest(334)
            .HeightRequest(416)
        }
        .HeightRequest(416)
        .WidthRequest(334)
        .CornerRadius(8)
        .VCenter()
        .HCenter()
        .BackgroundColor(Colors.White);
    }

    private void OnDraw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.Green;
        canvas.FillRectangle(0,0,286,256);
    }
}

public class Theme
{
    public static Color GrayColor { get; } = Color.FromRgba("#F5F5F5");
    public static Color GrayLightColor { get; } = Color.FromRgba("#DDDDDD");
    public static Color WhiteColor { get; } = Color.FromRgba("#FFFFFF");
    public static Color BlackColor { get; } = Color.FromRgba("#000000");
    public static Color PrimaryColor { get; } = Color.FromRgba("#8840FF");
    public static Color PrimaryDarkColor { get; } = Color.FromRgba("#6139A0");



    public static Button Button(string text)
        => new Button(text)
            .TextColor(BlackColor)
            .Padding(0)
            .BackgroundColor(GrayColor)
            .WidthRequest(106)
            .HeightRequest(32)
            .CornerRadius(8);

    public static ImageButton ImageButton(string imageSource)
        => new ImageButton(imageSource)
            .Aspect(Aspect.Center)
            .Padding(0)
            .BackgroundColor(WhiteColor)
            .CornerRadius(8)
            .BorderWidth(1)
            .BorderColor(GrayLightColor);
}
