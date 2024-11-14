using MauiReactor;
using MauiReactorTemplate.Startup.Components;
using MauiReactorTemplate.Startup.Resources.Styles;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;

namespace MauiReactorTemplate.Startup
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiReactorApp<HomePage>(app =>
                {
                    app.UseTheme<ApplicationTheme>();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.EnableMauiReactorHotReload();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
