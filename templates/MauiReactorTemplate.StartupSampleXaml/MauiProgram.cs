using CommunityToolkit.Maui;
using Fonts;
using MauiReactor.HotReload;
using MauiReactorTemplate.StartupSampleXaml.Data;
using MauiReactorTemplate.StartupSampleXaml.Services;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;


namespace MauiReactorTemplate.StartupSampleXaml;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureMauiHandlers(handlers =>
            {
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
            });

#if DEBUG
        builder.UseMauiReactorHotReload();
#endif


#if DEBUG
        builder.Logging.AddDebug();
#endif

        RegisterServices(builder.Services);

        return builder.Build();
    }

    [ComponentServices]
    static void RegisterServices(IServiceCollection services)
    {
#if DEBUG
        services.AddLogging(configure => configure.AddDebug());
#endif

        services.AddSingleton<ProjectRepository>();
        services.AddSingleton<TaskRepository>();
        services.AddSingleton<CategoryRepository>();
        services.AddSingleton<TagRepository>();
        services.AddSingleton<SeedDataService>();
        services.AddSingleton<ModalErrorHandler>();
    }
}
