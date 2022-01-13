using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal interface IComponentLoader
    {
        Component LoadComponent<T>() where T : Component, new();

        event EventHandler ComponentAssemblyChanged;

        void Run();

        void Stop();
    }
}
