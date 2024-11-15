using System.Collections;
using MauiReactor.Internals;
using MauiReactor.Parameters;

namespace MauiReactor
{
    public abstract partial class Component : VisualNode, IEnumerable<VisualNode>, IVisualNodeWithAttachedProperties
    {

        private BindableObject? _nativeControl;

        private readonly List<VisualNode> _children = [];

        private readonly Dictionary<BindableProperty, object> _attachedProperties = [];

        public abstract VisualNode Render();

        public void SetAttachedProperty(BindableProperty property, object value)
            => _attachedProperties[property] = value;

        public bool HasAttachedProperty(BindableProperty property)
            => _attachedProperties.ContainsKey(property);

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
            ArgumentNullException.ThrowIfNull(nodes);

            _children.AddRange(nodes);
        }

        //protected static T GetNodeFromPool<T>(VisualNode[] nodes) where T : Component, new()
        //{
        //    var node = GetNodeFromPool<T>();
        //    node.Add(nodes);
        //    return node;
        //}

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

        //protected sealed override void OnAnimate()
        //{
        //    base.OnAnimate();
        //}

        protected override void MergeWith(VisualNode newNode)
        {
            if (newNode.GetType().FullName == GetType().FullName && _isMounted)
            {
                OnMigrating(newNode);
                ((Component)newNode)._isMounted = true;
                ((Component)newNode)._nativeControl = _nativeControl;
                _nativeControl = null;
                ((Component)newNode).OnPropsChanged();
                ((Component)newNode).OnMountedOrPropsChanged();
                OnMigrated(newNode);

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
            if (!_invalidated)
            {
                if (Application.Current != null)
                {
                    if (Application.Current.Dispatcher.IsDispatchRequired)
                    {
                        _invalidated = true;
                        Application.Current.Dispatcher.Dispatch(base.Invalidate);
                    }
                    else
                        base.Invalidate();
                }
                else
                {
                    base.Invalidate();
                }
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
        }
        protected IParameter<T> GetOrCreateParameter<T>(string? name = null) where T : new()
        {
            var parameterContext = new ParameterContext(this);
            return parameterContext.Get<T>(name) ?? parameterContext.Create<T>(name);
        }

        //public static VisualNode Render(Func<ComponentContext, VisualNode> renderFunc)
        //{
        //    return new InlineComponent(renderFunc);
        //}

        public static VisualNode Render<S>(Func<ComponentContextState<S>, VisualNode> renderFunc, S? defaultValue = default)
        {
            return new InlineComponent<S>(renderFunc, defaultValue);
        }
    }

    public interface IComponentWithState
    {
        object State { get; internal set; }

        void ForwardState(object stateFromOldComponent, bool invalidateComponent);

        void InvalidateComponent();

        IComponentWithState? NewComponent { get; }

        void RegisterOnStateChanged(IVisualNode owner, Action action);
    }

    internal interface IComponentWithProps
    {
        object Props { get; internal set; }
    }

    public abstract class ComponentWithProps<P>(P? props = null) : Component, IComponentWithProps where P : class, new()
    {
        private bool _derivedProps = props != null;
        private P? _props = props;

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
                    throw new InvalidOperationException("Unable to set props on new component: Has Props been accessed from constructor?");
                }

                _props = (P)value;
            }
        }

        protected override void MergeWith(VisualNode newNode)
        {
            if (!_derivedProps && newNode != this && newNode is IComponentWithProps newComponentWithProps)
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

        private record RegisteredAction(WeakReference<IVisualNode> OwnerRef, Action Action)
        {
            internal bool IsOwnerAlive() => OwnerRef.TryGetTarget(out var _);
        }

        private List<RegisteredAction>? _actionsRegisteredOnStateChange;

        private bool _derivedState;

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
                    throw new InvalidOperationException("Unable to set State on new component: Has State been accessed from constructor?");
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
            if (stateFromOldComponent.GetType() == typeof(S))
            { 
                _state = (S)stateFromOldComponent;
            }
            else if (stateFromOldComponent.GetType().FullName == typeof(S).FullName)
            { 
                CopyObjectExtensions.CopyProperties(stateFromOldComponent, State);
            }

            if (_actionsRegisteredOnStateChange != null)
            {
                List<RegisteredAction> liveActions = new(_actionsRegisteredOnStateChange.Count);
                foreach (var registeredAction in _actionsRegisteredOnStateChange)
                {
                    if (registeredAction.IsOwnerAlive())
                    {
                        registeredAction.Action.Invoke();
                        liveActions.Add(registeredAction);
                    }
                }

                _actionsRegisteredOnStateChange = liveActions;
            }

            if (invalidateComponent)
            {
                Invalidate();
            }
        }

        void IComponentWithState.RegisterOnStateChanged(IVisualNode owner, Action action)
        {
            ArgumentNullException.ThrowIfNull(action);

            _actionsRegisteredOnStateChange ??= [];
            _actionsRegisteredOnStateChange.Add(new RegisteredAction(new WeakReference<IVisualNode>(owner), action));
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

        protected new void Invalidate()
        {
            if (TryForwardInvalidateToNewComponent())
            {
                return;
            }

            base.Invalidate();
        }

        private bool TryForwardInvalidateToNewComponent()
        {
            var newComponent = _newComponent;
            while (newComponent != null && newComponent.NewComponent != null && newComponent != newComponent.NewComponent)
                newComponent = newComponent.NewComponent;

            if (newComponent != null)
            {
                newComponent.InvalidateComponent();
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
            ArgumentNullException.ThrowIfNull(action);

            var currentDispather = Application.Current?.Dispatcher;
            if (currentDispather != null)
            {
                if (currentDispather.IsDispatchRequired)
                {
                    currentDispather.Dispatch(()=>SetState(action, invalidateComponent));
                    return;
                }
            }

            action(State);

            if (TryForwardStateToNewComponent(invalidateComponent))
            {
                return;
            }

            if (_actionsRegisteredOnStateChange != null)
            {
                List<RegisteredAction> liveActions = new(_actionsRegisteredOnStateChange.Count);
                foreach (var registeredAction in _actionsRegisteredOnStateChange)
                {
                    if (registeredAction.IsOwnerAlive())
                    {
                        registeredAction.Action.Invoke();
                        liveActions.Add(registeredAction);
                    }
                }

                _actionsRegisteredOnStateChange = liveActions;
            }

            if (invalidateComponent && !_isMounted)
            {
                System.Diagnostics.Debug.WriteLine($"WARNING: You are calling SetState on an unmounted component '{this.GetType().Name}'");
            }

            if (invalidateComponent && _isMounted)
            {
                Invalidate();
            }
        }

        protected override void MergeWith(VisualNode newNode)
        {
            if (!_derivedState && newNode != this && newNode is IComponentWithState newComponentWithState)
            {
                _newComponent = newComponentWithState;
                if (newNode.GetType() == this.GetType())
                {
                    newComponentWithState.State = State;
                }
                else if (newNode.GetType().FullName == this.GetType().FullName)
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