using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.TestApp.Pages;

class CarouselTestPage : Component
{
    private MauiControls.CarouselView? _carouselView;

    public override VisualNode Render()
    {
        return new ContentPage("Carousel Test Page")
        {
            new Grid("* 40", "*")
            {
                new CarouselView(r => _carouselView = r)
                    .ItemsSource(Enumerable.Range(1, 5), _=> new Label($"Page{_}").VCenter()),

                new IndicatorView(r =>
                {
                    if (_carouselView != null)
                    {
                        _carouselView.IndicatorView = r;
                    }
                })
                .HCenter()
                .VCenter()
                .IndicatorTemplate(() => new Label("Test"))
                .VisualState("CommonStates", "Normal", MauiControls.Label.TextColorProperty, Colors.Black)
                .VisualState("CommonStates", "Selected", MauiControls.Label.TextColorProperty, Colors.White)
                .GridRow(1),
            }
        }
        .BackgroundColor(Colors.White);
    }
}
