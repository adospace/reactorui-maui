using Microsoft.AspNetCore.Components.WebView.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Blazor.TestApp.Pages;

class MainPage : Component
{
    public override VisualNode Render()
    {
        return new ContentPage
        {
            new BlazorWebView()
                .HostPage("wwwroot/index.html")
                .RootComponent(new RootComponent(){ ComponentType = typeof(Routes), Selector="#app" })
        }
        .BackgroundColor(Color.FromRgba("#512bdf"));
    }
}
