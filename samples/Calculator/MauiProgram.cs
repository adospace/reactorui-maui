using MauiReactor;
using Calculator.Pages;
using Rearch.Reactor.Components;

namespace Calculator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseRearchReactorApp<MainPage>()
#if DEBUG
            .EnableMauiReactorHotReload()
#endif
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("WorkSans-Regular", "WorkSansRegular");
                fonts.AddFont("WorkSans-Light.ttf", "WorkSansLight");
            });

        return builder.Build();
    }
}
