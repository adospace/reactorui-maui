using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MauiReactor.HotReload
{
    internal interface ITypeLoaderEventConsumer
    {
        void OnAssemblyChanged();
    }

    internal interface ITypeLoader
    {
        WeakProducer<ITypeLoaderEventConsumer>? AssemblyChangedEvent { get; }

        Assembly? LastLoadedAssembly { get; }

        T LoadObject<T>(Type type);

        void Run();

        void Stop();
    }

    internal abstract class TypeLoader
    {
        private static RemoteTypeLoader? _remoteComponentLoader;
        private static LocalTypeLoader? _localComponentLoader;

        public static bool UseRemoteLoader { get; set; }

        public static ITypeLoader Instance 
            => UseRemoteLoader ? _remoteComponentLoader ??= new RemoteTypeLoader() : _localComponentLoader ??= new LocalTypeLoader();

        public static Action? OnHotReloadCompleted { get; set; }
    }
}
