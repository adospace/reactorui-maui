using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MauiReactor.HotReload
{
    internal interface IComponentLoader
    {
        event EventHandler<EventArgs> AssemblyChanged;

        //Component? LoadComponent<T>() where T : Component, new();

        Component LoadComponent(Type componentType);

        void Run();

        void Stop();
    }

    internal abstract class ComponentLoader
    {
        private static RemoteComponentLoader? _remoteComponentLoader;
        private static LocalComponentLoader? _localComponentLoader;

        public static bool UseRemoteLoader { get; set; }

        public static IComponentLoader Instance => UseRemoteLoader ? _remoteComponentLoader ??= new RemoteComponentLoader() : _localComponentLoader ??= new LocalComponentLoader();
    }
}
