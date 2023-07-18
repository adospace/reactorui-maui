using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.HotReload
{
    internal class LocalComponentLoader : IComponentLoader
    {
#pragma warning disable CS0067
        public event EventHandler<EventArgs>? AssemblyChanged;
#pragma warning restore CS0067
        public Component? LoadComponent<T>() where T : Component, new() => new T();

        public Component? LoadComponent(Type componentType)
        {
            return (Component?)(Activator.CreateInstance(componentType) ?? throw new InvalidOperationException());
        }

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
