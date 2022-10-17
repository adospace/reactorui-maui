using MauiReactor;
using Calculator.Pages;


namespace Calculator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiReactorApp<MainPage>()
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
