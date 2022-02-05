using MauiReactor.HotReload;
using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MauiReactor
{
    internal abstract class ReactorApplicationHost : VisualNode, IHostElement
    { 
        protected readonly Application _application;

        //internal IComponentLoader ComponentLoader { get; set; } = new LocalComponentLoader();

        protected ReactorApplicationHost(Application application)
        {
            Instance = this;

            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public static ReactorApplicationHost? Instance { get; private set; }

        public Action<UnhandledExceptionEventArgs>? UnhandledException { get; set; }

        internal void FireUnhandledExpectionEvent(Exception ex)
        {
            UnhandledException?.Invoke(new UnhandledExceptionEventArgs(ex, false));
            System.Diagnostics.Debug.WriteLine(ex);
        }

        public abstract IHostElement Run();

        public abstract void Stop();

        public ReactorApplicationHost OnUnhandledException(Action<UnhandledExceptionEventArgs> action)
        {
            UnhandledException = action;
            return this;
        }

        public INavigation? Navigation =>  _application.MainPage?.Navigation;

        public Microsoft.Maui.Controls.Page? ContainerPage => _application?.MainPage;

    }

    internal class ReactorApplicationHost<T> : ReactorApplicationHost where T : Component, new()
    {
        private Component? _rootComponent;
        private bool _sleeping = true;
        private readonly HotReloadServer? _hotReloadServer;

        internal ReactorApplicationHost(Application application, bool enableHotReload)
            :base(application)
        {
            if (enableHotReload)
            {
                _hotReloadServer = new HotReloadServer(OnComponentAssemblyChanged);
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Microsoft.Maui.Controls.Page page)
                _application.MainPage = page;
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. RxContentPage, RxShell etc)");    
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            //MainPage can't be set to null!
            //_application.MainPage = null;
        }

        public override IHostElement Run()
        {
            if (_sleeping)
            {
                _rootComponent ??= new T();
                _sleeping = false;
                OnLayout();
                _hotReloadServer?.Run();
            }

            return this;
        }

        private void OnComponentAssemblyChanged(Assembly assembly)
        {
            try
            {
                var newComponent = ReactorApplicationHost<T>.LoadComponent(assembly);
                if (newComponent != null)
                {
                    _rootComponent = newComponent;
                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot reload component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        public static Component? LoadComponent(Assembly assembly)
        {
            if (assembly == null)
                return new T();

            var type = assembly.GetType(Validate.EnsureNotNull(typeof(T).FullName));

            if (type == null)
            {
                return null;
                //throw new InvalidOperationException($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
            }

            return (Component?)Activator.CreateInstance(type);
        }

        public override void Stop()
        {
            if (!_sleeping)
            {
                _sleeping = true;
                _hotReloadServer?.Stop();
            }
        }

        protected internal override void OnLayoutCycleRequested()
        {
            if (!_sleeping)
            {
                Device.BeginInvokeOnMainThread(OnLayout);
            }
            base.OnLayoutCycleRequested();
        }

        private void OnLayout()
        {
            try
            {
                Layout();
                SetupAnimationTimer();
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        protected override IEnumerable<VisualNode?> RenderChildren()
        {
            yield return _rootComponent;
        }

        private void SetupAnimationTimer()
        {
            if (IsAnimationFrameRequested)
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(16), () =>
                {
                    Animate();
                    return IsAnimationFrameRequested;
                });
            }
        }

    }

    public abstract class ReactorApplication : Application
    { 
        internal static bool HotReloadEnabled { get; set; }
    }

    public class ReactorApplication<T> : ReactorApplication where T : Component, new()
    {

        private ReactorApplicationHost<T>? _host;

        protected override Window CreateWindow(IActivationState? activationState)
        {
            _host ??= new ReactorApplicationHost<T>(this, HotReloadEnabled);
            _host.Run();
            return base.CreateWindow(activationState);
        }

        public override void CloseWindow(Window window)
        {
            base.CloseWindow(window);
        }

        protected override void OnStart()
        {
            _host?.Run();
            base.OnStart();
        }

        protected override void OnSleep()
        {
            _host?.Stop();
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
        public static MauiAppBuilder UseMauiReactorApp<TComponent>(this MauiAppBuilder appBuilder, Action<Application>? configureApplication = null) where TComponent : Component, new()
            => appBuilder.UseMauiApp(sp => 
            {
                var app = new ReactorApplication<TComponent>();
                configureApplication?.Invoke(app);
                return app;
            });

        public static MauiAppBuilder EnableMauiReactorHotReload(this MauiAppBuilder appBuilder)
        {
            ReactorApplication.HotReloadEnabled = true;
            return appBuilder;
        }
    }

    public static class ApplicationExtensions
    {
        public static Application AddResource(this Application application, string resourceName, Assembly? containerAssembly = null)
        {
            var resourceDictionary = new ResourceDictionary();
            resourceDictionary.SetAndLoadSource(
                new Uri(resourceName, UriKind.Relative),
                resourceName,
                containerAssembly ?? Assembly.GetCallingAssembly(),
                null);

            application.Resources.Add(resourceDictionary);

            return application;
        }

        public static Application SetWindowsSpecificAssectDirectory(this Application application, string directoryName)
        {
            Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific.Application.SetImageDirectory(application, directoryName);

            return application;
        }
    }
}

