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
public partial interface IHybridWebView : IView
{
    object? DefaultFile { get; set; }

    object? HybridRoot { get; set; }

    Action? RawMessageReceivedAction { get; set; }

    Action<object?, HybridWebViewRawMessageReceivedEventArgs>? RawMessageReceivedActionWithArgs { get; set; }
}

public partial class HybridWebView<T> : View<T>, IHybridWebView where T : Microsoft.Maui.Controls.HybridWebView, new()
{
    public HybridWebView()
    {
        HybridWebViewStyles.Default?.Invoke(this);
    }

    public HybridWebView(Action<T?> componentRefAction) : base(componentRefAction)
    {
        HybridWebViewStyles.Default?.Invoke(this);
    }

    object? IHybridWebView.DefaultFile { get; set; }

    object? IHybridWebView.HybridRoot { get; set; }

    Action? IHybridWebView.RawMessageReceivedAction { get; set; }

    Action<object?, HybridWebViewRawMessageReceivedEventArgs>? IHybridWebView.RawMessageReceivedActionWithArgs { get; set; }

    internal override void Reset()
    {
        base.Reset();
        var thisAsIHybridWebView = (IHybridWebView)this;
        thisAsIHybridWebView.DefaultFile = null;
        thisAsIHybridWebView.HybridRoot = null;
        thisAsIHybridWebView.RawMessageReceivedAction = null;
        thisAsIHybridWebView.RawMessageReceivedActionWithArgs = null;
        OnReset();
    }

    partial void OnReset();
    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIHybridWebView = (IHybridWebView)this;
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.HybridWebView.DefaultFileProperty, thisAsIHybridWebView.DefaultFile);
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.HybridWebView.HybridRootProperty, thisAsIHybridWebView.HybridRoot);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && HybridWebViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIHybridWebView = (IHybridWebView)this;
        if (thisAsIHybridWebView.RawMessageReceivedAction != null || thisAsIHybridWebView.RawMessageReceivedActionWithArgs != null)
        {
            NativeControl.RawMessageReceived += NativeControl_RawMessageReceived;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_RawMessageReceived(object? sender, HybridWebViewRawMessageReceivedEventArgs e)
    {
        var thisAsIHybridWebView = (IHybridWebView)this;
        thisAsIHybridWebView.RawMessageReceivedAction?.Invoke();
        thisAsIHybridWebView.RawMessageReceivedActionWithArgs?.Invoke(sender, e);
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.RawMessageReceived -= NativeControl_RawMessageReceived;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }
}

public partial class HybridWebView : HybridWebView<Microsoft.Maui.Controls.HybridWebView>
{
    public HybridWebView()
    {
    }

    public HybridWebView(Action<Microsoft.Maui.Controls.HybridWebView?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class HybridWebViewExtensions
{
    public static T DefaultFile<T>(this T hybridWebView, string defaultFile)
        where T : IHybridWebView
    {
        hybridWebView.DefaultFile = defaultFile;
        return hybridWebView;
    }

    public static T DefaultFile<T>(this T hybridWebView, Func<string> defaultFileFunc)
        where T : IHybridWebView
    {
        hybridWebView.DefaultFile = new PropertyValue<string>(defaultFileFunc);
        return hybridWebView;
    }

    public static T HybridRoot<T>(this T hybridWebView, string hybridRoot)
        where T : IHybridWebView
    {
        hybridWebView.HybridRoot = hybridRoot;
        return hybridWebView;
    }

    public static T HybridRoot<T>(this T hybridWebView, Func<string> hybridRootFunc)
        where T : IHybridWebView
    {
        hybridWebView.HybridRoot = new PropertyValue<string>(hybridRootFunc);
        return hybridWebView;
    }

    public static T OnRawMessageReceived<T>(this T hybridWebView, Action? rawMessageReceivedAction)
        where T : IHybridWebView
    {
        hybridWebView.RawMessageReceivedAction = rawMessageReceivedAction;
        return hybridWebView;
    }

    public static T OnRawMessageReceived<T>(this T hybridWebView, Action<object?, HybridWebViewRawMessageReceivedEventArgs>? rawMessageReceivedActionWithArgs)
        where T : IHybridWebView
    {
        hybridWebView.RawMessageReceivedActionWithArgs = rawMessageReceivedActionWithArgs;
        return hybridWebView;
    }
}

public static partial class HybridWebViewStyles
{
    public static Action<IHybridWebView>? Default { get; set; }
    public static Dictionary<string, Action<IHybridWebView>> Themes { get; } = [];
}