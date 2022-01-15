using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    internal class SettingsPage : Component
    {
        public override VisualNode Render()
        {
            return new ContentPage(title: "SettingsPage")
            {
                new Label("SettingsPage")
            };
        }
    }
}
