using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using Microsoft.Maui.Graphics;

namespace MauiReactor.Calendar.Pages;

class MainPageState
{
    public DateTime? Date { get; set; }

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
                    Theme.Button(State.Date == null ? "Select Date" : State.Date.Value.ToShortDateString())
                        .OnClicked(()=>SetState(s => s.IsCalendarVisible = true))
                        .HCenter()
                }
                .VCenter(),

                new Calendar()
                    .Date(State.Date)
                    .OnDateSelected(newDate => SetState(s =>
                    {
                        s.Date = newDate;
                        s.IsCalendarVisible = false;
                    }))
                    .IsVisible(State.IsCalendarVisible),
            }
        };
    }
}

class CalendarState 
{
    public DateTime ShowingDate { get; set; }
    public double Opacity { get; set; } = 0.0;
    public double TranslateY { get; set; }

}

class Calendar : Component<CalendarState>
{
    private DateTime? _date;
    private bool _isVisible;
    private Action<DateTime>? _selectedDateAction;

    public Calendar Date(DateTime? date)
    {
        _date = date;
        return this;
    }

    public Calendar IsVisible(bool isVisible)
    {
        _isVisible = isVisible;
        return this;
    }

    public Calendar OnDateSelected(Action<DateTime> selectedDateAction)
    {
        _selectedDateAction = selectedDateAction;
        return this;
    }

    protected override void OnMounted()
    {
        State.ShowingDate = _date ?? DateTime.Now;
        State.Opacity = _isVisible ? 1.0 : 0.1;
        State.TranslateY = _isVisible ? 0.0 : 30;
        base.OnMounted();
    }

    protected override void OnPropsChanged()
    {
        State.ShowingDate = _date ?? DateTime.Now;
        State.Opacity = _isVisible ? 1.0 : 0.1;//Opacity on frame has some problems?!
        State.TranslateY = _isVisible ? 0.0 : 30;

        base.OnPropsChanged();
    }

    public override VisualNode Render()
    {
        return new Border
        {
            new Grid("Auto * Auto", "334")
            {
                new Grid("32", "32 * 32")
                {
                    Theme.ImageButton("left.png")
                        .OnClicked(()=>SetState(s => s.ShowingDate = s.ShowingDate.AddMonths(-1))),

                    new Label(State.ShowingDate.ToString("MMMM yyyy"))
                        .TextColor(Theme.BlackColor)
                        .FontSize(18)
                        .FontAttributes(MauiControls.FontAttributes.Bold)
                        .VCenter()
                        .HCenter()
                        .GridColumn(1),

                    Theme.ImageButton("right.png")
                        .OnClicked(()=>SetState(s => s.ShowingDate = s.ShowingDate.AddMonths(+1)))
                        .GridColumn(2)
                }
                .Margin(24),

                new GraphicsView()
                    .HeightRequest(256)
                    .WidthRequest(286)
                    .OnDraw(OnDraw)
                    .GridRow(1)
                    .OnEndInteraction((sender, args)=> OnTap(args)),

                new HorizontalScrollView
                {
                    new HorizontalStackLayout
                    {
                        Theme.Button("Today")
                            .OnClicked(()=>_selectedDateAction?.Invoke(DateTime.Today)),
                        Theme.Button("Next Week")
                            .OnClicked(()=>_selectedDateAction?.Invoke(DateTime.Today.AddDays(7))),
                        Theme.Button("Next Month")
                            .OnClicked(()=>_selectedDateAction?.Invoke(DateTime.Today.AddMonths(1)))
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
        .StrokeCornerRadius(8)
        .VCenter()
        .HCenter()
        .BackgroundColor(Theme.WhiteColor)
        .IsVisible(_isVisible)
        .Opacity(State.Opacity)
        .TranslationY(State.TranslateY)
        .WithAnimation(Easing.CubicOut)
        ;
    }

    private void OnDraw(ICanvas canvas, RectF dirtyRect)
    {
        var selectedDate = State.ShowingDate;
        var currentDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);

        currentDate = currentDate.AddDays(-(currentDate.DayOfWeek - DayOfWeek.Sunday));

        canvas.Font = new Microsoft.Maui.Graphics.Font("Arial");
        canvas.FontSize = 14;

        float x = 0;
        float y = 0;
        SizeF cellSize = new(32, 20);
        canvas.FontColor = Theme.GrayColor;

        foreach (var dayOfWeek in new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday })
        {
            canvas.DrawString(dayOfWeek.ToString()[..3], x, y, cellSize.Width, cellSize.Height, HorizontalAlignment.Center, VerticalAlignment.Center);
            x += cellSize.Width + 10;
        }

        x = 0;
        y += cellSize.Height + 20;
        cellSize = new(32, 32);
        for (int i = 0; i < 35; i++)
        {
            currentDate = currentDate.AddDays(1);

            if (currentDate == (_date ?? DateTime.Today))
            {
                canvas.FillColor = Theme.PrimaryColor;
                canvas.FillRoundedRectangle(new RectF(new PointF(x, y), cellSize), 8);

                canvas.StrokeColor = Theme.PrimaryDarkColor;
                canvas.StrokeSize = 1;
                canvas.DrawRoundedRectangle(new RectF(new PointF(x, y), cellSize), 8);

                canvas.FontColor = Theme.WhiteColor;
                canvas.DrawString(currentDate.Day.ToString(), x, y, cellSize.Width, cellSize.Height, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            else
            {
                canvas.FontColor = currentDate.Month == selectedDate.Month ? Theme.BlackColor : Theme.GrayColor;
                canvas.DrawString(currentDate.Day.ToString(), x, y, cellSize.Width, cellSize.Height, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            x += cellSize.Width + 10;

            if (i % 7 == 6)
            {
                x = 0;
                y += cellSize.Height + 10;
            }
        }

        canvas.StrokeColor = Theme.GrayLightColor;
        canvas.DrawLine(0, y + 16, 286, y + 16);

    }

    private void OnTap(MauiControls.TouchEventArgs args)
    {
        var touchPoint = args.Touches.FirstOrDefault();
        if (touchPoint != default)
        {
            var showingDate = State.ShowingDate;
            var selectedDate = new DateTime(showingDate.Year, showingDate.Month, 1);

            selectedDate = selectedDate.AddDays(-(selectedDate.DayOfWeek - DayOfWeek.Sunday) + 1);

            int col = (int)Math.Round((touchPoint.X - 5) / 42);
            int row = (int)Math.Round((touchPoint.Y - 5 - 40) / 42);

            Console.WriteLine($"x:{touchPoint.X} y: {(touchPoint.Y - 40)} row:{row} col:{col}");

            selectedDate = selectedDate.AddDays(row * 7 + col);

            _selectedDateAction?.Invoke(selectedDate);
        }
    }
}

public class Theme
{

    public static Color GrayColor { get; } = Color.FromRgba("#9C9D9F");
    public static Color GraySoftColor { get; } = Color.FromRgba("#F5F5F5");
    public static Color GrayLightColor { get; } = Color.FromRgba("#DDDDDD");
    public static Color WhiteColor { get; } = Color.FromRgba("#FFFFFF");
    public static Color BlackColor { get; } = Color.FromRgba("#000000");
    public static Color PrimaryColor { get; } = Color.FromRgba("#8840FF");
    public static Color PrimaryDarkColor { get; } = Color.FromRgba("#6139A0");



    public static Button Button(string text)
        => new Button(text)
            .TextColor(BlackColor)
            .Padding(0)
            .BackgroundColor(GraySoftColor)
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
