using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor.HotReload
{
    internal class LocalTypeLoader : ITypeLoader
    {
#pragma warning disable CS0067
        //private readonly WeakEvent<EventArgs> _event = new();
        //public event EventHandler<EventArgs> AssemblyChanged
        //{
        //    add => _event.AddListener(value);
        //    remove => _event.RemoveListener(value);
        //}
        //public event EventHandler<EventArgs>? AssemblyChanged;

        public WeakProducer<ITypeLoaderEventConsumer>? AssemblyChangedEvent { get; }

#pragma warning restore CS0067
        //public Component? LoadComponent<T>() where T : Component, new() => new T();

        //public T LoadObject<T>() where T : new()
        //{
        //    return (T)(Activator.CreateInstance(typeof(T)) ?? throw new InvalidOperationException());
        //}

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
