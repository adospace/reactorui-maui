using MauiReactor.WeatherTwentyOne.Models;
using MauiReactor.WeatherTwentyOne.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages.Components
{
    class Next7DWidgetState
    { 
        public Forecast[] Week { get; } = new []
        {
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(1),
                Day = new Day{ Phrase = "fluent_weather_sunny_high_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 52 }, Maximum = new Maximum { Unit = "F", Value = 77 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(2),
                Day = new Day { Phrase = "fluent_weather_partly_cloudy" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 61 }, Maximum = new Maximum { Unit = "F", Value = 82 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(3),
                Day = new Day { Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 62 }, Maximum = new Maximum { Unit = "F", Value = 77 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(4),
                Day = new Day { Phrase = "fluent_weather_thunderstorm_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 57 }, Maximum = new Maximum { Unit = "F", Value = 80 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(5),
                Day = new Day { Phrase = "fluent_weather_thunderstorm_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 49 }, Maximum = new Maximum { Unit = "F", Value = 61 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(6),
                Day = new Day { Phrase = "fluent_weather_partly_cloudy" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 49 }, Maximum = new Maximum { Unit = "F", Value = 68 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(7),
                Day = new Day { Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 47 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(1),
                Day = new Day { Phrase = "fluent_weather_sunny_high_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 52 }, Maximum = new Maximum { Unit = "F", Value = 77 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(2),
                Day = new Day { Phrase = "fluent_weather_partly_cloudy" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 61 }, Maximum = new Maximum { Unit = "F", Value = 82 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(3),
                Day = new Day { Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 62 }, Maximum = new Maximum { Unit = "F", Value = 77 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(4),
                Day = new Day { Phrase = "fluent_weather_thunderstorm_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 57 }, Maximum = new Maximum { Unit = "F", Value = 80 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(5),
                Day = new Day { Phrase = "fluent_weather_thunderstorm_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 49 }, Maximum = new Maximum { Unit = "F", Value = 61 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(6),
                Day = new Day { Phrase = "fluent_weather_partly_cloudy" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 49 }, Maximum = new Maximum { Unit = "F", Value = 68 } },
            },
            new Forecast
            {
                DateTime = DateTime.Today.AddDays(7),
                Day = new Day { Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature { Minimum = new Minimum { Unit = "F", Value = 47 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            }
        };
    }

    class Next7DWidget : Component<Next7DWidgetState>
    {
        public override VisualNode Render()
        {
            return new VerticalStackLayout
            {
                new Label("Daily Forecasts")
                    .Margin(DeviceInfo.Current.Idiom == DeviceIdiom.Phone ? new Thickness(15,0) : new Thickness(25, 0))
                    .Class("SectionTitle"),

                new HorizontalScrollView
                { 
                    new HorizontalStackLayout
                    { 
                        State.Week.Select(RenderForecast)                    
                    }
                    .Spacing(12)
                    .Padding(DeviceInfo.Current.Idiom == DeviceIdiom.Phone ? new Thickness(15,0) : new Thickness(25, 0))
                }
            };
        }

        private VisualNode RenderForecast(Forecast forecast)
        {
            return new VerticalStackLayout
            {
                new Label(forecast.DateTime.ToString("ddd"))
                    .Class("Subhead")
                    .HeightRequest(34)
                    .HCenter(),

                new Label($"{forecast.Temperature.Minimum.Value:F0}°")
                    .Class("Subhead")
                    .HeightRequest(34)
                    .HCenter(),

                new Image($"{forecast.Day.Phrase}.png")
                    .WidthRequest(34)
                    .HeightRequest(34)
                    .HCenter(),

                new BoxView()
                    .WidthRequest(1)
                    .HeightRequest(1)
                    .Margin(GetMaxTempOffset(forecast)),

                new Label($"{forecast.Temperature.Maximum.Value:F0}°")
                    .Class("Subhead")
                    .HCenter()
                    .HeightRequest(20),

                new BoxView()
                    .BackgroundColor(ThemeColors.Accent_Blue100)
                    .HeightRequest((forecast.Temperature.Maximum.Value - forecast.Temperature.Minimum.Value)*3)
                    .WidthRequest(10)
                    .CornerRadius(5)
                    .HCenter(),

                new Label($"{forecast.Temperature.Minimum.Value:F0}°")
                    .Class("Subhead")
                    .HCenter()
                    .HeightRequest(20),

                new BoxView()
                    .WidthRequest(1)
                    .HeightRequest(1)
                    .Margin(GetMinTempOffset(forecast)),

                new BoxView()
                    .Class("HRule"),

                new HorizontalStackLayout
                {
                    new Image("sm_solid_umbrella.png")
                        .WidthRequest(16)
                        .HeightRequest(16)
                        .VCenter(),

                    new Label("13%")
                        .Class("SubContent")
                        .VCenter()
                }
                .HeightRequest(40)
                .HCenter()
            };
        }

        private static Thickness GetMaxTempOffset(Forecast forecast)
        {
            const double max = 90 * 3;

            var maxTemp = forecast.Temperature.Maximum.Value * 3;
            var topMargin = max - maxTemp;

            return new Thickness(0, topMargin, 0, 0);
        }

        private static Thickness GetMinTempOffset(Forecast forecast)
        {
            const double min = 40 * 3;

            var minTemp = forecast.Temperature.Minimum.Value * 3;
            var bottomMargin = minTemp - min;

            return new Thickness(0, 0, 0, bottomMargin);
        }
 
    }
}
