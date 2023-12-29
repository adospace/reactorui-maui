using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using MauiReactor.TestApp.Pages;

namespace MauiReactor.TestApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiReactorApp<HomePage>(app =>
            {
                app.AddResource("Resources/Styles/DefaultTheme.xaml");

                app.SetWindowsSpecificAssetsDirectory("Assets");
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