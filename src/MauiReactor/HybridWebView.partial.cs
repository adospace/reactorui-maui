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
        hybridWebView.RawMessageReceivedActionWithArgs = (s, args) => rawMessageReceivedAction?.Invoke(args.Message);
        return hybridWebView;
    }
}
