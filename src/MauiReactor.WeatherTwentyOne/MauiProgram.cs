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
                    var resourceDictionary = new ResourceDictionary();
                    resourceDictionary.SetAndLoadSource(
                        new Uri("Resources/Styles/DefaultTheme.xaml", UriKind.Relative),
                        "Resources/Styles/DefaultTheme.xaml", 
                        typeof(MauiProgram).Assembly, 
                        null);

                    app.Resources.Add(resourceDictionary);

                    Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.Application.SetImageDirectory(app, "Assets");
                })
#if DEBUG
            .EnableMauiReactorHotReload()
#endif
            .ConfigureFonts(fonts => {
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
            });
            builder.ConfigureLifecycleEvents(lifecycle => {
#if WINDOWS
            lifecycle
                .AddWindows(windows => windows.OnLaunched((app, args) => {
                    var winuiApp = (Microsoft.UI.Xaml.Window)MauiWinUIApplication.Current.Application.Windows[0].Handler!.NativeView!;
                    winuiApp.SetIcon("Platforms/Windows/trayicon.ico");
                }));
#endif
            });

            var services = builder.Services;
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