using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MauiReactor.HotReload
{
    internal class LocalTypeLoader : ITypeLoader
    {
#pragma warning disable CS0067
        public WeakProducer<ITypeLoaderEventConsumer>? AssemblyChangedEvent { get; }
        public Assembly? LastLoadedAssembly { get; }
#pragma warning restore CS0067

        public T LoadObject<T>(Type type)
        {
            return (T)(Activator.CreateInstance(type) ?? throw new InvalidOperationException());
        }

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
