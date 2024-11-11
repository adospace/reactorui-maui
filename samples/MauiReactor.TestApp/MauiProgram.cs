using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using MauiReactor.TestApp.Pages;
using MauiReactor.TestApp.Resources.Styles;

namespace MauiReactor.TestApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiReactorApp<TestBug257>(app =>
            {
                //we can mix styles from xaml dictionary with...
                app.AddResource("Resources/Styles/DefaultTheme.xaml");

                app.SetWindowsSpecificAssetsDirectory("Assets");

                //... the MauiReactor theming, but often it's easier to just manage styles in either XAML or c#
                app.UseTheme<AppTheme>();
            })
#if DEBUG
            .EnableMauiReactorHotReload()
            //This will enable the FrameRateIndicator widget
            //Disable before publishing the app
            .EnableFrameRateIndicator()
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .UseMauiCommunityToolkit();

        builder.Services.AddSingleton<Services.IncrementService>();


        return builder.Build();
    }
}