namespace MauiReactor.TestApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiReactorApp<HomePage>(app =>
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

            return builder.Build();
        }
    }
}