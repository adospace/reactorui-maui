using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace MauiReactor.HotReload;

internal class TypeLoader
{
    private static TypeLoader? _instance;

    public static TypeLoader Instance
    {
        get => _instance ??= new();
        set => _instance = value;
    }

    public WeakProducer<ITypeLoaderEventConsumer>? AssemblyChangedEvent { get; } = new();

    public virtual T? LoadObject<T>(Type type, bool throwExceptions = true) where T : class
    {
        //this is not intended to called directly (only via HotReloadTypeLoader)
        return null;
    }

    public virtual void Run()
    {
        //this is not intended to called directly (only via HotReloadTypeLoader)
        
    }

    public virtual void Stop() 
    {
        //this is not intended to called directly (only via HotReloadTypeLoader)
    }

    /// <summary>
    /// Announces a hot-reload pass to every host. Bumps the generation first, so that any
    /// re-render triggered by a consumer already sees the new generation and refreshes
    /// memoized values and effects instead of trusting last render's dependencies.
    /// There are four ITypeLoaderEventConsumer hosts (PageHost, ReactorApplicationHost and
    /// its generic override, ComponentHost); this is the one place they all funnel through.
    /// </summary>
    protected void RaiseAssemblyChanged()
    {
        HotReloadGeneration.Bump();

        AssemblyChangedEvent?.Raise(consumer => consumer.OnAssemblyChanged());
    }

    public virtual void CopyProperties(object source, object dest)
    {
        //this is not intended to called directly (only via HotReloadTypeLoader)
    }
}
