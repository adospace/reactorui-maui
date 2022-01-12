using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal interface IComponentLoader
    {
        RxComponent LoadComponent<T>() where T : RxComponent, new();

        event EventHandler ComponentAssemblyChanged;

        void Run();

        void Stop();
    }
}
