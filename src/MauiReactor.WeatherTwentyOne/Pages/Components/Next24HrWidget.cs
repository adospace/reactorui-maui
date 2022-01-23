using MauiReactor.WeatherTwentyOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages.Components
{
    class Next24HrWidgetState : IState
    {
        public Forecast[] Hours { get; } = new []
        {
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(1),
                Day = new Day{ Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 47 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(2),
                Day = new Day{ Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 47 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            }
            ,
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(3),
                Day = new Day{ Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 48 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            }
            ,
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(4),
                Day = new Day{ Phrase = "fluent_weather_rain_showers_day_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 49 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            }
            ,
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(5),
                Day = new Day{ Phrase = "fluent_weather_cloudy_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 52 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(6),
                Day = new Day{ Phrase = "fluent_weather_cloudy_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 53 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(7),
                Day = new Day{ Phrase = "fluent_weather_cloudy_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 58 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(8),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 63 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(9),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 64 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(10),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 65 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(11),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 68 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(12),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 68 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(13),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 68 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(14),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 65 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(15),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 63 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(16),
                Day = new Day{ Phrase = "fluent_weather_sunny_20_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 60 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(17),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 58 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(18),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 54 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(19),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 53 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(20),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 52 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(21),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 50 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(22),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 47 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            },
            new Forecast
            {
                DateTime = DateTime.Now.AddHours(23),
                Day = new Day{ Phrase = "fluent_weather_moon_16_filled" },
                Temperature = new Temperature{ Minimum = new Minimum{ Unit = "F", Value = 47 }, Maximum = new Maximum { Unit = "F", Value = 67 } },
            }
        };
    }

    internal class Next24HrWidget : Component<Next24HrWidgetState>
    {
        public override VisualNode Render()
        {
            return new VerticalStackLayout
            {
                new Label("Next 24 Hours")
                    .Margin(Device.Idiom == TargetIdiom.Phone ? new Thickness(15,0) : new Thickness(25,0))
                    .Class("SectionTitle"),

                new HorizontalScrollView
                {
                    new HorizontalStackLayout
                    {
                        State.Hours.Select(forecast => new StackLayout
                        {
                            new Label(forecast.DateTime.ToString("h tt"))
                                .Class("Small")
                                .HCenter(),

                            new Image($"{forecast.Day.Phrase}.png")
                                .HeightRequest(40)
                                .WidthRequest(40)
                                .HCenter(),

                            new Label($"{forecast.Temperature.Minimum.Value:F0}°")
                                .Class("Subhead")
                                .HCenter()
                        })
                    }
                    .Spacing(12)
                    .Padding(Device.Idiom == TargetIdiom.Phone ? new Thickness(15, 0) : new Thickness(25,0))
                }
            };
        }
    }
}
