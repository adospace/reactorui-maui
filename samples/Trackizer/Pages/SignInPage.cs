using MauiReactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackizer.Styles;

namespace Trackizer.Pages;

class SignInPage : Component
{
    public override VisualNode Render()
    {
        return new ContentPage("Trackizer - Sign In")
        {
            new Grid("Auto * Auto", "*")
            {
                new Image("logo.png")
                    .Margin(100, 60),

                new VerticalStackLayout
                {
                    new VerticalStackLayout
                    {
                        Theme.Current.BodySmall("Login").TextColor(Theme.Current.Gray50),

                        Theme.Current.Entry()
                    }
                    .Spacing(5),

                    new VerticalStackLayout
                    {
                        Theme.Current.BodySmall("Password").TextColor(Theme.Current.Gray50),

                        Theme.Current.Entry()
                    }
                    .Spacing(5),

                    Theme.Current.PrimaryButton("Sign In")
                }
                .Spacing(20)
                .Margin(20)
                .VCenter()
                .GridRow(1),



                new VerticalStackLayout
                {
                    Theme.Current.BodyMedium("If you don't have an account yet?")
                        .HCenter(),

                    Theme.Current.Button("Sign Up"),
                }
                .GridRow(2)
                .HCenter()
                .Spacing(20)
                .Margin(20)
            }
        }
        .Set(MauiControls.NavigationPage.HasNavigationBarProperty, false);
    }


}
