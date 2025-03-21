﻿using MauiReactor;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


#if DEBUG
            //builder.EnableMauiReactorHotReload();
		    builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}