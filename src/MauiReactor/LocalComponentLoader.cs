using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal class LocalComponentLoader : IComponentLoader
    {
        public event EventHandler ComponentAssemblyChanged;

        public RxComponent LoadComponent<T>() where T : RxComponent, new() => new T();

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
