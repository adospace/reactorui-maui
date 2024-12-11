// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable
namespace MauiReactor;
public partial interface IWebView : IView
{
    EventCommand<WebNavigatedEventArgs>? NavigatedEvent { get; set; }

    EventCommand<WebNavigatingEventArgs>? NavigatingEvent { get; set; }

    EventCommand<WebViewProcessTerminatedEventArgs>? ProcessTerminatedEvent { get; set; }
}

public partial class WebView<T> : View<T>, IWebView where T : Microsoft.Maui.Controls.WebView, new()
{
    public WebView()
    {
        WebViewStyles.Default?.Invoke(this);
    }

    public WebView(Action<T?> componentRefAction) : base(componentRefAction)
    {
        WebViewStyles.Default?.Invoke(this);
    }

    EventCommand<WebNavigatedEventArgs>? IWebView.NavigatedEvent { get; set; }

    EventCommand<WebNavigatingEventArgs>? IWebView.NavigatingEvent { get; set; }

    EventCommand<WebViewProcessTerminatedEventArgs>? IWebView.ProcessTerminatedEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && WebViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<WebNavigatedEventArgs>? _executingNavigatedEvent;
    private EventCommand<WebNavigatingEventArgs>? _executingNavigatingEvent;
    private EventCommand<WebViewProcessTerminatedEventArgs>? _executingProcessTerminatedEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIWebView = (IWebView)this;
        if (thisAsIWebView.NavigatedEvent != null)
        {
            NativeControl.Navigated += NativeControl_Navigated;
        }

        if (thisAsIWebView.NavigatingEvent != null)
        {
            NativeControl.Navigating += NativeControl_Navigating;
        }

        if (thisAsIWebView.ProcessTerminatedEvent != null)
        {
            NativeControl.ProcessTerminated += NativeControl_ProcessTerminated;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_Navigated(object? sender, WebNavigatedEventArgs e)
    {
        var thisAsIWebView = (IWebView)this;
        if (_executingNavigatedEvent == null || _executingNavigatedEvent.IsCompleted)
        {
            _executingNavigatedEvent = thisAsIWebView.NavigatedEvent;
            _executingNavigatedEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Navigating(object? sender, WebNavigatingEventArgs e)
    {
        var thisAsIWebView = (IWebView)this;
        if (_executingNavigatingEvent == null || _executingNavigatingEvent.IsCompleted)
        {
            _executingNavigatingEvent = thisAsIWebView.NavigatingEvent;
            _executingNavigatingEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_ProcessTerminated(object? sender, WebViewProcessTerminatedEventArgs e)
    {
        var thisAsIWebView = (IWebView)this;
        if (_executingProcessTerminatedEvent == null || _executingProcessTerminatedEvent.IsCompleted)
        {
            _executingProcessTerminatedEvent = thisAsIWebView.ProcessTerminatedEvent;
            _executingProcessTerminatedEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.Navigated -= NativeControl_Navigated;
            NativeControl.Navigating -= NativeControl_Navigating;
            NativeControl.ProcessTerminated -= NativeControl_ProcessTerminated;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is WebView<T> @webview)
        {
            if (_executingNavigatedEvent != null && !_executingNavigatedEvent.IsCompleted)
            {
                @webview._executingNavigatedEvent = _executingNavigatedEvent;
            }

            if (_executingNavigatingEvent != null && !_executingNavigatingEvent.IsCompleted)
            {
                @webview._executingNavigatingEvent = _executingNavigatingEvent;
            }

            if (_executingProcessTerminatedEvent != null && !_executingProcessTerminatedEvent.IsCompleted)
            {
                @webview._executingProcessTerminatedEvent = _executingProcessTerminatedEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class WebView : WebView<Microsoft.Maui.Controls.WebView>
{
    public WebView()
    {
    }

    public WebView(Action<Microsoft.Maui.Controls.WebView?> componentRefAction) : base(componentRefAction)
    {
    }

    public WebView(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class WebViewExtensions
{
    /*
    
    
    
    
    
    
    */
    public static T Source<T>(this T webView, Microsoft.Maui.Controls.WebViewSource source)
        where T : IWebView
    {
        //webView.Source = source;
        webView.SetProperty(Microsoft.Maui.Controls.WebView.SourceProperty, source);
        return webView;
    }

    public static T Source<T>(this T webView, Func<Microsoft.Maui.Controls.WebViewSource> sourceFunc)
        where T : IWebView
    {
        //webView.Source = new PropertyValue<Microsoft.Maui.Controls.WebViewSource>(sourceFunc);
        webView.SetProperty(Microsoft.Maui.Controls.WebView.SourceProperty, new PropertyValue<Microsoft.Maui.Controls.WebViewSource>(sourceFunc));
        return webView;
    }

    public static T UserAgent<T>(this T webView, string userAgent)
        where T : IWebView
    {
        //webView.UserAgent = userAgent;
        webView.SetProperty(Microsoft.Maui.Controls.WebView.UserAgentProperty, userAgent);
        return webView;
    }

    public static T UserAgent<T>(this T webView, Func<string> userAgentFunc)
        where T : IWebView
    {
        //webView.UserAgent = new PropertyValue<string>(userAgentFunc);
        webView.SetProperty(Microsoft.Maui.Controls.WebView.UserAgentProperty, new PropertyValue<string>(userAgentFunc));
        return webView;
    }

    public static T Cookies<T>(this T webView, System.Net.CookieContainer cookies)
        where T : IWebView
    {
        //webView.Cookies = cookies;
        webView.SetProperty(Microsoft.Maui.Controls.WebView.CookiesProperty, cookies);
        return webView;
    }

    public static T Cookies<T>(this T webView, Func<System.Net.CookieContainer> cookiesFunc)
        where T : IWebView
    {
        //webView.Cookies = new PropertyValue<System.Net.CookieContainer>(cookiesFunc);
        webView.SetProperty(Microsoft.Maui.Controls.WebView.CookiesProperty, new PropertyValue<System.Net.CookieContainer>(cookiesFunc));
        return webView;
    }

    public static T OnNavigated<T>(this T webView, Action? navigatedAction)
        where T : IWebView
    {
        webView.NavigatedEvent = new SyncEventCommand<WebNavigatedEventArgs>(execute: navigatedAction);
        return webView;
    }

    public static T OnNavigated<T>(this T webView, Action<WebNavigatedEventArgs>? navigatedAction)
        where T : IWebView
    {
        webView.NavigatedEvent = new SyncEventCommand<WebNavigatedEventArgs>(executeWithArgs: navigatedAction);
        return webView;
    }

    public static T OnNavigated<T>(this T webView, Action<object?, WebNavigatedEventArgs>? navigatedAction)
        where T : IWebView
    {
        webView.NavigatedEvent = new SyncEventCommand<WebNavigatedEventArgs>(executeWithFullArgs: navigatedAction);
        return webView;
    }

    public static T OnNavigated<T>(this T webView, Func<Task>? navigatedAction)
        where T : IWebView
    {
        webView.NavigatedEvent = new AsyncEventCommand<WebNavigatedEventArgs>(execute: navigatedAction);
        return webView;
    }

    public static T OnNavigated<T>(this T webView, Func<WebNavigatedEventArgs, Task>? navigatedAction)
        where T : IWebView
    {
        webView.NavigatedEvent = new AsyncEventCommand<WebNavigatedEventArgs>(executeWithArgs: navigatedAction);
        return webView;
    }

    public static T OnNavigated<T>(this T webView, Func<object?, WebNavigatedEventArgs, Task>? navigatedAction)
        where T : IWebView
    {
        webView.NavigatedEvent = new AsyncEventCommand<WebNavigatedEventArgs>(executeWithFullArgs: navigatedAction);
        return webView;
    }

    public static T OnNavigating<T>(this T webView, Action? navigatingAction)
        where T : IWebView
    {
        webView.NavigatingEvent = new SyncEventCommand<WebNavigatingEventArgs>(execute: navigatingAction);
        return webView;
    }

    public static T OnNavigating<T>(this T webView, Action<WebNavigatingEventArgs>? navigatingAction)
        where T : IWebView
    {
        webView.NavigatingEvent = new SyncEventCommand<WebNavigatingEventArgs>(executeWithArgs: navigatingAction);
        return webView;
    }

    public static T OnNavigating<T>(this T webView, Action<object?, WebNavigatingEventArgs>? navigatingAction)
        where T : IWebView
    {
        webView.NavigatingEvent = new SyncEventCommand<WebNavigatingEventArgs>(executeWithFullArgs: navigatingAction);
        return webView;
    }

    public static T OnNavigating<T>(this T webView, Func<Task>? navigatingAction)
        where T : IWebView
    {
        webView.NavigatingEvent = new AsyncEventCommand<WebNavigatingEventArgs>(execute: navigatingAction);
        return webView;
    }

    public static T OnNavigating<T>(this T webView, Func<WebNavigatingEventArgs, Task>? navigatingAction)
        where T : IWebView
    {
        webView.NavigatingEvent = new AsyncEventCommand<WebNavigatingEventArgs>(executeWithArgs: navigatingAction);
        return webView;
    }

    public static T OnNavigating<T>(this T webView, Func<object?, WebNavigatingEventArgs, Task>? navigatingAction)
        where T : IWebView
    {
        webView.NavigatingEvent = new AsyncEventCommand<WebNavigatingEventArgs>(executeWithFullArgs: navigatingAction);
        return webView;
    }

    public static T OnProcessTerminated<T>(this T webView, Action? processTerminatedAction)
        where T : IWebView
    {
        webView.ProcessTerminatedEvent = new SyncEventCommand<WebViewProcessTerminatedEventArgs>(execute: processTerminatedAction);
        return webView;
    }

    public static T OnProcessTerminated<T>(this T webView, Action<WebViewProcessTerminatedEventArgs>? processTerminatedAction)
        where T : IWebView
    {
        webView.ProcessTerminatedEvent = new SyncEventCommand<WebViewProcessTerminatedEventArgs>(executeWithArgs: processTerminatedAction);
        return webView;
    }

    public static T OnProcessTerminated<T>(this T webView, Action<object?, WebViewProcessTerminatedEventArgs>? processTerminatedAction)
        where T : IWebView
    {
        webView.ProcessTerminatedEvent = new SyncEventCommand<WebViewProcessTerminatedEventArgs>(executeWithFullArgs: processTerminatedAction);
        return webView;
    }

    public static T OnProcessTerminated<T>(this T webView, Func<Task>? processTerminatedAction)
        where T : IWebView
    {
        webView.ProcessTerminatedEvent = new AsyncEventCommand<WebViewProcessTerminatedEventArgs>(execute: processTerminatedAction);
        return webView;
    }

    public static T OnProcessTerminated<T>(this T webView, Func<WebViewProcessTerminatedEventArgs, Task>? processTerminatedAction)
        where T : IWebView
    {
        webView.ProcessTerminatedEvent = new AsyncEventCommand<WebViewProcessTerminatedEventArgs>(executeWithArgs: processTerminatedAction);
        return webView;
    }

    public static T OnProcessTerminated<T>(this T webView, Func<object?, WebViewProcessTerminatedEventArgs, Task>? processTerminatedAction)
        where T : IWebView
    {
        webView.ProcessTerminatedEvent = new AsyncEventCommand<WebViewProcessTerminatedEventArgs>(executeWithFullArgs: processTerminatedAction);
        return webView;
    }
}

public static partial class WebViewStyles
{
    public static Action<IWebView>? Default { get; set; }
    public static Dictionary<string, Action<IWebView>> Themes { get; } = [];
}