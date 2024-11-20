using Microsoft.Extensions.Logging;

namespace MauiReactorTemplate.StartupXaml;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.EnableMauiReactorHotReload();
        builder.OnMauiReactorUnhandledException((e) =>
        {
            System.Diagnostics.Debug.WriteLine(e.ExceptionObject);
        });
        builder.Logging.AddDebug();
#endif


        return builder.Build();
    }
}
