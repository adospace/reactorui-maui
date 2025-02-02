using MauiReactor.WeatherTwentyOne.Pages;
using MauiReactor.WeatherTwentyOne.Services;
using Microsoft.Maui.LifecycleEvents;

namespace MauiReactor.WeatherTwentyOne
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiReactorApp<ShellPage>(app => 
                {
                    //app.AddResource("Resources/Styles/DefaultTheme.xaml");

                    app.SetWindowsSpecificAssetsDirectory("Assets");
                })
#if DEBUG
            //.EnableMauiReactorHotReload()
#endif
            .ConfigureFonts(fonts => {
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            });
//            builder.ConfigureLifecycleEvents(lifecycle => {
//#if WINDOWS
//        lifecycle
//            .AddWindows(windows =>
//                windows.OnNativeMessage((app, args) => {
//                    if (WindowExtensions.Hwnd == IntPtr.Zero)
//                    {
//                        WindowExtensions.Hwnd = args.Hwnd;
//                        WindowExtensions.SetIcon("Platforms/Windows/trayicon.ico");
//                    }
//                    app.ExtendsContentIntoTitleBar = false;
//                }));
//#endif
//            });

            var services = builder.Services;
            services.AddSingleton<IWeatherService, WeatherService>();
#if WINDOWS
            services.AddSingleton<ITrayService, WinUI.TrayService>();
            services.AddSingleton<INotificationService, WinUI.NotificationService>();
#elif MACCATALYST
            services.AddSingleton<ITrayService, MacCatalyst.TrayService>();
            services.AddSingleton<INotificationService, MacCatalyst.NotificationService>();
#endif

            return builder.Build();
        }
    }
}