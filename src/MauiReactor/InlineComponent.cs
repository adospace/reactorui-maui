using MauiReactor.Internals;

namespace MauiReactor
{
    public class InlineComponent : Component<InlineComponentState>
    {
        private readonly Func<ComponentContext, VisualNode> _renderFunc;

        public InlineComponent(Func<ComponentContext, VisualNode> renderFunc)
        {
            _renderFunc = renderFunc;
        }

        public override VisualNode Render()
        {
            return _renderFunc(new ComponentContext(this));
        }

        internal void SetStateCore(object? newStateValue, bool invalidateComponent = true)
        {
            SetState(s => s.StateValue = newStateValue, invalidateComponent);
        }

        internal void SetStateCore(object? newStateValue, TimeSpan delay, bool invalidateComponent = true)
        {
            SetState(s => s.StateValue = newStateValue, delay, invalidateComponent);
        }
    }


    public class ComponentContextState<S>
    {
        private readonly InlineComponent _component;
        private readonly S? _initialState;

        internal ComponentContextState(InlineComponent component, S? initialState = default)
        {
            _component = component;
            _initialState = initialState;
        }

        public S? Value => (S?)(_component.State.StateValue ?? _initialState ?? default);

        public void Set(Func<S?, S?> setStateFunction, bool invalidateComponent = true)
        {
            var newState = setStateFunction(Value);
            _component.SetStateCore(newState, invalidateComponent);
        }

        public void Set(Func<S?, S?> setStateFunction, TimeSpan delay, bool invalidateComponent = true)
        {
            var newState = setStateFunction(Value);
            _component.SetStateCore(newState, delay, invalidateComponent);
        }

        public void Set(Func<S?, S?> setStateFunction, int delayMilliseconds, bool invalidateComponent = true)
        {
            var newState = setStateFunction(Value);
            _component.SetStateCore(newState, TimeSpan.FromMilliseconds(delayMilliseconds), invalidateComponent);
        }
    }

    public class ComponentContext
    {
        private readonly InlineComponent _component;

        internal ComponentContext(InlineComponent component)
        {
            _component = component;
        }

        public ComponentContextState<S> UseState<S>()
            => new(_component);
        public ComponentContextState<S> UseState<S>(S defaultValue)
            => new(_component, defaultValue);

        public IServiceProvider Services
            => ServiceCollectionProvider.ServiceProvider;
    }

    public class InlineComponentState
    {
        public object? StateValue { get; set; }
    }
}