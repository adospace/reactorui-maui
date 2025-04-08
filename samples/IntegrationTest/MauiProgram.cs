using MauiReactor;
using Microsoft.Extensions.Logging;

namespace IntegrationTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiReactor()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


#if DEBUG
            //builder.EnableMauiReactorHotReload();
		    builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<SampleService>();

            return builder.Build();
        }
    }

    class SampleService
    {
        public int Increment(int v)
        {
            return v + 1;
        }
    }

}