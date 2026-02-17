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

    public virtual void CopyProperties(object source, object dest)
    {
        //this is not intended to called directly (only via HotReloadTypeLoader)
    }
}
