using System;
using System.Reflection;

namespace MauiReactor.HotReload
{
    internal class RemoteComponentLoader : IComponentLoader
    {
        public event EventHandler<EventArgs>? AssemblyChanged;

        private readonly HotReloadServer _server;

        private Assembly? _assembly;

        public T? LoadComponent<T>() where T : Component, new()
        {
            if (_assembly == null)
                return new T();

            var type = _assembly.GetType(typeof(T).FullName ?? throw new InvalidOperationException());

            if (type == null)
            {
                System.Diagnostics.Debug.WriteLine($"Unable to hot reload component {typeof(T).FullName}: type not found in received assembly");
                return null;
                //throw new InvalidOperationException($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
            }

            return (T)(Activator.CreateInstance(type) ?? throw new InvalidOperationException());
        }

        public RemoteComponentLoader()
        {
            _server = new HotReloadServer(ReceivedAssemblyFromHost);
        }

        private void ReceivedAssemblyFromHost(Assembly newAssembly)
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
    }
}