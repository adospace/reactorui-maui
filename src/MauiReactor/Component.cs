using System.Collections;
using MauiReactor.Internals;
using MauiReactor.Parameters;

namespace MauiReactor
{
    public interface IComponent
    {
    }

    public abstract class Component : VisualNode, IEnumerable<VisualNode>, IVisualNodeWithAttachedProperties
    {
        private readonly Dictionary<BindableProperty, object> _attachedProperties = new();

        private ParameterContext? _parameterContext;

        public abstract VisualNode Render();

        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;

        public bool HasAttachedProperty(BindableProperty property)
            => _attachedProperties.ContainsKey(property);

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

        //protected new Microsoft.Maui.Controls.Page? ContainerPage
        //{
        //    get
        //    {
        //        //if (Parent == null)
        //        //{
        //        //    return null;
        //        //}

        //        //return Parent.ContainerPage;
        //        return GetPageHost()?.ContainerPage;
        //    }
        //}

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
            if (_parameterContext != null && newNode is Component newComponent)
            {
                newComponent._parameterContext ??= new ParameterContext(newComponent);
                _parameterContext.MigrateTo(newComponent._parameterContext);
            }

            if (newNode.GetType().FullName == GetType().FullName && _isMounted)
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
            => ContainerPage?.Navigation;

        private Microsoft.Maui.Controls.Page? _containerPage;
        public Microsoft.Maui.Controls.Page? ContainerPage
        {
            get
            {
                _containerPage ??= ((IVisualNode)this).GetContainerPage();
                return _containerPage;
            }
        }

        public static IServiceProvider Services
            => ReactorApplicationHost.Instance.Services;

        internal void InvalidateComponent()
        {
            if (Application.Current == null)
            {
                return;
            }

            if (Application.Current.Dispatcher.IsDispatchRequired)
                Application.Current.Dispatcher.Dispatch(Invalidate);
            else
                Invalidate();
        }

        protected IParameter<T> CreateParameter<T>(string? name = null) where T : new()
        {
            _parameterContext ??= new ParameterContext(this);
            return _parameterContext.Create<T>(name);
        }

        public IParameter<T> GetParameter<T>(string? name = null) where T : new()
        {
            IParameter<T>? parameter = _parameterContext?.Get<T>(name);

            while (true)
            {
                var parentComponent = GetParent<Component>();
                if (parentComponent == null)
                    break;

                parameter = parentComponent._parameterContext?.Get<T>(name);

                if (parameter != null)
                {
                    _parameterContext ??= new ParameterContext(this);
                    parameter = _parameterContext.Register((parameter as IParameterWithReferences<T>) ?? throw new InvalidOperationException($"Parameter '{name}' is not of type {typeof(T).FullName}"));
                    break;
                }
            }

            return parameter ?? throw new InvalidOperationException($"Unable to find parameter with name '{typeof(T).FullName}'");
        }
    }

    internal interface IComponentWithState
    {
        object State { get; }

        void ForwardState(object stateFromOldComponent, bool invalidateComponent);

        void InvalidateComponent();

        IComponentWithState? NewComponent { get; }

        void RegisterOnStateChanged(Action action);
    }

    internal interface IComponentWithProps
    {
        object Props { get; }
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
    }

    public abstract class Component<S, P> : ComponentWithProps<P>, IComponentWithState where S : class, IState, new() where P : class, IProps, new()
    {
        private IComponentWithState? _newComponent;

        private readonly List<Action> _actionsRegisterdOnStateChange = new();

        private readonly bool _derivedState;

        protected Component(S? state = null, P? props = null)
            : base(props)
        {
            State = state ?? new S();
            _derivedState = state != null;
        }

        public S State { get; private set; }

        object IComponentWithState.State => State;

        IComponentWithState? IComponentWithState.NewComponent => _newComponent;

        protected override void OnInvalidated()
        {
            var newComponent = _newComponent;
            while (newComponent != null && newComponent.NewComponent != null)
                newComponent = newComponent.NewComponent;

            if (newComponent != null)
            {
                newComponent.InvalidateComponent();
            }

            base.OnInvalidated();
        }

        void IComponentWithState.InvalidateComponent() => Invalidate();

        void IComponentWithState.ForwardState(object stateFromOldComponent, bool invalidateComponent)
        {
            CopyObjectExtensions.CopyProperties(stateFromOldComponent, State);

            foreach (var registeredAction in _actionsRegisterdOnStateChange)
            {
                registeredAction.Invoke();
            }

            Validate.EnsureNotNull(Application.Current);

            if (invalidateComponent)
            {
                if (Application.Current.Dispatcher.IsDispatchRequired)
                    Application.Current.Dispatcher.Dispatch(Invalidate);
                else
                    Invalidate();
            }
        }

        void IComponentWithState.RegisterOnStateChanged(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _actionsRegisterdOnStateChange.Add(action);
        }

        private bool TryForwardStateToNewComponent(bool invalidateComponent)
        {
            var newComponent = _newComponent;
            while (newComponent != null && newComponent.NewComponent != null)
                newComponent = newComponent.NewComponent;

            if (newComponent != null)
            {
                newComponent.ForwardState(State, invalidateComponent);
                return true;
            }

            return false;
        }

        protected virtual void SetState(Action<S> action, bool invalidateComponent = true)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(State);

            if (TryForwardStateToNewComponent(invalidateComponent))
                return;

            foreach (var registeredAction in _actionsRegisterdOnStateChange)
            {
                registeredAction.Invoke();
            }

            Validate.EnsureNotNull(Application.Current);

            if (invalidateComponent)
            {
                if (Application.Current.Dispatcher.IsDispatchRequired)
                    Application.Current.Dispatcher.Dispatch(Invalidate);
                else
                    Invalidate();
            }
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (!_derivedState && newNode is IComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;
                CopyObjectExtensions.CopyProperties(State, newComponentWithState.State);
            }

            if (newNode is IComponentWithProps newComponentWithProps)
            {
                CopyObjectExtensions.CopyProperties(Props, newComponentWithProps.Props);
            }

            base.MergeWith(newNode);
        }

        internal override void Layout(IComponentWithState? containerComponent = null)
        {
            base.Layout(this);
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