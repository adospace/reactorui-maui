using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

internal class WeakProducer<TConsumer> where TConsumer : class
{
    private readonly List<WeakReference<TConsumer>> _listeners = [];

    public void AddListener(TConsumer handler)
    {
        RemoveListener(handler);

        _listeners.Add(new WeakReference<TConsumer>(handler));
    }

    public void RemoveListener(TConsumer handler)
    {
        _listeners.RemoveAll(wr =>
        {
            if (!wr.TryGetTarget(out var refHandler) || handler == refHandler)
            {
                return true;
            }

            return false;
        });
    }

    public void Raise(Action<TConsumer> actionOnConsumer)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            var weakReference = _listeners[i];

            if (weakReference.TryGetTarget(out var handler))
            {
                actionOnConsumer(handler);
            }
            else
            {
                _listeners.RemoveAt(i);
            }
        }
    }
}


//internal class WeakEvent<TEventArgs>
//{
//    private readonly List<WeakReference<EventHandler<TEventArgs>>> _listeners = [];

//    public void AddListener(EventHandler<TEventArgs> handler)
//    {
//        RemoveListener(handler);

//        _listeners.Add(new WeakReference<EventHandler<TEventArgs>>(handler));
//    }

//    public void RemoveListener(EventHandler<TEventArgs> handler)
//    {
//        _listeners.RemoveAll(wr =>
//        {
//            if (!wr.TryGetTarget(out var refHandler) || handler == refHandler)
//            {
//                return true;
//            }

//            return false;
//        });
//    }

//    public void Raise(object sender, TEventArgs args)
//    {
//        for (int i = _listeners.Count - 1; i >= 0; i--)
//        {
//            var weakReference = _listeners[i];

//            if (weakReference.TryGetTarget(out var handler))
//            {
//                handler?.Invoke(sender, args);
//            }
//            else
//            {
//                _listeners.RemoveAt(i);
//            }
//        }
//    }
//}
