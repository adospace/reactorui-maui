using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

[assembly: MetadataUpdateHandler(typeof(MauiReactor.HotReload.HotReloadTypeLoader))]

namespace MauiReactor.HotReload;

internal class HotReloadTypeLoader : TypeLoader
{
    private readonly HotReloadServer _server;

    private Assembly? _assembly;

    private bool _running;

    //private static HotReloadTypeLoader? _instance;

    public HotReloadTypeLoader()
    {
        _server = new HotReloadServer(ReceivedAssemblyFromHost);
    }

    //public static HotReloadTypeLoader Instance
    //{
    //    get => MauiReactorFeatures.HotReloadIsEnabled ? (_instance ??= new()) : throw new InvalidOperationException();
    //}

    public Assembly? LastLoadedAssembly => _assembly;

    public Action? OnHotReloadCompleted { get; set; }

    public override T? LoadObject<T>(Type type, bool throwExceptions = true) where T : class
    {
        if (_assembly == null)
        {
            return (T?)Activator.CreateInstance(type);
            //throw new InvalidOperationException("No assembly loaded from Hot Reload server");
        }

        try
        {
            var objectTypeInAssembly = _assembly.GetType(type.FullName ?? throw new InvalidOperationException()) ?? throw new InvalidOperationException();

            return (T)(Activator.CreateInstance(objectTypeInAssembly) ?? throw new InvalidOperationException());
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MauiReactor] Unable to hot reload component {type.FullName}:{Environment.NewLine}{ex}");
            if (throwExceptions)
            {
                throw new InvalidOperationException($"Unable to hot reload component {type.FullName}", ex);
            }            
        }

        return default;
    }

    private void ReceivedAssemblyFromHost(Assembly? newAssembly)
    {
        _assembly = newAssembly;
        AssemblyChangedEvent?.Raise(consumer => consumer.OnAssemblyChanged());
        OnHotReloadCompleted?.Invoke();
    }

    public override void Run()
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

    public override void Stop()
    {
        DeviceDisplay.Current.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
        _server.Stop();
        _running = false;
    }

    public override void CopyProperties(object source, object dest)
    {
        var sourceProps = source
            .GetType()
            .GetProperties()
            .Where(x => x.CanRead)
            .ToList();

        var destProps = dest
            .GetType()
            .GetProperties()
            .Where(_ => _.CanWrite)
            .ToDictionary(_ => _.Name, _ => _);

        foreach (var sourceProp in sourceProps)
        {
            if (!destProps.TryGetValue(sourceProp.Name, out var destProp))
                continue;

            var sourceValue = sourceProp.GetValue(source, null);

            try
            {
                if (sourceValue != null &&
                    sourceProp.PropertyType != destProp.PropertyType)
                {
                    var sourceValueType = sourceValue.GetType();
                    if (sourceValueType.IsEnum)
                    {
                        var underlyingTypeAsNullable = Nullable.GetUnderlyingType(sourceProp.PropertyType);
                        if (underlyingTypeAsNullable != null)
                            sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(underlyingTypeAsNullable));
                        else
                            sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[MauiReactor] Using Json serialization for property '{destProp.Name}' of state ({source?.GetType()}) to copy state to new component after hot-reload");
                        var serializedSourceValue = System.Text.Json.JsonSerializer.Serialize(sourceValue);
                        sourceValue = System.Text.Json.JsonSerializer.Deserialize(serializedSourceValue, destProp.PropertyType);
                    }
                }

                destProp.SetValue(dest, sourceValue, null);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to copy property '{destProp.Name}' of state ({source?.GetType()}) to new state after hot-reload:{Environment.NewLine}{ex})");
            }
        }
    }

    private static string DumpExceptionMessage(Exception ex)
    {
        if (ex.InnerException != null)
        {
            return $"{ex.GetType().Name}({DumpExceptionMessage(ex.InnerException)})";
        }

        return ex.Message;
    }


#pragma warning disable IDE0051 // Remove unused private members
    static void UpdateApplication(Type[]? _)
#pragma warning restore IDE0051 // Remove unused private members
    {
        //if (!MauiReactorFeatures.HotReloadIsEnabled)
        //{
        //    Debug.WriteLine($"[MauiReactor] Hot-Reload is not enabled, please call EnableMauiReactorHotReload() on your AppBuilder");
        //    return;
        //}

        Debug.WriteLine($"[MauiReactor] Hot-Reload triggered");

        ((HotReloadTypeLoader)Instance).ReceivedAssemblyFromHost(null);
    }
}
