﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.HotReload;
using MauiReactor.Internals;
using Microsoft.Extensions.Logging;

namespace MauiReactor.Integration;

public class ComponentHost : Microsoft.Maui.Controls.ContentView
{
    internal class ComponentHostNode : VisualNode, IVisualNode, IHostElement, ITemplateHost, ITypeLoaderEventConsumer
    {
        private readonly ComponentHost _host;
        private Component? _component;
        private bool _sleeping;

        private Microsoft.Maui.Controls.Page? _containerPage;

        private readonly LinkedList<IVisualNodeWithNativeControl> _listOfVisualsToAnimate = new();

        public ComponentHostNode(ComponentHost host)
        {
            _host = host;
        }

        public Microsoft.Maui.Controls.Page? ContainerPage => _containerPage ??= _host.GetParent<Microsoft.Maui.Controls.Page>();

        BindableObject? ITemplateHost.NativeElement => ContainerPage;

        public void OnAssemblyChanged()
        {
            if (!MauiReactorFeatures.HotReloadIsEnabled)
            {
                throw new InvalidOperationException();
            }            
            
            Validate.EnsureNotNull(_component);


            try
            {
                var newComponent = HotReloadTypeLoader.Instance.LoadObject<Component>(_component.GetType());
                if (newComponent != null)
                {
                    _component = newComponent;

                    Invalidate();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to hot reload component {_component.GetType().FullName}: type not found in received assembly");
                }
            }
            catch (Exception ex)
            {
                var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ComponentHostNode>>();
                logger?.LogError(ex, "Unable to layout component");

                System.Diagnostics.Debug.WriteLine(ex);
                ReactorApplicationHost.FireUnhandledExceptionEvent(ex);
            }
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
            }
            catch (Exception ex)
            {
                var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<ComponentHostNode>>();
                logger?.LogError(ex, "Unable to layout component");

                ReactorApplicationHost.FireUnhandledExceptionEvent(ex);
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

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            if (nativeControl is View contentView)
            {
                _host.Content = contentView;
            }
            else
            {
                throw new NotSupportedException($"Invalid root component ({nativeControl.GetType()}): must be a View-derived class");
            }
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            _host.Content = null;
        }

        IHostElement IHostElement.Run()
        {
            _sleeping = false;

            if (MauiReactorFeatures.HotReloadIsEnabled)
            { 
                HotReloadTypeLoader.Instance.Run();
                HotReloadTypeLoader.Instance.AssemblyChangedEvent?.AddListener(this);
            }

            OnLayout();

            return this;
        }

        void IHostElement.Stop()
        {
            if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                HotReloadTypeLoader.Instance.AssemblyChangedEvent?.RemoveListener(this);
            }
            _sleeping = true;
        }

        void IHostElement.RequestAnimationFrame(IVisualNodeWithNativeControl visualNode)
        {
            _listOfVisualsToAnimate.AddFirst(visualNode);
        }

        public void SetupComponent(Type? componentType)
        {
            var thisAsHostElement = (IHostElement)this;
            thisAsHostElement.Stop();
            
            if (componentType != null)
            {
                _component = (Component)(Activator.CreateInstance(componentType) ?? throw new InvalidOperationException($"Unable to create an instance of type {componentType}"));

                thisAsHostElement.Run();
            }
        }
    }

    private readonly ComponentHostNode _componentHostNode;

    public static readonly BindableProperty ComponentProperty = BindableProperty.Create(nameof(Component), typeof(Type), typeof(ComponentHost), null,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var componentHost = (ComponentHost)bindableObject;
                componentHost._componentHostNode.SetupComponent((Type?)newValue);
            }));

    public ComponentHost()
    {
        _componentHostNode = new(this);
    }

    protected override void OnHandlerChanged()
    {
        ServiceCollectionProvider.ServiceProvider ??= Handler?.MauiContext?.Services!;
        base.OnHandlerChanged();
    }

    public Type? Component
    {
        get => (Type?)GetValue(ComponentProperty);
        set => SetValue(ComponentProperty, value);
    }
}


