using MauiReactor.Internals;
using Microsoft.Maui.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.HotReload;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseMauiReactorHotReload(
        this MauiAppBuilder appBuilder,
        Action? onHotReloadCompleted = null)
    {
        ServiceCollectionProvider.Instance = new HotReloadServiceCollectionProvider();

        TypeLoader.Instance = new HotReloadTypeLoader
        {
            OnHotReloadCompleted = onHotReloadCompleted
        };

        return appBuilder;
    }
}
