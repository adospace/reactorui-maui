using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor;
using MauiReactor.WeatherTwentyOne.Resources.Styles;
using MauiReactor.Compatibility;
using Location = MauiReactor.WeatherTwentyOne.Services.Location;
using MauiReactor.Shapes;
using MauiReactor.WeatherTwentyOne.Services;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    class FavoritesPageState
    {
        public Location[] Favorites { get; set; } = Array.Empty<Location>();        
    }

    class FavoritesPage : Component<FavoritesPageState>
    {
        static readonly Color _searchBoxColor = Color.Parse("#637989");

        protected override void OnMounted()
        {
            Fetch();

            base.OnMounted();
        }

        private async void Fetch()
        {
            IWeatherService weatherService = new WeatherService(null);
            var locations = await weatherService.GetLocations(string.Empty);

            SetState(s => s.Favorites = locations.ToArray());

        }

        public override VisualNode Render()
        {
            return new ContentPage(title: "Favorites")
            {
                new ScrollView
                { 
                    new VerticalStackLayout
                    { 
                        RenderSearchBox(),

                        RenderActualContent()
                    }
                }
            };
        }

        VisualNode RenderSearchBox()
        {
            return new HorizontalStackLayout
            {
                new Image("search_icon.png")
                    .VCenter()
                    .HeightRequest(22)
                    .WidthRequest(22),

                new Label("Search")
                    .TextColor(_searchBoxColor)
                    .FontSize(18)
                    .WidthRequest(240)
                    .VCenter()
            }
            .VStart()
            .Spacing(18)
            .Padding(DeviceInfo.Idiom == DeviceIdiom.Phone ? new Thickness(15) : new Thickness(25, 25, 25, 0));
        }

        VisualNode RenderActualContent()
        {
            return new CollectionView()
                .ItemsLayout(RenderItemsLayout())
                .ItemsSource(State.Favorites, RenderFavoriteItem)
                .Margin(8,0)
                .Footer(RenderFooter());
        }

        static VerticalGridItemsLayout RenderItemsLayout()
            => new VerticalGridItemsLayout(span: 2)
                    .VerticalItemSpacing(8)
                    .HorizontalItemSpacing(8);

        private static VisualNode RenderFooter() 
            => new Border
            {
                new ImageButton("add_icon.png")
                    .VCenter()
                    .HCenter()
            }
            .HeightRequest(132)
            .WidthRequest(132)
            .HCenter()
            .Margin(DeviceInfo.Current.Idiom == DeviceIdiom.Phone ? 15 : 25)
            .Stroke(Brush.Transparent)
            .StrokeThickness(1)
            .BackgroundColor(ThemeColors.Background_Mid)
            .StrokeShape(new RoundRectangle().CornerRadius(60));

        private VisualNode RenderFavoriteItem(Location location)
        {
            return new Frame
            {
                new Grid("42,40,*", "*")
                {
                    new Image(location.Icon)
                        .WidthRequest(36)
                        .HeightRequest(36)
                        .VStart()
                        .HEnd(),

                    new Label(location.Value)
                        .Class("LargeTitle"),

                    new VStack(spacing: 0)
                    { 
                        new Label(location.Name)
                            .Class("Subhead"),
                        new Label(location.WeatherStation)
                            .Class("SubContent")
                    }
                    .GridRowSpan(2)
                    .VCenter(),

                    new HStack(spacing: 10)
                    { 
                        new Image("solid_umbrella.png")
                            .VCenter()
                            .WidthRequest(20)
                            .HeightRequest(20),

                        new Label("13%")
                            .Class("Small")
                            .VCenter()
                    }
                    .GridRow(2)
                    .VEnd(),

                    new HStack(spacing: 10)
                    { 
                        new Image("solid_humidity.png")
                            .VCenter()
                            .WidthRequest(20)
                            .HeightRequest(20),
                        new Label("45%")
                            .Class("Small")
                            .VCenter()
                    
                    }
                    .GridRow(2)
                    .HEnd()
                    .VEnd()

                }
                .Padding(20)
            }
            .Padding(0)
            .CornerRadius(20)
            .HasShadow(false)
            .BackgroundColor(Application.Current?.UserAppTheme == AppTheme.Dark ? ThemeColors.DarkGray : ThemeColors.LightGray);
        }


    }
}
