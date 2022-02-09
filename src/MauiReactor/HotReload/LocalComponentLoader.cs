using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.HotReload
{
    internal class LocalComponentLoader : IComponentLoader
    {
        public event EventHandler<EventArgs>? AssemblyChanged;

        public T? LoadComponent<T>() where T : Component, new() => new T();

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
