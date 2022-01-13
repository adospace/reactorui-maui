using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal class LocalComponentLoader : IComponentLoader
    {
        public event EventHandler? ComponentAssemblyChanged;

        public Component LoadComponent<T>() where T : Component, new() => new T();

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
