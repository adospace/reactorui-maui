using MauiReactor;
using Calculator.Pages;
using Calculator.Resources.Styles;


namespace Calculator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiReactorApp<MainPage>(app =>
             {
                 app.UseTheme<AppTheme>();
             })
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
