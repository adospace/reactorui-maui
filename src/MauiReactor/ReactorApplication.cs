﻿using MauiReactor.HotReload;
using MauiReactor.Internals;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Dispatching;
using System.Diagnostics;
using System.Reflection;

namespace MauiReactor;

internal abstract class ReactorApplicationHost : VisualNode, IHostElement, IVisualNode, ITypeLoaderEventConsumer
{
    protected readonly ReactorApplication _application;

    protected ReactorApplicationHost(ReactorApplication application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));

        if (MauiReactorFeatures.HotReloadIsEnabled)
        {
            HotReloadTypeLoader.Instance.AssemblyChangedEvent?.AddListener(this);
        }
    }

    public static Action<UnhandledExceptionEventArgs>? UnhandledException { get; set; }

    internal static void FireUnhandledExceptionEvent(Exception ex)
    {
        UnhandledException?.Invoke(new UnhandledExceptionEventArgs(ex, false));
    }

    public Microsoft.Maui.Controls.Window? MainWindow { get; protected set; }

    public abstract IHostElement Run();

    public abstract void Stop();

    public virtual void OnAssemblyChanged()
    { }

    public abstract void RequestAnimationFrame(IVisualNodeWithNativeControl visualNode);

    public Microsoft.Maui.Controls.Page? ContainerPage { get; protected set; } //=> _application?.MainPage;

    IHostElement? IVisualNode.GetPageHost()
    {
        return this;
    }

    Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
    {
        return ContainerPage;
    }
}

internal class ReactorApplicationHost<T> : ReactorApplicationHost where T : Component, new()
{
    private Component? _rootComponent;
    private bool _sleeping = false;
    private bool _started = false;
    private bool _layoutCallEnqueued;

    private readonly LinkedList<IVisualNodeWithNativeControl> _listOfVisualsToAnimate = new();

    internal ReactorApplicationHost(ReactorApplication<T> application)
        : base(application)
    {
        _application.RequestedThemeChanged += OnApplicationRequestedThemeChanged;
    }

    private void OnApplicationRequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
    {
        if (_application.Theme != null)
        {
            _application.Theme.Apply();
            _rootComponent?.InvalidateComponent();
        }
    }

    protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
    {
        if (nativeControl is Microsoft.Maui.Controls.Page page)
        {
            //_application.MainPage = page;
            ContainerPage = page;

            if (page.Parent is Microsoft.Maui.Controls.Window mainWindow)
            {
                MainWindow = mainWindow;
            }
        }
        else if (nativeControl is Microsoft.Maui.Controls.Window mainWindow)
        {
            MainWindow = mainWindow;
        }
        else
        {
            throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. ContentPage, Shell etc)");
        }
    }

    protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
    {
        //MainPage can't be set to null!
        //_application.MainPage = null;
    }

    public void Pause()
    {
        _sleeping = true;
    }

    public void Resume()
    {
        _sleeping = false;
    }

    public override IHostElement Run()
    {
        if (!_started)
        {
            _started = true;
            _application.Theme?.Apply();
            _rootComponent ??= new T();
            OnLayout();
            if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                HotReloadTypeLoader.Instance.Run();
            }

            if (MauiReactorFeatures.FrameRateIsEnabled)
            {
                FrameRateIndicator.Start();
            }
        }

        return this;
    }

    public override void OnAssemblyChanged()
    {
        try
        {
            if (_application.Theme != null)
            {
                _application.Theme = MauiReactorFeatures.HotReloadIsEnabled ?
                    HotReloadTypeLoader.Instance.LoadObject<Theme>(_application.Theme.GetType()) :
                    (Theme?)Activator.CreateInstance(_application.Theme.GetType());
                _application.Theme?.Apply();
            }

            var newComponent = MauiReactorFeatures.HotReloadIsEnabled ?
                HotReloadTypeLoader.Instance.LoadObject<Component>(typeof(T)) :
                new T();

            if (newComponent != null &&
                _rootComponent != newComponent)
            {
                _rootComponent = newComponent;

                Invalidate();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Unable to hot-reload component {typeof(T).FullName}: type not found in received assembly");
                var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ReactorApplicationHost<T>>>();
                logger?.LogError("Unable to hot reload component {Type}: type not found in received assembly", typeof(T).FullName);
            }
        }
        catch (Exception ex)
        {
            var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ReactorApplicationHost<T>>>();
            logger?.LogError(ex, "Unable to hot reload component {Type}: type not found in received assembly", typeof(T).FullName);

            FireUnhandledExceptionEvent(
                new InvalidOperationException($"Unable to hot reload component {typeof(T).FullName}: type not found in received assembly", ex));

            System.Diagnostics.Debug.WriteLine(ex);
        }

    }

    public override void Stop()
    {
        if (_started)
        {
            _started = false;
            if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                HotReloadTypeLoader.Instance.Stop();
            }

            if (MauiReactorFeatures.FrameRateIsEnabled)
            {
                FrameRateIndicator.Stop();
            }
        }
    }

    protected internal override void OnLayoutCycleRequested()
    {
        if (_started && !_sleeping && Application.Current != null)
        {
            //var logger = ServiceCollectionProvider
            //    .ServiceProvider
            //    .GetService<ILogger<ReactorApplication>>();
            if (!_layoutCallEnqueued)
            {
                //logger?.LogDebug("Dispatch layout callback");
                _layoutCallEnqueued = true;
                Application.Current.Dispatcher.Dispatch(OnLayout);
            }
            //else
            //{
            //    logger?.LogDebug("Queued layout callback");
            //}
        }

        base.OnLayoutCycleRequested();
    }

    private void OnLayout()
    {
        _layoutCallEnqueued = false;

        try
        {
            var logger = ServiceCollectionProvider
                .ServiceProvider?
                .GetService<ILogger<ReactorApplication>>();
                
            if (logger != null &&
                logger.IsEnabled(LogLevel.Debug))
            {
                DateTime now = DateTime.Now;
                Layout();
                logger.LogDebug("Layout time: {elapsedMilliseconds}ms", (DateTime.Now - now).TotalMilliseconds);
            }
            else
            {
                Layout();
            }

            if (_listOfVisualsToAnimate.Count > 0)
            {
                AnimationCallback();
            }
        }
        catch (Exception ex)
        {
            var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ReactorApplicationHost<T>>>();
            logger?.LogError(ex, "Unable to layout component {Type}", typeof(T).FullName);

            FireUnhandledExceptionEvent(ex);
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    protected override IEnumerable<VisualNode?> RenderChildren()
    {
        yield return _rootComponent;
    }

    public override void RequestAnimationFrame(IVisualNodeWithNativeControl visualNode)
    {
        _listOfVisualsToAnimate.AddFirst(visualNode);
    }

    private void AnimationCallback()
    {
        if (!_started || _sleeping || Application.Current == null)
        {
            return;
        }

        DateTime now = DateTime.Now;
        if (AnimateVisuals())
        {
            //System.Diagnostics.Debug.WriteLine($"{(DateTime.Now - now).TotalMilliseconds}");
            var elapsedMilliseconds = (DateTime.Now - now).TotalMilliseconds;

            if (elapsedMilliseconds > 16)
            {
                //System.Diagnostics.Debug.WriteLine($"[MauiReactor] FPS WARNING {elapsedMilliseconds}ms");
                ServiceCollectionProvider
                    .ServiceProvider?
                    .GetService<ILogger<ReactorApplication>>()?
                    .LogWarning("FPS drop: {elapsedMilliseconds}ms to render a frame", elapsedMilliseconds);
                Application.Current.Dispatcher.Dispatch(AnimationCallback);
            }
            else
            {
                Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(16 - elapsedMilliseconds), AnimationCallback);
            }
        }
    }

    private bool AnimateVisuals()
    {
        if (_listOfVisualsToAnimate.Count == 0)
            return false;

        bool animated = false;
        LinkedListNode<IVisualNodeWithNativeControl>? nodeToAnimate = _listOfVisualsToAnimate.First;
        while (nodeToAnimate != null)
        {
            var nextNode = nodeToAnimate.Next;

            if (nodeToAnimate.Value.Animate())
            {
                animated = true;
            }
            else
            {
                _listOfVisualsToAnimate.Remove(nodeToAnimate);
            }

            nodeToAnimate = nextNode;
        }

        return animated;
    }
}

public abstract class ReactorApplication : Application
{
    protected ReactorApplication(IServiceProvider serviceProvider)
    {
        ServiceCollectionProvider.ServiceProvider = serviceProvider;
    }

    public Action<Uri>? AppLinkRequestReceived { get; set; }

    public Theme? Theme { get; internal set; }

    protected override void OnAppLinkRequestReceived(Uri uri)
    {
        AppLinkRequestReceived?.Invoke(uri);

        base.OnAppLinkRequestReceived(uri);
    }

}

public class ReactorApplication<T> : ReactorApplication where T : Component, new()
{

    private ReactorApplicationHost<T>? _host;

    public ReactorApplication(IServiceProvider serviceProvider)
        :base(serviceProvider)
    { }

    protected override Microsoft.Maui.Controls.Window CreateWindow(IActivationState? activationState)
    {
        _host ??= new ReactorApplicationHost<T>(this);
        _host.Run();

        if (_host.MainWindow != null)
        {
            return _host.MainWindow;
        }

        if (_host.ContainerPage != null)
        {
            if (_host.ContainerPage.Parent is Microsoft.Maui.Controls.Window window)
            {
                return window;
            }

            return new Microsoft.Maui.Controls.Window(_host.ContainerPage);
        }

        return base.CreateWindow(activationState);
    }


    protected override void OnStart()
    {
        _host?.Run();
        base.OnStart();
    }

    protected override void OnResume()
    {
        //https://github.com/adospace/reactorui-maui/issues/26
        //seems like some devices (Android 9.0?) do not send (or Maui app doesn't receive) the resume event
        //so for now do not suspend the event loop (actually it's even not required at all to suspend it as the app itself is suspended by the os)
        //_host?.Resume();
        base.OnResume();
    }

    protected override void OnSleep()
    {
        //do not pause the event loop: see OnResume() above
        //_host?.Pause();
        base.OnSleep();
    }

    protected override void CleanUp()
    {
        _host?.Stop();
        base.CleanUp();
    }
}

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder UseMauiReactorApp<TComponent>(this MauiAppBuilder appBuilder, 
        Action<ReactorApplication>? configureApplication = null,
        Action? onHotReloadCompleted = null,
        Action<UnhandledExceptionEventArgs>? unhandledExceptionAction = null) where TComponent : Component, new()
        => appBuilder.UseMauiApp(sp =>
        {
            if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                HotReloadTypeLoader.Instance.OnHotReloadCompleted = onHotReloadCompleted;
            }

            ReactorApplicationHost.UnhandledException = unhandledExceptionAction;

            var app = new ReactorApplication<TComponent>(sp);
            configureApplication?.Invoke(app);
            return app;
        });

    //public static MauiAppBuilder EnableMauiReactorHotReload(this MauiAppBuilder appBuilder, Action? onHotReloadCompleted = null)
    //{
    //    TypeLoader.UseRemoteLoader = true;
    //    TypeLoader.OnHotReloadCompleted = onHotReloadCompleted;
    //    ServiceCollectionProvider.EnableHotReload = true;
    //    return appBuilder;
    //}

    //public static MauiAppBuilder EnableFrameRateIndicator(this MauiAppBuilder appBuilder)
    //{
    //    ReactorApplicationHost._showFrameRate = true;
    //    return appBuilder;
    //}

    //public static MauiAppBuilder OnMauiReactorUnhandledException(this MauiAppBuilder appBuilder, Action<UnhandledExceptionEventArgs> unhandledExceptionAction)
    //{
    //    ReactorApplicationHost.UnhandledException = unhandledExceptionAction;
    //    return appBuilder;
    //}

}

public static class ApplicationExtensions
{
    //public static Application AddResource(this Application application, string resourceName, Assembly? containerAssembly = null)
    //{
    //    var resourceDictionary = new ResourceDictionary();
    //    //resourceDictionary.SetAndCreateSource(new Uri(resourceName, UriKind.Relative));

    //    //var methodInfo = typeof(ResourceDictionary).GetMethod("SetAndLoadSource", BindingFlags.NonPublic | BindingFlags.Instance);
    //    //if (methodInfo != null)
    //    //{
    //    //    var parameters = new object?[]
    //    //    {
    //    //        new Uri(resourceName, UriKind.Relative),
    //    //        resourceName,
    //    //        containerAssembly ?? Assembly.GetCallingAssembly(),
    //    //        null
    //    //    };
    //    //    methodInfo.Invoke(resourceDictionary, parameters);
    //    //}
    //    //else
    //    //{
    //    //    throw new InvalidOperationException("Method 'SetAndLoadSource' not found.");
    //    //}
    //    //resourceDictionary.SetAndLoadSource(
    //    //    new Uri(resourceName, UriKind.Relative),
    //    //    resourceName,
    //    //    containerAssembly ?? Assembly.GetCallingAssembly(),
    //    //    null);

    //    application.Resources.MergedDictionaries.Add(resourceDictionary);

    //    return application;
    //}

    public static Application SetWindowsSpecificAssetsDirectory(this Application application, string directoryName)
    {
        Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.Application.SetImageDirectory(application, directoryName);

        return application;
    }

    public static Application UseTheme<T>(this ReactorApplication application) where T : Theme, new()
    {
        application.Theme = new T();
        return application;
    }

    public static Application OnUnhandledException(this ReactorApplication application, Action<UnhandledExceptionEventArgs>? unhandledExceptionAction = null)
    {
        ReactorApplicationHost.UnhandledException = unhandledExceptionAction;
        return application;
    }

    public static Application OnHotReloadCompleted(this ReactorApplication application, Action? onHotReloadCompleted = null)
    {
        if (MauiReactorFeatures.HotReloadIsEnabled)
        {
            HotReloadTypeLoader.Instance.OnHotReloadCompleted = onHotReloadCompleted;
        }
        return application;
    }
}

