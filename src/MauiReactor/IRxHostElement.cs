using System;

namespace MauiReactor
{
    public interface IRxHostElement
    {
        IRxHostElement Run();

        void Stop();

        Page? ContainerPage { get; }
    }
}
