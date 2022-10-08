using MauiReactor.WeatherTwentyOne.Pages.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    internal class HomePage : Component
    {
        public override VisualNode Render()
        {
            return new ContentPage(title: "Redmond, WA")
            {
                DeviceInfo.Idiom == DeviceIdiom.Desktop  ?
                RenderDesktopLayout()
                :
                RenderPhoneLayout()
            };
        }

        private VisualNode RenderPhoneLayout()
        {
            return new ScrollView
            {
                new VerticalStackLayout
                {
                    new CurrentWidget(),

                    new BoxView()
                        .HeightRequest(1),

                    new Next24HrWidget(),

                    new Next7DWidget()
                }
                .Padding(0,50)
                .Spacing(25)
            };
        }

        private VisualNode RenderDesktopLayout()
            => new Grid(
                rows: "*",
                columns: "*,500")
                {
                    new ScrollView
                    {
                        new VerticalStackLayout
                        {
                            new FlexLayout
                            {
                                new CurrentWidget()
                                    .Width(200),

                                new WindLiveWidget(),
                            }
                            .MinimumHeightRequest(360)
                            .AlignItems(Microsoft.Maui.Layouts.FlexAlignItems.Center)
                            .AlignContent(Microsoft.Maui.Layouts.FlexAlignContent.Center)
                            .JustifyContent(Microsoft.Maui.Layouts.FlexJustify.SpaceEvenly),

                            new BoxView()
                                .HeightRequest(1),

                            new Next24HrWidget(),

                            new Next7DWidget()
                        }
                        .Padding(0,50)
                        .Spacing(50)
                    },

                    new WidgetsPanel()
                        .GridColumn(1)
                };

    }
}
