using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal class PageHost<T> : VisualNode, IHostElement where T : Component, new()
    {
        private Component? _component;

        private bool _sleeping;

        public Microsoft.Maui.Controls.Page? ContainerPage { get; private set; }

        protected PageHost()
        {
        }

        public static Microsoft.Maui.Controls.Page CreatePage()
        {
            var host = new PageHost<T>();
            host.Run();
            return host.ContainerPage ?? throw new InvalidOperationException();
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Microsoft.Maui.Controls.Page page)
            {
                ContainerPage = page;
                ContainerPage.Appearing += OnComponentPage_Appearing;
                ContainerPage.Disappearing += OnComponentPage_Disappearing;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. ContentPage, Shell etc)");
            }
        }

        private void OnComponentPage_Appearing(object? sender, EventArgs e)
        {
            _sleeping = false;
            OnLayoutCycleRequested();
        }

        private void OnComponentPage_Disappearing(object? sender, EventArgs e)
        {
            _sleeping = true;
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            if (ContainerPage != null)
            {
                ContainerPage.Appearing -= OnComponentPage_Appearing;
                ContainerPage.Disappearing -= OnComponentPage_Disappearing;
            }

            ContainerPage = null;
        }

        protected virtual Component InitializeComponent(Component component)
        {
            return component;
        }

        public IHostElement Run()
        {
            _component ??= InitializeComponent(new T());

            Validate.EnsureNotNull(ReactorApplicationHost.Instance);

            ReactorApplicationHost.Instance.ComponentLoader.AssemblyChanged += OnComponentAssemblyChanged;

            OnLayout();

            if (ContainerPage == null)
            {
                throw new InvalidOperationException($"Component {_component.GetType()} doesn't render a page as root");
            }

            return this;
        }

        private void OnComponentAssemblyChanged(object? sender, EventArgs e)
        {
            Validate.EnsureNotNull(ReactorApplicationHost.Instance);

            try
            {
                var newComponent = ReactorApplicationHost.Instance.ComponentLoader.LoadComponent<T>();
                if (newComponent != null)
                {
                    _component = newComponent;

                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot relead component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                ReactorApplicationHost.Instance.FireUnhandledExpectionEvent(ex);
            }
        }

        public void Stop()
        {
            Validate.EnsureNotNull(ReactorApplicationHost.Instance);

            ReactorApplicationHost.Instance.ComponentLoader.AssemblyChanged -= OnComponentAssemblyChanged;
            _sleeping = true;
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
            Validate.EnsureNotNull(ReactorApplicationHost.Instance);

            try
            {
                Layout();
                SetupAnimationTimer();
            }
            catch (Exception ex)
            {
                ReactorApplicationHost.Instance?.FireUnhandledExpectionEvent(ex);
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Validate.EnsureNotNull(_component);
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

    internal class PageHost<T, P> : PageHost<T> where T : Component, new() where P : class, IProps, new()
    {
        private readonly Action<P> _propsInitializer;

        protected PageHost(Action<P> stateInitializer)
        {
            _propsInitializer = stateInitializer;
        }

        public static Microsoft.Maui.Controls.Page? CreatePage(Action<P> stateInitializer) 
        {
            var host = new PageHost<T, P>(stateInitializer);
            host.Run();
            return host.ContainerPage;
        }

        protected override Component InitializeComponent(Component component)
        {
            if (!(component is ComponentWithProps<P> componentWithProps))
                throw new InvalidOperationException($"Component type ({component.GetType()}) should derive from RxComponentWithProps<{typeof(P)}>");

            _propsInitializer?.Invoke(componentWithProps.Props);
            return component;
        }

    }
}
