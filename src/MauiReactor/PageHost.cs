using MauiReactor.HotReload;
using MauiReactor.Internals;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    internal abstract class PageHost : VisualNode
    {
        internal static BindablePropertyKey MauiReactorPageHostBagKey = BindableProperty.CreateAttachedReadOnly(nameof(MauiReactorPageHostBagKey),
            typeof(ITemplateHost), typeof(PageHost), null);
    }


    internal class PageHost<T> : PageHost, IVisualNode, IHostElement, ITemplateHost where T : Component, new()
    {
        private Component? _component;

        private bool _sleeping;
        private bool _disappearing;

        public Microsoft.Maui.Controls.Page? ContainerPage { get; private set; }

        BindableObject? ITemplateHost.NativeElement => ContainerPage;

        private EventHandler? _layoutCycleExecuted;

        private readonly LinkedList<VisualNode> _listOfVisualsToAnimate = new();

        private readonly Action<object>? _propsInitializer;

        protected PageHost(Action<object>? propsInitializer = null)
        {
            _propsInitializer = propsInitializer;
        }

        event EventHandler? ITemplateHost.LayoutCycleExecuted
        {
            add
            {
                _layoutCycleExecuted += value;
            }
            remove
            {
                _layoutCycleExecuted -= value;
            }
        }

        Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
        {
            return ContainerPage;
        }

        IHostElement? IVisualNode.GetPageHost()
        {
            return this;
        }


        public static Microsoft.Maui.Controls.Page CreatePage(Action<object>? propsInitializer = null)
        {
            var host = new PageHost<T>(propsInitializer);
            host.Run();
            return host.ContainerPage ?? throw new InvalidOperationException();
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Microsoft.Maui.Controls.Page page)
            {
                ContainerPage = page;
                ContainerPage.SetValue(MauiReactorPageHostBagKey, this);
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
            _disappearing = true;
            Invalidate();
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            if (ContainerPage != null)
            {
                ContainerPage.SetValue(MauiReactorPageHostBagKey, null);

                ContainerPage.Appearing -= OnComponentPage_Appearing;
                ContainerPage.Disappearing -= OnComponentPage_Disappearing;
            }

            ContainerPage = null;
        }

        protected virtual Component InitializeComponent(Component component)
        {
            if (_propsInitializer != null)
            {
                if (component is not IComponentWithProps componentWithProps)
                    throw new InvalidOperationException($"Component type ({component.GetType()}) should derive from ComponentWithProps<...>");

                _propsInitializer.Invoke(componentWithProps.Props);
                return component;
            }

            return component;
        }

        public IHostElement Run()
        {
            _component ??= InitializeComponent(new T());

            ComponentLoader.Instance.Run();
            ComponentLoader.Instance.AssemblyChanged += OnComponentAssemblyChanged;

            OnLayout();

            if (ContainerPage == null)
            {
                throw new InvalidOperationException($"Component {_component.GetType()} doesn't render a page as root");
            }
            
            _isMounted = true;

            return this;
        }

        private void OnComponentAssemblyChanged(object? sender, EventArgs e)
        {
            //Validate.EnsureNotNull(ReactorApplicationHost.Instance);

            try
            {
                var newComponent = ComponentLoader.Instance.LoadComponent(typeof(T));
                if (newComponent != null)
                {
                    _component = newComponent;

                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot reload component {typeof(T).FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                ReactorApplicationHost.Instance?.FireUnhandledExceptionEvent(ex);
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void Stop()
        {
            ComponentLoader.Instance.AssemblyChanged -= OnComponentAssemblyChanged;
            _sleeping = true;
        }

        protected internal override void OnLayoutCycleRequested()
        {
            if (!_sleeping)
            {
                if (Application.Current == null)
                {
                    OnLayout();
                }
                else
                {
                    ContainerPage?.Dispatcher.Dispatch(OnLayout);
                }

                _layoutCycleExecuted?.Invoke(this, EventArgs.Empty);
            }

            base.OnLayoutCycleRequested();
        }

        private void OnLayout()
        {
            try
            {
                Layout();
                SetupAnimationTimer();

                if (_disappearing)
                {
                    _sleeping = true;
                }
            }
            catch (Exception ex)
            {
                ReactorApplicationHost.Instance?.FireUnhandledExceptionEvent(ex);
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            if (_component == null || _disappearing)
            {
                yield break;
            }

            yield return _component;
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

        protected PageHost(Action<P> propsInitializer)
        {
            _propsInitializer = propsInitializer;
        }

        public static Microsoft.Maui.Controls.Page CreatePage(Action<P> propsInitializer) 
        {
            var host = new PageHost<T, P>(propsInitializer);
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
