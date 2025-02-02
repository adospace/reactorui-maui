using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;


public static partial class HybridWebViewExtensions
{
    public static T OnRawMessageReceived<T>(this T hybridWebView, Action<string?>? rawMessageReceivedAction)
        where T : IHybridWebView
    {
        hybridWebView.RawMessageReceivedEvent = new SyncEventCommand<HybridWebViewRawMessageReceivedEventArgs>(
            (s, args) => rawMessageReceivedAction?.Invoke(args.Message));
        return hybridWebView;
    }
    public static T OnRawMessageReceived<T>(this T hybridWebView, Func<string?, Task>? rawMessageReceivedAction, bool runInBackground = false)
        where T : IHybridWebView
    {
        if (rawMessageReceivedAction != null)
        {
            hybridWebView.RawMessageReceivedEvent = new AsyncEventCommand<HybridWebViewRawMessageReceivedEventArgs>(executeWithArgs: (args) => rawMessageReceivedAction.Invoke(args.Message), runInBackground);
        }
        return hybridWebView;
    }
}
