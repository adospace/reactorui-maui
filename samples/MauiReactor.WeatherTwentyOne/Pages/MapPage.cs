using MauiReactor.WeatherTwentyOne.Pages.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    internal class MapPage : Component
    {
        public override VisualNode Render()
        {
            return new ContentPage(title: "Wind Map")
            {
                Device.Idiom == TargetIdiom.Phone ?
                RenderPhoneLayout()
                :
                RenderDesktopLayout()
            };
        }

        static VisualNode RenderPhoneLayout()
            => new WebView("https://embed.windy.com");

        static VisualNode RenderDesktopLayout()
            => new Grid("*", "*,480")
            {
                new WebView("https://embed.windy.com"),

                new WidgetsPanel()
                    .GridColumn(1)
            };
    }
}