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
                    app.AddResource("Resources/Styles/DefaultTheme.xaml");

                    app.SetWindowsSpecificAssectDirectory("Assets");
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