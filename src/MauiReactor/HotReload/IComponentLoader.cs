using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MauiReactor.HotReload
{
    internal interface IComponentLoader
    {
        event EventHandler<EventArgs> AssemblyChanged;

        public T? LoadComponent<T>() where T : Component, new();

        void Run();

        void Stop();
    }
}
