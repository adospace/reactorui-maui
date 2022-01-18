using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.WeatherTwentyOne.Pages
{
    class FavoritesPageState : IState
    {
        public Location[] Favorites { get; set; } = Array.Empty<Location>();

        
    }

    class FavoritesPage : Component<FavoritesPageState>
    {
        static readonly Color _searchBoxColor = Color.Parse("#637989");

        public override VisualNode Render()
        {
            return new ContentPage(title: "FavoritesPage")
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
            .Padding(Device.Idiom == TargetIdiom.Phone ? new Thickness(15) : new Thickness(25, 25, 25, 0));
        }

        VisualNode RenderActualContent()
        {
            return new CollectionView()
                .ItemsSource(State.Favorites, RenderFavoriteItem)
                .Margin(new Thickness(8,0));
        }

        private VisualNode RenderFavoriteItem(Location location)
        {
            return new Frame
            {

            };
        }


    }
}
