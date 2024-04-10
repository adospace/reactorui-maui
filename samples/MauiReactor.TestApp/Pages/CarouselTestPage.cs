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
                .IndicatorVisualState("CommonStates", "Normal", MauiControls.Label.TextColorProperty, Colors.Black)
                .IndicatorVisualState("CommonStates", "Selected", MauiControls.Label.TextColorProperty, Colors.White)
                .GridRow(1),
            }
        }
        .BackgroundColor(Colors.White);
    }
}

public class TestModel
{
    public string? Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
}

class CarouselTestPageWithImagesState
{
    public List<TestModel> Models { get; set; } = new();

    public TestModel? SelectedModel { get; set; }

}

class CarouselTestWithImagesPage : Component<CarouselTestPageWithImagesState>
{
    private MauiControls.CarouselView? _carouselView;

    protected override void OnMounted()
    {
        State.Models = new List<TestModel>()
        {
            new TestModel
            {
                Title = "Lorem Ipsum Dolor Sit Amet",
                SubTitle = "Consectetur Adipiscing Elit",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in libero non quam accumsan sodales vel vel nunc."
            },
            new TestModel
            {
                Title = "Praesent Euismod Tristique",
                SubTitle = "Vestibulum Fringilla Egestas",
                Description = "Praesent euismod tristique vestibulum. Vivamus vestibulum justo in massa aliquam, id facilisis odio feugiat. Duis euismod massa id elit imperdiet."
            },
            new TestModel
            {
                Title = "Aliquam Erat Volutpat",
                SubTitle = "Aenean Feugiat In Mollis",
                Description = "Aliquam erat volutpat. Aenean feugiat in mollis ac. Nullam eget justo ut orci dictum auctor."
            },
            new TestModel
            {
                Title = "Suspendisse Tincidunt",
                SubTitle = "Faucibus Ligula Quis",
                Description = "Suspendisse tincidunt, arcu eget auctor efficitur, nulla justo tristique neque, et fermentum orci ante eget nunc."
            },
        };
        
        State.SelectedModel = State.Models.First();

        base.OnMounted();
    }

    public override VisualNode Render()
    {
        return new ContentPage("Custom IndicatorView Test Page")
        {
            new VStack
            {
                new Grid("* 40", "*")
                {
                    new CarouselView(r => _carouselView = r)
                        .HeightRequest(450)
                        .HorizontalScrollBarVisibility(ScrollBarVisibility.Never)
                        .ItemsSource(State.Models, _=> new Grid("*", "*")
                        {
                            new VStack
                            {
                                  new Label(_.Title)
                                    .Margin(0,5),
                                  new Label(_.SubTitle)
                                    .Margin(0,5),
                                 new Label(_.Description)
                                    .Margin(0,8)
                            }
                        })
                        .CurrentItem(() => State.SelectedModel!)
                        .OnCurrentItemChanged((s, args) => SetState(s => s.SelectedModel = (TestModel)args.CurrentItem))
                        ,

                    new HStack(spacing: 5)
                    {
                        State.Models.Select(item => 
                            new Image(State.SelectedModel == item ? "tab_home.png" : "tab_map.png")
                                .WidthRequest(20)
                                .HeightRequest(20)
                                .OnTapped(()=>SetState(s=>s.SelectedModel = item, false))
                            )
                    }
                    .HCenter()
                    .VCenter()
                    .GridRow(1)
                }
            }
            .Padding(8,0)
            .VCenter()
        };
    }
}