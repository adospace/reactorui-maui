using MauiReactor.Shapes;
using MauiReactor.WeatherTwentyOne.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages.Components
{
    class CurrentWidget : Component
    {
        private double? _width;

        public CurrentWidget Width(double width)
        {
            _width = width;
            return this;
        }

        public override VisualNode Render()
        {
            return new VerticalStackLayout
            {
                new Image("weather_partly_cloudy_day.png")
                    .WidthRequest(200)
                    .HeightRequest(200)
                    .HCenter()
                    .Aspect(Aspect.AspectFit),

                new Label("52℉")
                    .HCenter()
                    .Class("Title1"),

                new Border
                {
                    new Label("Clear")
                        .HCenter()
                        .FontSize(18)
                }
                .BackgroundColor(ThemeColors.Primary)
                .Stroke(Brush.Transparent)
                .StrokeThickness(1.0)
                .HCenter()
                .Padding(15,4)
                .StrokeShape(new RoundRectangle().CornerRadius(60.0))
            }
            .When(_width.HasValue, _ => _.WidthRequest(_width.Value));
        }
    }
}
