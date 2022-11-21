using MauiReactor.Internals;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal class PageHost<T> : VisualNode, IVisualNode, IHostElement where T : Component, new()
    {
        private Component? _component;

        private bool _sleeping;

        public Microsoft.Maui.Controls.Page? ContainerPage { get; private set; }

        //private IDispatcherTimer? _animationTimer;

        private readonly LinkedList<VisualNode> _listOfVisualsToAnimate = new();

        protected PageHost()
        {
        }

        Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
        {
            return ContainerPage;
        }

        IHostElement? IVisualNode.GetPageHost()
        {
            return this;
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
                //Device.BeginInvokeOnMainThread(OnLayout);
                ContainerPage?.Dispatcher.Dispatch(OnLayout);
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
            if (_listOfVisualsToAnimate.Count > 0 && Application.Current != null)
            {
                Application.Current.Dispatcher.Dispatch(AnimationCallback);
            }
        }
        private void AnimationCallback()
        {
            if (_sleeping)
            {
                return;
            }

            if (Application.Current != null && AnimateVisuals())
            {
                Application.Current.Dispatcher.Dispatch(AnimationCallback);
            }
        }

        private bool AnimateVisuals()
        {
            if (_listOfVisualsToAnimate.Count == 0)
                return false;

            bool animated = false;
            LinkedListNode<VisualNode>? nodeToAnimate = _listOfVisualsToAnimate.First;
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

        public void RequestAnimationFrame(VisualNode visualNode)
        {
            _listOfVisualsToAnimate.AddFirst(visualNode);
        }
    }

    internal class PageHost<T, P> : PageHost<T> where T : Component, new() where P : class, new()
    {
        private readonly Action<P> _propsInitializer;

        protected PageHost(Action<P> stateInitializer)
        {
            _propsInitializer = stateInitializer;
        }

        public static Microsoft.Maui.Controls.Page CreatePage(Action<P> stateInitializer) 
        {
            var host = new PageHost<T, P>(stateInitializer);
            host.Run();
            return Validate.EnsureNotNull(host.ContainerPage);
        }

        protected override Component InitializeComponent(Component component)
        {
            if (component is not ComponentWithProps<P> componentWithProps)
                throw new InvalidOperationException($"Component type ({component.GetType()}) should derive from ComponentWithProps<{typeof(P)}>");

            _propsInitializer.Invoke(componentWithProps.Props);
            return component;
        }

    }
}
