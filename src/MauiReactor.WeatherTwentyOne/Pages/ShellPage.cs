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
            if (Device.Idiom == TargetIdiom.Phone)
            {
                return RenderPhoneLayout();
            }

            return RenderDesktopLayout();
        }

        static VisualNode RenderPhoneLayout()
            => new Shell
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
            .ItemTemplate(RenderItemTemplate)
            .FlyoutWidth(68.0)
            .FlyoutBackgroundColor(Colors.Red)
            .FlyoutBehavior(FlyoutBehavior.Disabled);

        static VisualNode RenderDesktopLayout()
            => new Shell
            {
                new FlyoutItem("Home", "tab_home.png")
                {
                    new HomePage()
                },
                new FlyoutItem("Favorites", "tab_favorites.png")
                {
                    new FavoritesPage()
                },
                new FlyoutItem("Map", "tab_map.png")
                {
                    new MapPage()
                },
                new FlyoutItem("Settings", "tab_settings.png")
                {
                    new SettingsPage()
                }
            }
            .ItemTemplate(RenderItemTemplate)
            .FlyoutWidth(68.0)
            .FlyoutBackgroundColor(Colors.Red)
            .FlyoutBehavior(FlyoutBehavior.Locked);


        static VisualNode RenderItemTemplate(Microsoft.Maui.Controls.BaseShellItem item)
            => new Grid("68", "68")
            {
                new Image()
                    .Source(item.FlyoutIcon)
                    .VCenter()
                    .HCenter()
            };
    }
}
