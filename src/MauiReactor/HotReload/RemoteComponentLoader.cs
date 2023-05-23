using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;

[assembly: MetadataUpdateHandler(typeof(MauiReactor.HotReload.RemoteComponentLoader))]

namespace MauiReactor.HotReload;

internal class RemoteComponentLoader : IComponentLoader
{
    public event EventHandler<EventArgs>? AssemblyChanged;

    private readonly HotReloadServer _server;

    private static RemoteComponentLoader? _instance;

    private Assembly? _assembly;

    private bool _running;

    public Component? LoadComponent<T>() where T : Component, new()
    {
        if (_assembly == null)
            return new T();

        return LoadComponent(typeof(T));
    }

    public Component? LoadComponent(Type componentType)
    {
        if (_assembly == null)
        {
            return (Component?)(Activator.CreateInstance(componentType) ?? throw new InvalidOperationException());
        }

        var type = _assembly.GetType(componentType.FullName ?? throw new InvalidOperationException());

        if (type == null)
        {
            System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to hot reload component {componentType.FullName}: type not found in received assembly");
            return null;
            //throw new InvalidOperationException($"Unable to hot-reload component {typeof(T).FullName}: type not found in received assembly");
        }

        try
        {
            return (Component?)(Activator.CreateInstance(type) ?? throw new InvalidOperationException());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to hot reload component {componentType.FullName}:{Environment.NewLine}{ex}");
            throw;
        }
    }

    public RemoteComponentLoader()
    {
        _instance = this;
        _server = new HotReloadServer(ReceivedAssemblyFromHost);
    }

    private void ReceivedAssemblyFromHost(Assembly? newAssembly)
    {
        _assembly = newAssembly;
        AssemblyChanged?.Invoke(this, EventArgs.Empty);
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
        if (_instance == null)
        {
            Debug.WriteLine($"[MauiReactor] Hot-Reload is not enabled, please call EnableMauiReactorHotReload() on your AppBuilder");
            return;
        }

        //Debug.WriteLine($"[MauiReactor] Hot-Reload triggered");

        _instance.ReceivedAssemblyFromHost(null);
    }
}