using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using MauiReactor.TestApp.Pages;
using MauiReactor.TestApp.Resources.Styles;
using Microsoft.Maui.Controls.Hosting;

namespace MauiReactor.TestApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .UseMauiCommunityToolkit();

        builder.Services.AddSingleton<Services.IncrementService>();


        return builder.Build();
    }
}