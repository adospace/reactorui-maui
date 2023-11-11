using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace MauiReactor;

public partial interface IBlazorWebView : IView
{
    List<RootComponent> RootComponents { get; }

    string? HostPage { get; set; }
}

public partial class BlazorWebView<T>
{
    List<RootComponent> IBlazorWebView.RootComponents { get; } = new();

    string? IBlazorWebView.HostPage { get; set; }

    partial void OnBeginUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIBlazorWebView = (IBlazorWebView)this;


        NativeControl.HostPage = thisAsIBlazorWebView.HostPage;

        foreach (var rootComponent in  thisAsIBlazorWebView.RootComponents)
        {
            if (!NativeControl.RootComponents.Contains(rootComponent))
            {
                NativeControl.RootComponents.Add(rootComponent);
            }
        }        
    }
}

public static partial class BlazorWebViewExtensions
{
    public static T RootComponent<T>(this T blazorWebView, RootComponent rootComponent)
        where T : IBlazorWebView
    {
        blazorWebView.RootComponents.Add(rootComponent);
        return blazorWebView;
    }

    public static T HostPage<T>(this T blazorWebView, string hostPage)
        where T : IBlazorWebView
    {
        blazorWebView.HostPage = hostPage;
        return blazorWebView;
    }
}
