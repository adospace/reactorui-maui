using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;

namespace MauiReactor.TestApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiReactorApp<HomePage>(app =>
                {
                    app.AddResource("Resources/Styles/DefaultTheme.xaml");

                    app.SetWindowsSpecificAssectDirectory("Assets");
                })
#if DEBUG
                .EnableMauiReactorHotReload()
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
}