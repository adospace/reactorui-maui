using MauiReactor;
using MauiReactor.HotReload;
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
                    },
                    unhandledExceptionAction: e => 
                    {
                        System.Diagnostics.Debug.WriteLine(e.ExceptionObject);
                    })
#if DEBUG
                .UseMauiReactorHotReload()
#endif
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
