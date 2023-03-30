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

    public Component? LoadComponent<T>() where T : Component, new()
    {
        if (_assembly == null)
            return new T();

        var type = _assembly.GetType(typeof(T).FullName ?? throw new InvalidOperationException());

        if (type == null)
        {
            System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to hot reload component {typeof(T).FullName}: type not found in received assembly");
            return null;
            //throw new InvalidOperationException($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
        }

        try
        {
            return (Component?)(Activator.CreateInstance(type) ?? throw new InvalidOperationException());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to hot reload component {typeof(T).FullName}:{Environment.NewLine}{ex}");
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
        _server.Run();
    }

    public void Stop()
    {
        _server.Stop();
    }

    static void UpdateApplication(Type[]? _)
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