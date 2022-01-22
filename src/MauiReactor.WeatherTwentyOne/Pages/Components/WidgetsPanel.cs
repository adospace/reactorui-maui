using MauiReactor.WeatherTwentyOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using MauiReactor.Compatibility;
using MauiReactor.WeatherTwentyOne.Resources.Styles;

namespace MauiReactor.WeatherTwentyOne.Pages.Components
{
    class WidgetsPanelState : IState
    {
        public Metric[] Metrics { get; set; } = new[]
        {
            new Metric{ Title="Humidity", Icon="humidity_icon.png", WeatherStation="Pond Elementary", Value="78%"},
            new Metric{ Title="Rain", Icon="rain_icon.png", WeatherStation="Pond Elementary", Value="0.2in"},
            new Metric{ Title="Chance of Rain", Icon="umbrella_icon.png", WeatherStation="Pond Elementary", Value="2%"},
            new Metric{ Title="Wind", Icon="wind_icon.png", WeatherStation="Pond Elementary", Value="9mph"},
            new Metric{ Title="Humidity", Icon="humidity_icon.png", WeatherStation="City Hall", Value="78%"},
            new Metric{ Title="Rain", Icon="rain_icon.png", WeatherStation="Rockwood Reservation", Value="0.2in"},
            new Metric{ Title="Chance of Rain", Icon="umbrella_icon.png", WeatherStation="County Library", Value="2%"},
        };
    }

    class WidgetsPanel : Component<WidgetsPanelState>
    {
        public override VisualNode Render()
        {
            return new Grid("*", "1,*")
            {
                new BoxView()
                    .BackgroundColor(ThemeColors.Background_Mid)
                    .WidthRequest(1)
                    .HStart(),

                new CollectionView()
                    .ItemsLayout(new VerticalGridItemsLayout(span: 3)
                        .HorizontalItemSpacing(8)
                        .VerticalItemSpacing(8))
                    .ItemsSource(State.Metrics, RenderMetric)
                    .GridColumn(1)
                    .Margin(5)
            };
        }

        private VisualNode RenderMetric(Metric metric)
        {
            return new Frame
            {
                new Grid("*", "*")
                {
                    new Image(metric.Icon)
                        .HeightRequest(45)
                        .WidthRequest(45)
                        .Aspect(Aspect.AspectFit)
                        .VStart()
                        .HEnd(),

                    new StackLayout
                    {
                        new Label(metric.Value)
                            .Class("LargeTitle"),
                        new Label("From")
                            .Class("Subhead"),
                        new Label(metric.WeatherStation)
                            .Class("SubContent")
                    }
                    .HStart()
                    .Spacing(0)
                    .VEnd()
                }
                .Margin(20)
                .OnTapped(OnTapped)
            }
            .HeightRequest(154)
            .WidthRequest(154)
            .Padding(0)
            .CornerRadius(20)
            .HasShadow(false)
            .BackgroundColor(Application.Current?.RequestedTheme == OSAppTheme.Dark ? ThemeColors.Background_Mid : ThemeColors.LightGray);
        }

        async void OnTapped(object? sender, EventArgs eventArgs)
        {
            var g = (sender as Microsoft.Maui.Controls.Grid) ?? throw new InvalidOperationException();

            await g.FadeTo(0, 200);
            await g.FadeTo(0.5, 100);
            await g.FadeTo(0, 100);
            await g.FadeTo(0.3, 100);
            await g.FadeTo(0, 100);

            await Task.Delay(1000);

            await g.FadeTo(1, 400);

        }
    }
}
