using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackizer.Styles;

namespace Trackizer.Pages;

class LandingPage : Component
{
    public override VisualNode Render()
    {
        return new ContentPage("Trackizer")
        {
            new Grid("Auto * Auto", "*")
            {
                new Image("logo.png")
                    .Margin(100, 60),

                new Image("langing_background2.png")
                    .HeightRequest(250)
                    .TranslationX(-290)
                    .TranslationY(-130)
                    .GridRow(1),

                new Image("langing_background2.png")
                    .HeightRequest(250)
                    .TranslationX(300)
                    .TranslationY(150)
                    .Rotation(20)
                    .GridRow(1),

                new Image("landing_image3.png")
                    .GridRow(1),

                new VerticalStackLayout
                {
                    Theme.Current.PrimaryButton("Get started"),

                    Theme.Current.Button("I have an account")
                        .OnClick(OnNavigateSignInPage),
                }
                .GridRow(2)
                .HCenter()
                .Spacing(20)
                .Margin(20)
            }
        }
        .Set(MauiControls.NavigationPage.HasNavigationBarProperty, false);
    }

    private async void OnNavigateSignInPage()
    {
        if (Navigation == null)
        {
            return;
        }

        await Navigation.PushAsync<SignInPage>();
    }
}
