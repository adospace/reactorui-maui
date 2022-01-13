using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    public abstract class ReactorApplication : VisualNode, IHostElement
    { 
        protected readonly Application _application;

        internal IComponentLoader ComponentLoader { get; set; } = new LocalComponentLoader();

        protected ReactorApplication(Application application)
        {
            Instance = this;

            _application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public static ReactorApplication? Instance { get; private set; }

        public Action<UnhandledExceptionEventArgs>? UnhandledException { get; set; }

        internal void FireUnhandledExpectionEvent(Exception ex)
        {
            UnhandledException?.Invoke(new UnhandledExceptionEventArgs(ex, false));
            System.Diagnostics.Debug.WriteLine(ex);
        }

        public abstract IHostElement Run();

        public abstract void Stop();

        public static ReactorApplication Create<T>(Application application) where T : Component, new() 
            => new ReactorApplication<T>(application);

        public ReactorApplication OnUnhandledException(Action<UnhandledExceptionEventArgs> action)
        {
            UnhandledException = action;
            return this;
        }

        public INavigation? Navigation =>  _application.MainPage?.Navigation;

        public Microsoft.Maui.Controls.Page? ContainerPage => _application?.MainPage;

    }

    public class ReactorApplication<T> : ReactorApplication where T : Component, new()
    {
        private Component? _rootComponent;
        private bool _sleeping = true;


        internal ReactorApplication(Application application)
            :base(application)
        {
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
                _rootComponent ??= ComponentLoader.LoadComponent<T>();
                ComponentLoader.ComponentAssemblyChanged += OnComponentAssemblyChanged;
                _sleeping = false;
                OnLayout();
                ComponentLoader.Run();
            }

            return this;
        }

        private void OnComponentAssemblyChanged(object? sender, EventArgs e)
        {
            try
            {
                var newComponent = ComponentLoader.LoadComponent<T>();
                if (newComponent != null)
                {
                    _rootComponent = newComponent;
                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                FireUnhandledExpectionEvent(ex);
            }
        }

        public override void Stop()
        {
            if (!_sleeping)
            {
                ComponentLoader.ComponentAssemblyChanged -= OnComponentAssemblyChanged;
                _sleeping = true;
                ComponentLoader.Stop();
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
}

