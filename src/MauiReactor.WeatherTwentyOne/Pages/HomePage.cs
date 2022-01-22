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
                new Grid(
                    rows: "*",
                    columns: Device.Idiom == TargetIdiom.Phone ? "*" : "*,500")
                { 
                    RenderMainContent(),

                    RenderWidgetsPanel()
                }
            };
        }

        private VisualNode RenderMainContent()
        {
            return new ScrollView
            {
                new VerticalStackLayout
                {
                    Device.Idiom != TargetIdiom.Phone ? 
                        new FlexLayout
                        { 
                            new CurrentWidget()
                                .Width(200),

                            new WindLiveWidget(),
                        }
                        .MinimumHeightRequest(360)
                        .AlignItems(Microsoft.Maui.Layouts.FlexAlignItems.Center)
                        .AlignContent(Microsoft.Maui.Layouts.FlexAlignContent.Center)
                        .JustifyContent(Microsoft.Maui.Layouts.FlexJustify.SpaceEvenly)
                    :
                    new CurrentWidget(),

                    new BoxView()
                        .HeightRequest(1),

                    new Next24HrWidget(),

                    new Next7DWidget()
                }
                .Padding(Device.Idiom == TargetIdiom.Phone ? new Thickness(0,50) : new Thickness(0,50))
                .Spacing(Device.Idiom == TargetIdiom.Phone ? 25 : 50)
            };
        }

        private VisualNode? RenderWidgetsPanel()
        {
            if (Device.Idiom == TargetIdiom.Phone)
                return null;

            return new WidgetsPanel()
                .GridColumn(1);    
        }
    }
}
