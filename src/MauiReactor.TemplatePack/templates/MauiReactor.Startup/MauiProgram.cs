using MauiReactor;
using Microsoft.Extensions.Logging;
using MauiReactor.Startup.Pages;


namespace MauiReactor.Startup;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiReactorApp<MainPage>(app =>
            {
                app.AddResource("Resources/Styles/Colors.xaml");
                app.AddResource("Resources/Styles/Styles.xaml");
            })
//-:cnd:noEmit
#if DEBUG
            .EnableMauiReactorHotReload()
#endif
//+:cnd:noEmit
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            });

//-:cnd:noEmit
#if DEBUG
    		builder.Logging.AddDebug();
#endif
//-:cnd:noEmit

        return builder.Build();
    }
}
