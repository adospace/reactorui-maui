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
        private static readonly HashSet<PageHost> _pageHostCache = [];
        private readonly INavigation _navigation;
        protected bool _sleeping;
        protected bool _unloading;

        public PageHost(INavigation navigation)
        {
            _navigation = navigation;
            foreach(var pageHost in _pageHostCache.ToArray())
            {
                pageHost.CheckUnloading();
            }
        }

        public Microsoft.Maui.Controls.Page? ContainerPage { get; private set; }

        private void CheckUnloading()
        {
            if (ContainerPage != null &&
                !_navigation.NavigationStack.Contains(ContainerPage) && 
                !_navigation.ModalStack.Contains(ContainerPage))
            {
                _unloading = true;
                Invalidate();
                _pageHostCache.Remove(this);
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is Microsoft.Maui.Controls.Page page)
            {
                ContainerPage = page;
                ContainerPage.SetValue(MauiReactorPageHostBagKey, this);
                ContainerPage.Appearing += ComponentPage_Appearing;
                ContainerPage.Disappearing += ContainerPage_Disappearing;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. ContentPage, Shell etc)");
            }
        }

        private void ContainerPage_Disappearing(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{ContainerPage?.Title} Disappearing");
            CheckUnloading();
        }

        private void ComponentPage_Appearing(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{ContainerPage?.Title} Appearing");
            _sleeping = false;
            OnLayoutCycleRequested();
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            if (ContainerPage != null)
            {
                System.Diagnostics.Debug.WriteLine($"{ContainerPage.Title} OnRemoveChild");

                ContainerPage.SetValue(MauiReactorPageHostBagKey, null);

                ContainerPage.Appearing -= ComponentPage_Appearing;
                ContainerPage.Disappearing -= ContainerPage_Disappearing;
            }

            ContainerPage = null;
        }


        internal static BindablePropertyKey MauiReactorPageHostBagKey = BindableProperty.CreateAttachedReadOnly(nameof(MauiReactorPageHostBagKey),
            typeof(ITemplateHost), typeof(PageHost), null);
    }


    internal class PageHost<T> : PageHost, IVisualNode, IHostElement, ITemplateHost where T : Component, new()
    {
        private Component? _component;

        BindableObject? ITemplateHost.NativeElement => ContainerPage;

        private readonly LinkedList<VisualNode> _listOfVisualsToAnimate = new();

        private readonly Action<object>? _propsInitializer;

        protected PageHost(INavigation navigation, Action<object>? propsInitializer = null)
            :base(navigation)
        {
            _propsInitializer = propsInitializer;
        }

        Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
        {
            return ContainerPage;
        }

        IHostElement? IVisualNode.GetPageHost()
        {
            return this;
        }


        public static Microsoft.Maui.Controls.Page CreatePage(INavigation nagivation, Action<object>? propsInitializer = null)
        {
            var host = new PageHost<T>(nagivation, propsInitializer);
            host.Run();
            return host.ContainerPage ?? throw new InvalidOperationException();
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
                ReactorApplicationHost.FireUnhandledExceptionEvent(
                    new InvalidOperationException($"Unable to hot reload component {typeof(T).FullName}: type not found in received assembly", ex));
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

                TemplateHost.FireLayoutCycleExecuted(this);
            }

            base.OnLayoutCycleRequested();
        }

        private void OnLayout()
        {
            try
            {
                Layout();
                SetupAnimationTimer();

                if (_unloading)
                {
                    _sleeping = true;
                }
            }
            catch (Exception ex)
            {
                ReactorApplicationHost.FireUnhandledExceptionEvent(ex);
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            if (_component == null || _unloading)
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

        protected PageHost(INavigation navigation, Action<P> propsInitializer)
            :base(navigation)
        {
            _propsInitializer = propsInitializer;
        }

        public static Microsoft.Maui.Controls.Page CreatePage(INavigation navigation, Action<P> propsInitializer) 
        {
            var host = new PageHost<T, P>(navigation, propsInitializer);
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
