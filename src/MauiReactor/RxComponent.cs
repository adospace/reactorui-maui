using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MauiReactor.Internals;

namespace MauiReactor
{
    public interface IRxComponent
    {
    }

    public abstract class RxComponent : VisualNode, IEnumerable<VisualNode>, IVisualNodeWithAttachedProperties
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

        private IRxHostElement? GetPageHost()
        {
            var current = Parent;
            while (current != null && current is not IRxHostElement)
                current = current.Parent;

            return current as IRxHostElement;
        }

        protected Page? ContainerPage
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
                ((RxComponent)newNode)._isMounted = true;
                ((RxComponent)newNode)._nativeControl = _nativeControl;
                _nativeControl = null;
                ((RxComponent)newNode).OnPropsChanged();
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
            => RxApplication.Instance?.Navigation;
    }

    internal interface IRxComponentWithState
    {
        object State { get; }

        PropertyInfo[] StateProperties { get; }

        void ForwardState(object stateFromOldComponent);
    }

    internal interface IRxComponentWithProps
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

    public abstract class RxComponentWithProps<P> : RxComponent, IRxComponentWithProps where P : class, IProps, new()
    {
        public RxComponentWithProps(P? props = null)
        {
            Props = props ?? new P();
        }

        public P Props { get; private set; }
        object IRxComponentWithProps.Props => Props;
        public PropertyInfo[] PropsProperties => typeof(P).GetProperties().Where(_ => _.CanWrite).ToArray();
    }

    public abstract class RxComponent<S, P> : RxComponentWithProps<P>, IRxComponentWithState where S : class, IState, new() where P : class, IProps, new()
    {
        private IRxComponentWithState? _newComponent;

        protected RxComponent(S? state = null, P? props = null)
            : base(props)
        {
            State = state ?? new S();
        }

        public S State { get; private set; }

        public PropertyInfo[] StateProperties => typeof(S).GetProperties().Where(_ => _.CanWrite).ToArray();

        object IRxComponentWithState.State => State;

        void IRxComponentWithState.ForwardState(object stateFromOldComponent)
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
            if (newNode is IRxComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;
                State.CopyPropertiesTo(newComponentWithState.State, newComponentWithState.StateProperties);
            }

            if (newNode is IRxComponentWithProps newComponentWithProps)
            {
                Props.CopyPropertiesTo(newComponentWithProps.Props, newComponentWithProps.PropsProperties);
            }

            base.MergeWith(newNode);
        }
    }

    public class EmptyProps : IProps
    { }

    public abstract class RxComponent<S> : RxComponent<S, EmptyProps> where S : class, IState, new()
    {
        protected RxComponent(S? state = null, EmptyProps? props = null)
            : base(state, props)
        {
        }
    }
}