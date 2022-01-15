using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    internal class ShellPage : Component
    {
        public override VisualNode Render()
        {
            return new Shell
            {
                new TabBar
                { 
                    new Tab("Home", "tab_home.png")
                    { 
                        new HomePage()
                    },
                    new Tab("Favorites", "tab_favorites.png")
                    {
                        new FavoritesPage()
                    },
                    new Tab("Map", "tab_map.png")
                    {
                        new MapPage()
                    },
                    new Tab("Settings", "tab_settings.png")
                    {
                        new SettingsPage()
                    }


                }
            }
            .FlyoutWidth(68.0)
            .FlyoutBackgroundColor(Colors.Red)
            .FlyoutBehavior(Device.Idiom == TargetIdiom.Phone ? FlyoutBehavior.Disabled : FlyoutBehavior.Locked);
        }


    }
}
