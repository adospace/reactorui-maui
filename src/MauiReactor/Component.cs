using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MauiReactor.Internals;

namespace MauiReactor
{
    public interface IComponent
    {
    }

    public abstract class Component : VisualNode, IEnumerable<VisualNode>, IVisualNodeWithAttachedProperties
    {
        private readonly Dictionary<BindableProperty, object> _attachedProperties = new();

        public abstract VisualNode Render();

        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;

        private BindableObject? _nativeControl;

        private readonly List<VisualNode> _children = new();

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(params VisualNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            _children.AddRange(nodes);
        }

        protected new IReadOnlyList<VisualNode> Children()
            => _children;

        private IHostElement? GetPageHost()
        {
            var current = Parent;
            while (current != null && current is not IHostElement)
                current = current.Parent;

            return current as IHostElement;
        }

        protected Microsoft.Maui.Controls.Page? ContainerPage
        {
            get
            {
                return GetPageHost()?.ContainerPage;
            }
        }

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            foreach (var attachedProperty in _attachedProperties)
            {
                nativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
            }

            Validate.EnsureNotNull(Parent);

            Parent.AddChild(this, nativeControl);

            _nativeControl = nativeControl;
        }

        protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
        {
            Validate.EnsureNotNull(Parent);

            Parent.RemoveChild(this, nativeControl);
            
            foreach (var attachedProperty in _attachedProperties)
            {
                nativeControl.ClearValue(attachedProperty.Key);
            }

            _nativeControl = null;
        }

        protected sealed override IEnumerable<VisualNode> RenderChildren()
        {
            yield return Render();
        }

        protected sealed override void OnUpdate()
        {
            if (_nativeControl != null)
            {
                foreach (var attachedProperty in _attachedProperties)
                {
                    _nativeControl.SetValue(attachedProperty.Key, attachedProperty.Value);
                }
            }

            base.OnUpdate();
        }

        protected sealed override void OnAnimate()
        {
            base.OnAnimate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType().FullName == GetType().FullName)
            {
                ((Component)newNode)._isMounted = true;
                ((Component)newNode)._nativeControl = _nativeControl;
                _nativeControl = null;
                ((Component)newNode).OnPropsChanged();
                base.MergeWith(newNode);
            }
            else
            {
                Unmount();
            }
        }

        protected sealed override void OnMount()
        {
            //System.Diagnostics.Debug.WriteLine($"Mounting {Key ?? GetType()} under {Parent.Key ?? Parent.GetType()} at index {ChildIndex}");

            base.OnMount();

            OnMounted();
        }

        protected virtual void OnMounted()
        {
        }

        protected sealed override void OnUnmount()
        {
            OnWillUnmount();

            foreach (var child in base.Children)
            {
                child.Unmount();
            }

            base.OnUnmount();
        }

        protected virtual void OnWillUnmount()
        {
        }

        protected virtual void OnPropsChanged()
        { }

        public INavigation? Navigation
            => ReactorApplication.Instance?.Navigation;
    }

    internal interface IComponentWithState
    {
        object State { get; }

        PropertyInfo[] StateProperties { get; }

        void ForwardState(object stateFromOldComponent);
    }

    internal interface IComponentWithProps
    {
        object Props { get; }

        PropertyInfo[] PropsProperties { get; }
    }

    public interface IState
    {
    }

    public interface IProps
    {
    }

    public abstract class ComponentWithProps<P> : Component, IComponentWithProps where P : class, IProps, new()
    {
        public ComponentWithProps(P? props = null)
        {
            Props = props ?? new P();
        }

        public P Props { get; private set; }
        object IComponentWithProps.Props => Props;
        public PropertyInfo[] PropsProperties => typeof(P).GetProperties().Where(_ => _.CanWrite).ToArray();
    }

    public abstract class Component<S, P> : ComponentWithProps<P>, IComponentWithState where S : class, IState, new() where P : class, IProps, new()
    {
        private IComponentWithState? _newComponent;

        protected Component(S? state = null, P? props = null)
            : base(props)
        {
            State = state ?? new S();
        }

        public S State { get; private set; }

        public PropertyInfo[] StateProperties => typeof(S).GetProperties().Where(_ => _.CanWrite).ToArray();

        object IComponentWithState.State => State;

        void IComponentWithState.ForwardState(object stateFromOldComponent)
        {
            stateFromOldComponent.CopyPropertiesTo(State, StateProperties);

            Validate.EnsureNotNull(Application.Current);

            if (Application.Current.Dispatcher.IsDispatchRequired)
                Application.Current.Dispatcher.Dispatch(Invalidate);
            else
                Invalidate();
        }

        protected virtual void SetState(Action<S> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(State);

            if (_newComponent != null)
            {
                _newComponent.ForwardState(State);
                return;
            }

            Validate.EnsureNotNull(Application.Current);

            if (Application.Current.Dispatcher.IsDispatchRequired)
                Application.Current.Dispatcher.Dispatch(Invalidate);
            else
                Invalidate();
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (newNode is IComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;
                State.CopyPropertiesTo(newComponentWithState.State, newComponentWithState.StateProperties);
            }

            if (newNode is IComponentWithProps newComponentWithProps)
            {
                Props.CopyPropertiesTo(newComponentWithProps.Props, newComponentWithProps.PropsProperties);
            }

            base.MergeWith(newNode);
        }
    }

    public class EmptyProps : IProps
    { }

    public abstract class Component<S> : Component<S, EmptyProps> where S : class, IState, new()
    {
        protected Component(S? state = null, EmptyProps? props = null)
            : base(state, props)
        {
        }
    }
}