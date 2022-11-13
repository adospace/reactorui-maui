using MauiReactor;
using MauiReactor.FigmaPlugin.Pages;


namespace MauiReactor.FigmaPlugin
{
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

                    app.SetWindowsSpecificAssectDirectory("Assets");
                })
#if DEBUG
                .EnableMauiReactorHotReload()
#endif
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemiBold");
                    fonts.AddFont("CascadiaCode.ttf", "CascadiaCodeRegular");
                });

            return builder.Build();
        }
    }
}