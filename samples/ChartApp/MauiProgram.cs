using MauiReactor;
using ChartApp.Pages;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp.Views.Maui.Controls.Hosting;
using LiveChartsCore.SkiaSharpView.Maui;
namespace ChartApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseSkiaSharp()
            .UseLiveCharts()
            .UseMauiReactorApp<ChartPage>(app =>
            {
                //app.AddResource("Resources/Styles/Colors.xaml");
                //app.AddResource("Resources/Styles/Styles.xaml");

                LiveCharts.Configure(config =>
                    config
                        // registers SkiaSharp as the library backend
                        // REQUIRED unless you build your own
                        .AddSkiaSharp()

                        // adds the default supported types
                        // OPTIONAL but highly recommend
                        .AddDefaultMappers()

                        // select a theme, default is Light
                        // OPTIONAL
                        .AddDarkTheme()
                    //.AddLightTheme()
                    );
            })
            //.EnableMauiReactorHotReload()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            });

        return builder.Build();
    }
}
