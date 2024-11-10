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
        private readonly INavigation _navigation;
        protected bool _sleeping;
        protected bool _unloading;

        public PageHost(INavigation navigation)
        {
            _navigation = navigation;
        }

        private WeakReference<Microsoft.Maui.Controls.Page>? _containerPage;

        public Microsoft.Maui.Controls.Page? ContainerPage
        {
            get
            {
                if (_containerPage?.TryGetTarget(out var page) == true)
                { 
                    return page; 
                }

                return null;
            }
            private set
            {
                if (value == null)
                {
                    _containerPage = null;
                    return;
                }

                if (_containerPage?.TryGetTarget(out var _) == true)
                {
                    throw new InvalidOperationException();
                }

                _containerPage = new WeakReference<Microsoft.Maui.Controls.Page>(value);
            }
        }

        //private void CheckUnloading()
        //{
        //    var containerPage = ContainerPage;
        //    if (containerPage == null ||
        //        (!_navigation.NavigationStack.Contains(containerPage) && 
        //        !_navigation.ModalStack.Contains(containerPage)))
        //    {
        //        if (!_unloading)
        //        {
        //            _unloading = true;
        //            Invalidate();
        //        }
        //    }
        //    else if (containerPage != null &&
        //        (
        //        _navigation.NavigationStack.Count > 0 && _navigation.NavigationStack[_navigation.NavigationStack.Count - 1] == containerPage ||
        //        _navigation.ModalStack.Count > 0 && _navigation.ModalStack[_navigation.ModalStack.Count - 1] == containerPage
        //        ))
        //    {
        //        Application.Current?.Dispatcher.Dispatch(CheckUnloading);
        //    }
        //}

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (ContainerPage != null)
            {
#if DEBUG
                if (ContainerPage != nativeControl)
                {
                    throw new InvalidOperationException("PageHost can only host one page");
                }
#endif

                return;
            }

            if (nativeControl is Microsoft.Maui.Controls.Page page)
            {
                ContainerPage = page;
                page.SetValue(MauiReactorPageHostBagKey, this);
                page.Appearing += ComponentPage_Appearing;
                page.Disappearing += ContainerPage_Disappearing;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a page (i.e. ContentPage, Shell etc)");
            }
        }

        private void ContainerPage_Disappearing(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{ContainerPage?.Title} Disappearing");
            //Application.Current?.Dispatcher.Dispatch(CheckUnloading);
            _unloading = true;
            Invalidate();
        }

        private void ComponentPage_Appearing(object? sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"{ContainerPage?.Title} Appearing");
            if (_unloading)
            {
                _unloading = false;
                IsLayoutCycleRequired = true;
                Application.Current?.Dispatcher.Dispatch(OnRecyclingPage);
            }
            else
            {
                _sleeping = false;
                OnLayoutCycleRequested();
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            //var containerPage = ContainerPage;
            //if (containerPage != null)
            //{
            //    System.Diagnostics.Debug.WriteLine($"{containerPage.Title} OnRemoveChild");

            //    containerPage.SetValue(MauiReactorPageHostBagKey, null);

            //    containerPage.Appearing -= ComponentPage_Appearing;
            //    containerPage.Disappearing -= ContainerPage_Disappearing;
            //}

            //ContainerPage = null;
        }

        protected abstract void OnRecyclingPage();


        internal static BindablePropertyKey MauiReactorPageHostBagKey = BindableProperty.CreateAttachedReadOnly(nameof(MauiReactorPageHostBagKey),
            typeof(ITemplateHost), typeof(PageHost), null);
    }

    internal class PageHost<T> : PageHost, IVisualNode, IHostElement, ITemplateHost, ITypeLoaderEventConsumer where T : Component, new()
    {
        private Component? _component;

        BindableObject? ITemplateHost.NativeElement => ContainerPage;

        private readonly LinkedList<IVisualNodeWithNativeControl> _listOfVisualsToAnimate = new();

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
            _sleeping = false;
            _component ??= InitializeComponent(TypeLoader.Instance.LoadObject<Component>(typeof(T)));

            TypeLoader.Instance.Run();
            TypeLoader.Instance.AssemblyChangedEvent?.AddListener(this);

            OnLayout();

            if (ContainerPage == null)
            {
                throw new InvalidOperationException($"Component {_component.GetType()} doesn't render a page as root");
            }
            
            _isMounted = true;

            return this;
        }

        protected override void OnRecyclingPage()
        {
            Reset();
            Run();
        }

        public void OnAssemblyChanged()
        {
            try
            {
                var newComponent = TypeLoader.Instance.LoadObject<Component>(typeof(T));
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
            TypeLoader.Instance.AssemblyChangedEvent?.RemoveListener(this);
            _component = null;
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
                    Stop();
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

        public void RequestAnimationFrame(IVisualNodeWithNativeControl visualNode)
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
