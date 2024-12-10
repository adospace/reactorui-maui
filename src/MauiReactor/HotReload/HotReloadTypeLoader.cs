using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

[assembly: MetadataUpdateHandler(typeof(MauiReactor.HotReload.HotReloadTypeLoader))]

namespace MauiReactor.HotReload;

internal interface ITypeLoaderEventConsumer
{
    void OnAssemblyChanged();
}

internal class HotReloadTypeLoader
{
    private readonly HotReloadServer _server;

    private Assembly? _assembly;

    private bool _running;

    private static HotReloadTypeLoader? _instance;

    public HotReloadTypeLoader()
    {
        _server = new HotReloadServer(ReceivedAssemblyFromHost);
    }

    public static HotReloadTypeLoader Instance
    {
        get => MauiReactorFeatures.HotReloadIsEnabled ? (_instance ??= new()) : throw new InvalidOperationException();
    }

    public Assembly? LastLoadedAssembly => _assembly;

    public Action? OnHotReloadCompleted { get; set; }

    public WeakProducer<ITypeLoaderEventConsumer>? AssemblyChangedEvent { get; } = new();


    public T LoadObject<T>(Type type)
    {
        if (_assembly == null)
        {
            return (T)(Activator.CreateInstance(type) ?? throw new InvalidOperationException());
        }

        var objectTypeInAssembly = _assembly.GetType(type.FullName ?? throw new InvalidOperationException()) ?? throw new InvalidOperationException();

        try
        {
            return (T)(Activator.CreateInstance(objectTypeInAssembly) ?? throw new InvalidOperationException());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MauiReactor] Unable to hot reload component {objectTypeInAssembly.FullName}:{Environment.NewLine}{ex}");
            throw new InvalidOperationException($"Unable to hot reload component {objectTypeInAssembly.FullName}", ex);
        }
    }

    private void ReceivedAssemblyFromHost(Assembly? newAssembly)
    {
        _assembly = newAssembly;
        AssemblyChangedEvent?.Raise(consumer => consumer.OnAssemblyChanged());
        OnHotReloadCompleted?.Invoke();
    }

    public void Run()
    {
        if (_running)
        {
            return;
        }

        _running = true;
        DeviceDisplay.Current.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        _server.Run();
    }

    static float? _lastRefreshRate;

    private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        if (_lastRefreshRate == null ||
            _lastRefreshRate != e.DisplayInfo.RefreshRate)
        {
            _lastRefreshRate = e.DisplayInfo.RefreshRate;
            Debug.WriteLine($"[MauiReactor] FPS: {_lastRefreshRate}");
        }
    }

    public void Stop()
    {
        DeviceDisplay.Current.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
        _server.Stop();
        _running = false;
    }

#pragma warning disable IDE0051 // Remove unused private members
    static void UpdateApplication(Type[]? _)
#pragma warning restore IDE0051 // Remove unused private members
    {
        if (Instance == null)
        {
            Debug.WriteLine($"[MauiReactor] Hot-Reload is not enabled, please call EnableMauiReactorHotReload() on your AppBuilder");
            return;
        }

        //Debug.WriteLine($"[MauiReactor] Hot-Reload triggered");

        Instance.ReceivedAssemblyFromHost(null);
    }
}
