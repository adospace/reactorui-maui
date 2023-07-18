using System.Collections;
using MauiReactor.Internals;
using MauiReactor.Parameters;

namespace MauiReactor
{
    public abstract class Component : VisualNode, IEnumerable<VisualNode>, IVisualNodeWithAttachedProperties
    {
        private readonly Dictionary<BindableProperty, object> _attachedProperties = new();

        //private ParameterContext? _parameterContext;

        private Component? _newComponent;

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

        protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
        {
            foreach (var attachedProperty in _attachedProperties)
            {
                nativeControl.SetPropertyValue(attachedProperty.Key, attachedProperty.Value);
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
                nativeControl.ResetValue(attachedProperty.Key);
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
                    _nativeControl.SetPropertyValue(attachedProperty.Key, attachedProperty.Value);
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
            if (newNode is Component newComponentMigrated)
            {
                _newComponent = newComponentMigrated;
            }

            //if (_parameterContext != null && newNode is Component newComponent)
            //{
            //    newComponent._parameterContext ??= new ParameterContext(newComponent);
            //    _parameterContext.MigrateTo(newComponent._parameterContext);
            //}

            if (newNode.GetType().FullName == GetType().FullName && _isMounted)
            {
                ((Component)newNode)._isMounted = true;
                ((Component)newNode)._nativeControl = _nativeControl;
                _nativeControl = null;
                ((Component)newNode).OnPropsChanged();
                ((Component)newNode).OnMountedOrPropsChanged();

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
            OnMounted();
            OnMountedOrPropsChanged();

            base.OnMount();
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
        {
        }

        protected virtual void OnMountedOrPropsChanged()
        {

        }

        public INavigation? Navigation
            => ContainerPage?.Navigation ?? NavigationProvider.Navigation;

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
            => ServiceCollectionProvider.ServiceProvider; //ReactorApplicationHost.Instance.Services;

        internal void InvalidateComponent() => Invalidate();

        protected new void Invalidate()
        {
            if (Application.Current != null)
            {
                if (Application.Current.Dispatcher.IsDispatchRequired)
                    Application.Current.Dispatcher.Dispatch(base.Invalidate);
                else
                    base.Invalidate();
            }
            else
            {
                base.Invalidate();
            }
        }

        protected IParameter<T> CreateParameter<T>(string? name = null) where T : new()
        {
            var parameterContext = new ParameterContext(this);
            return parameterContext.Create<T>(name);
        }

        public IParameter<T> GetParameter<T>(string? name = null) where T : new()
        {
            var parameterContext = new ParameterContext(this);
            return parameterContext.Get<T>(name) ?? throw new InvalidOperationException($"Unable to find parameter with name '{name ?? typeof(T).FullName}'");
            //IParameter<T>? parameter = null;
            //Component currentComponent = this;

            //while (true)
            //{
            //    while (currentComponent._newComponent != null)
            //    {
            //        currentComponent = currentComponent._newComponent;
            //    }

            //    var parentComponent = currentComponent.GetParent<Component>();
            //    if (parentComponent == null)
            //        break;

            //    parameter = parentComponent._parameterContext?.Get<T>(name);

            //    if (parameter != null)
            //    {
            //        _parameterContext ??= new ParameterContext(this);
            //        parameter = _parameterContext.Register((parameter as IParameterWithReferences<T>) ?? throw new InvalidOperationException($"Parameter '{name}' is not of type {typeof(T).FullName}"));
            //        break;
            //    }

            //    currentComponent = parentComponent;
            //}

            //return parameter ?? throw new InvalidOperationException($"Unable to find parameter with name '{name ?? typeof(T).FullName}'");
        }
    
        public static VisualNode Render(Func<ComponentContext, VisualNode> renderFunc)
        {
            return new InlineComponent(renderFunc);
        }
    }

    internal interface IComponentWithState
    {
        object State { get; internal set; }

        void ForwardState(object stateFromOldComponent, bool invalidateComponent);

        void InvalidateComponent();

        IComponentWithState? NewComponent { get; }

        void RegisterOnStateChanged(Action action);
    }

    internal interface IComponentWithProps
    {
        object Props { get; internal set; }
    }

    public abstract class ComponentWithProps<P> : Component, IComponentWithProps where P : class, new()
    {
        private readonly bool _derivedProps;
        private P? _props;

        public ComponentWithProps(P? props = null)
        {
            _props = props;
            _derivedProps = props != null;
        }

        public P Props
        {
            get => _props ??= new P();
        }

        object IComponentWithProps.Props
        {
            get => Props;
            set
            {
                if (_props != null)
                {
                    throw new InvalidOperationException();
                }

                _props = (P)value;
            }
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (!_derivedProps && newNode is IComponentWithProps newComponentWithProps)
            {
                if (newNode.GetType() == GetType())
                {
                    newComponentWithProps.Props = Props;
                }
                else
                {
                    CopyObjectExtensions.CopyProperties(Props, newComponentWithProps.Props);
                }
            }

            base.MergeWith(newNode);
        }
    }

    public abstract class Component<S, P> : ComponentWithProps<P>, IComponentWithState where S : class, new() where P : class, new()
    {
        private IComponentWithState? _newComponent;

        private readonly List<Action> _actionsRegisteredOnStateChange = new();

        private readonly bool _derivedState;

        private S? _state;

        protected Component(S? state = null, P? props = null)
            : base(props)
        {
            _state = state;
            _derivedState = state != null;
        }

        public S State 
        {
            get => _state ??= new S();
        }

        object IComponentWithState.State
        {
            get => State;
            set
            {
                if (_state != null)
                {
                    throw new InvalidOperationException();
                }
                _state = (S)value;
            }
        }

        IComponentWithState? IComponentWithState.NewComponent => _newComponent;

        protected override void OnInvalidated()
        {
            var newComponent = _newComponent;
            while (newComponent != null && newComponent.NewComponent != null && newComponent != newComponent.NewComponent)
                newComponent = newComponent.NewComponent;

            if (newComponent != null && newComponent != newComponent.NewComponent)
            {
                newComponent.InvalidateComponent();
            }

            base.OnInvalidated();
        }

        void IComponentWithState.InvalidateComponent() => Invalidate();

        void IComponentWithState.ForwardState(object stateFromOldComponent, bool invalidateComponent)
        {
            CopyObjectExtensions.CopyProperties(stateFromOldComponent, State);

            foreach (var registeredAction in _actionsRegisteredOnStateChange)
            {
                registeredAction.Invoke();
            }

            Validate.EnsureNotNull(Application.Current);

            if (invalidateComponent)
            {
                Invalidate();
            }
        }

        void IComponentWithState.RegisterOnStateChanged(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            _actionsRegisteredOnStateChange.Add(action);
        }

        private bool TryForwardStateToNewComponent(bool invalidateComponent)
        {
            var newComponent = _newComponent;
            while (newComponent != null && newComponent.NewComponent != null && newComponent != newComponent.NewComponent)
                newComponent = newComponent.NewComponent;

            if (newComponent != null)
            {
                newComponent.ForwardState(State, invalidateComponent);
                return true;
            }

            return false;
        }

        protected void SetState(Action<S> action, TimeSpan delay, bool invalidateComponent = true)
            => Application.Current?.Dispatcher.DispatchDelayed(delay, () => SetState(action, invalidateComponent));

        protected void SetState(Action<S> action, int delayMilliseconds, bool invalidateComponent = true)
            => Application.Current?.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(delayMilliseconds), () => SetState(action, invalidateComponent));

        protected virtual void SetState(Action<S> action, bool invalidateComponent = true)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            action(State);

            if (TryForwardStateToNewComponent(invalidateComponent))
                return;

            foreach (var registeredAction in _actionsRegisteredOnStateChange)
            {
                registeredAction.Invoke();
            }

            //Validate.EnsureNotNull(Application.Current);

            if (invalidateComponent && _isMounted)
            {
                Invalidate();
            }
        }

        internal override void MergeWith(VisualNode newNode)
        {
            if (!_derivedState && newNode is IComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;
                if (newNode.GetType() == this.GetType())
                {
                    newComponentWithState.State = State;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("WARNING: State copied!");
                    CopyObjectExtensions.CopyProperties(State, newComponentWithState.State);
                }
            }

            base.MergeWith(newNode);
        }

        internal override void Layout(IComponentWithState? containerComponent = null)
        {
            base.Layout(this);
        }
    }

    public class EmptyProps
    { }

    public abstract class Component<S> : Component<S, EmptyProps> where S : class, new()
    {
        protected Component(S? state = null, EmptyProps? props = null)
            : base(state, props)
        {
        }
    }

}