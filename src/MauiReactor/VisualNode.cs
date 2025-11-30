using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using MauiReactor.Animations;
using MauiReactor.Internals;

namespace MauiReactor
{
    public interface IVisualNode
    {
        void AppendAnimatable(BindableProperty key, RxAnimation animation);

        Microsoft.Maui.Controls.Page? GetContainerPage();

        IHostElement? GetPageHost();

        IComponentWithState? GetContainerComponent();

        string? ThemeKey { get; set; }

        void SetProperty(BindableProperty property, object? value);
        
        void SetProperty(BindableProperty property, IPropertyValue? value);

        bool HasPropertySet(BindableProperty property);

    }

    public static class VisualNodeExtensions
    {
        public static T OnPropertyChanged<T>(this T element, Action<object?, PropertyChangedEventArgs> action) where T : VisualNode
        {
            element.PropertyChangedAction = action;
            return element;
        }

        public static T OnPropertyChanging<T>(this T element, Action<object?, System.ComponentModel.PropertyChangingEventArgs> action) where T : VisualNode
        {
            element.PropertyChangingAction = action;
            return element;
        }

        public static T Set<T>(this T node, BindableProperty property, object? value) where T : IVisualNode
        {
            node.SetProperty(property, value);
            return node;
        }

        public static T When<T>(this T node, bool flag, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            if (flag)
            {
                actionToApplyWhenFlagIsTrue(node);
            }
            return node;
        }

        public static T OnAndroid<T>(this T node, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            node.When(DeviceInfo.Current.Platform == DevicePlatform.Android, actionToApplyWhenFlagIsTrue);
            return node;
        }

        public static T OniOS<T>(this T node, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            node.When(DeviceInfo.Current.Platform == DevicePlatform.iOS, actionToApplyWhenFlagIsTrue);
            return node;
        }

        public static T OnMac<T>(this T node, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            node.When(DeviceInfo.Current.Platform == DevicePlatform.macOS ||
                DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst, actionToApplyWhenFlagIsTrue);
            return node;
        }

        public static T OnWindows<T>(this T node, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            node.When(DeviceInfo.Current.Platform == DevicePlatform.WinUI, actionToApplyWhenFlagIsTrue);
            return node;
        }

        public static T OnPhone<T>(this T node, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            node.When(DeviceInfo.Current.Idiom == DeviceIdiom.Phone, actionToApplyWhenFlagIsTrue);
            return node;
        }

        public static T OnDesktop<T>(this T node, Action<T> actionToApplyWhenFlagIsTrue) where T : IVisualNode
        {
            node.When(DeviceInfo.Current.Idiom == DeviceIdiom.Desktop, actionToApplyWhenFlagIsTrue);
            return node;
        }

        public static T WithAnimation<T>(this T node, Easing? easing = null, double duration = 600, double initialDelay = 0) where T : VisualNode
        {
            node.EnableCurrentAnimatableProperties(easing, duration, initialDelay);
            return node;
        }

        public static T WithKey<T>(this T node, object key) where T : VisualNode
        {
            node.Key = key;
            return node;
        }

        public static T WithMetadata<T>(this T node, string key, object value) where T : VisualNode
        {
            node.SetMetadata(key, value);
            return node;
        }

        public static T WithMetadata<T>(this T node, object value) where T : VisualNode
        {
            ArgumentNullException.ThrowIfNull(value);

            node.SetMetadata(value.GetType().FullName ?? throw new InvalidOperationException(), value);
            return node;
        }

        public static T WithOutAnimation<T>(this T node) where T : VisualNode
        {
            node.DisableCurrentAnimatableProperties();
            return node;
        }

        public static T ThemeKey<T>(this T node, string? themeKey) where T : IVisualNode
        {
            node.ThemeKey = themeKey;
            return node;
        }
    }

    public static class VisualNodeStyles
    {
        public static Action<IVisualNode>? Default { get; set; }
    }

    public abstract class VisualNode : IVisualNode, IAutomationItemContainer
    {
        protected bool _isMounted = false;

        protected bool _stateChanged = true;

        protected internal Dictionary<BindableProperty, Animatable>? _animatables;

        private Dictionary<string, object?>? _metadata;

        private IReadOnlyList<VisualNode>? _children = null;

        internal protected bool _invalidated = false;

        private IComponentWithState? _containerComponent;

        private int _childIndex;

        private string? _themeKey;

        [ThreadStatic] 
        internal static bool _skipAnimationMigration;

        protected VisualNode()
        {
            VisualNodeStyles.Default?.Invoke(this);
        }

        internal static readonly BindablePropertyKey _mauiReactorPropertiesBagKey = BindableProperty.CreateAttachedReadOnly(nameof(_mauiReactorPropertiesBagKey),
            typeof(HashSet<BindableProperty>), typeof(VisualNode), null);

        public int ChildIndex
        {
            get
            {
                if (!SupportChildIndexing)
                {
                    throw new InvalidOperationException($"Node of type {GetType()} doesn't support child index property");
                }

                return _childIndex;
            }
            set
            {
                if (!SupportChildIndexing)
                {
                    throw new InvalidOperationException($"Node of type {GetType()} doesn't support child index property");
                }

                _childIndex = value;
            }
        }

        protected virtual bool SupportChildIndexing { get; } = true;

        public object? Key { get; set; }
        
        public Action<object?, PropertyChangedEventArgs>? PropertyChangedAction { get; set; }
        
        public Action<object?, System.ComponentModel.PropertyChangingEventArgs>? PropertyChangingAction { get; set; }
        
        public string? ThemeKey
        {
            get => _themeKey;
            set
            {
                if (_themeKey != value)
                {
                    _themeKey = value;
                    OnThemeChanged();
                }
            }
        }

        protected virtual void OnThemeChanged()
        {

        }

        internal bool IsLayoutCycleRequired { get; set; } = true;
        
        internal virtual VisualNode? Parent { get; set; }

        internal IReadOnlyList<VisualNode> Children
        {
            get
            {
                if (_children == null)
                {
                    var renderedChildrenList = RenderChildren();

                    List<VisualNode> listOfChildren;

                    //try to reduce the number of allocations and iterations

                    if (renderedChildrenList is List<VisualNode> renderedChildren)
                    {
                        var childIndex = 0;
                        List<int>? listOfChildrenToRemove = null;
                        for (int i = 0; i < renderedChildren.Count; i++)
                        {
                            if (renderedChildren[i] != null)
                            {
                                if (renderedChildren[i].SupportChildIndexing)
                                {
                                    renderedChildren[i].ChildIndex = childIndex;
                                    childIndex++;
                                }
                                renderedChildren[i].Parent = this;
                            }
                            else
                            {
                                listOfChildrenToRemove ??= [];
                                listOfChildrenToRemove.Add(i);
                            }
                        }

                        if (listOfChildrenToRemove != null)
                        {
                            for (int i = listOfChildrenToRemove.Count - 1; i >= 0; i--)
                            {
                                renderedChildren.RemoveAt(listOfChildrenToRemove[i]);
                            }
                        }

                        listOfChildren = renderedChildren;
                    }
                    else
                    {
                        listOfChildren = [];
                        var childIndex = 0;
                        foreach (var child in renderedChildrenList)
                        {
                            if (child != null)
                            {
                                listOfChildren.Add(child);
                                if (child.SupportChildIndexing)
                                {
                                    child.ChildIndex = childIndex;
                                    childIndex++;
                                }
                                child.Parent = this;
                            }
                        }
                    }

                    _children = listOfChildren;
                }
                return _children;
            }
        }

        internal virtual void Reset()
        {
            _isMounted = false;
            _stateChanged = true;
            _animatables = null;
            _metadata = null;
            _children = null;
            _invalidated = false;
            _containerComponent = null;
            _childIndex = 0;

            Key = null;
            PropertyChangedAction = null;
            PropertyChangingAction = null;
            IsLayoutCycleRequired = true;
            Parent = null;
        }

        public void AppendAnimatable(BindableProperty key, RxAnimation animation)
        {
            var newAnimatableProperty = new Animatable(key, animation);

            _animatables ??= [];
            _animatables[key] = newAnimatableProperty;
        }

        public T? GetMetadata<T>(string key, T? defaultValue = default)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("can'be null or empty", nameof(key));
            }

            if (_metadata != null && _metadata.TryGetValue(key, out var value))
                return (T?)value;

            return defaultValue;
        }

        public T? GetMetadata<T>(T? defaultValue = default)
            => GetMetadata(typeof(T).FullName ?? throw new InvalidOperationException(), defaultValue);

        public void SetMetadata<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("can'be null or empty", nameof(key));
            }

            _metadata ??= [];
            _metadata[key] = value;
        }

        public void SetMetadata<T>(T value)
        {
            _metadata ??= [];
            _metadata[typeof(T).FullName ?? throw new InvalidOperationException()] = value;
        }

        internal void AddChild(VisualNode widget, BindableObject childNativeControl)
        {
            OnAddChild(widget, childNativeControl);
        }

        internal void DisableCurrentAnimatableProperties()
        {
            if (_animatables != null)
            {
                foreach (var animatable in _animatables
                .Where(_ => _.Value.IsEnabled.GetValueOrDefault(true)))
                {
                    animatable.Value.IsEnabled = false;
                };
            }
        }

        internal void EnableCurrentAnimatableProperties(Easing? easing = null, double duration = 600, double initialDelay = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(duration);
            ArgumentOutOfRangeException.ThrowIfNegative(initialDelay);

            if (_animatables != null/* && duration > 0*/)
            {
                foreach (var animatable in _animatables)
                {
                    if (animatable.Value.IsEnabled.GetValueOrDefault(true))
                    {
                        if (animatable.Value.Animation is RxTweenAnimation tweenAnimation)
                        {
                            tweenAnimation.Easing ??= easing;
                            tweenAnimation.Duration ??= duration;
                            tweenAnimation.InitialDelay ??= initialDelay;
                            if (animatable.Value.IsEnabled.GetValueOrDefault() == false)
                            {
                                animatable.Value.Animation.Start();
                            }
                            animatable.Value.IsEnabled = true;
                        }
                    }
                };
            }
        }

        internal void Update()
        {
            OnUpdate();
            foreach (var child in Children)
            {
                child.Update();
            }
        }

        internal virtual void Layout(IComponentWithState? containerComponent = null)
        {
            if (!IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = false;

            if (_invalidated)
            {
                var oldChildren = Children;
                _children = null;
                MergeChildrenFrom(oldChildren);
                _invalidated = false;
            }

            OnLayout(containerComponent);
        }

        internal virtual void OnLayout(IComponentWithState? containerComponent = null)
        {
            _containerComponent = containerComponent;

            if (!_isMounted && Parent != null)
            {
                OnBeforeMount();
                OnMount();
            }

            CommitAnimations();

            //AnimateThis();

            if (_isMounted && _stateChanged)
            {
                OnUpdate();
            }

            foreach (var child in Children.Where(_ => _.IsLayoutCycleRequired))
            {
                child.Layout(containerComponent);
            }
        }

        internal virtual void CommitAnimations() { }

        internal virtual void MergeChildrenFrom(IReadOnlyList<VisualNode> oldChildren)
        {
            if (oldChildren != Children)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    if (oldChildren.Count > i)
                    {
                        oldChildren[i].MergeWith(Children[i]);
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = Children.Count; i < oldChildren.Count; i++)
                {
                    oldChildren[i].Unmount();
                    oldChildren[i].Parent = null;
                }
            }
        }

        protected virtual void MergeWith(VisualNode newNode)
        {
            if (newNode == this)
                return;

            for (int i = 0; i < Children.Count; i++)
            {
                if (newNode.Children.Count > i)
                {
                    Children[i].MergeWith(newNode.Children[i]);
                }
                else
                {
                    break;
                }
            }

            for (int i = newNode.Children.Count; i < Children.Count; i++)
            {
                Children[i].Unmount();
                Children[i].Parent = null;
            }

            Parent = null;
            _isMounted = false;
        }

        internal void RemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
            if (_isMounted)
            {
                OnRemoveChild(widget, childNativeControl);
            }
        }

        internal void Unmount()
        {
            OnUnmount();
        }

        protected internal virtual void OnLayoutCycleRequested()
        {
        }

        IHostElement? IVisualNode.GetPageHost()
        {
            if (Parent is IVisualNode parentVisualNode)
            {
                return parentVisualNode.GetPageHost();
            }

            return null;
        }

        Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
        {
            return ((IVisualNode?)Parent)?.GetContainerPage();
        }

        IComponentWithState? IVisualNode.GetContainerComponent()
        {
            if (_containerComponent != null)
            {
                return _containerComponent;
            }

            return ((IVisualNode?)Parent)?.GetContainerComponent();
        }

        protected void SetPropertyValue(BindableObject dependencyObject, BindableProperty property, object? propertyValueAsObject)
        {
            if (propertyValueAsObject != null)
            {
                //if (propertyValueAsObject is IPropertyValue propertyValue)
                //{
                //    SetPropertyValue(dependencyObject, property, propertyValue);
                //}
                //else
                //{
                    dependencyObject.SetPropertyValue(property, propertyValueAsObject);
                //}
            }
            else
            {
                dependencyObject.ResetValue(property);
            }
        }

        protected void SetPropertyValue(BindableObject dependencyObject, BindableProperty property, IPropertyValue? propertyValue)
        {
            if (propertyValue != null)
            {
                var newValue = propertyValue.GetValue();

                dependencyObject.SetPropertyValue(property, newValue);                

                if (propertyValue.HasValueFunction)
                {
                    var containerComponent = propertyValue.OwnerComponent ?? ((IVisualNode)this).GetContainerComponent();
                    containerComponent?.RegisterOnStateChanged(this, propertyValue.GetValueAction(dependencyObject, property));
                }
            }
            else
            {
                dependencyObject.ResetValue(property);
            }
        }

        protected virtual T? GetParent<T>() where T : VisualNode
        {
            var parent = Parent;

            if (parent is T)
                return (T?)parent;

            if (parent == null)
                return null;

            return parent.GetParent<T>();
        }

        protected void Invalidate()
        {
            _invalidated = true;

            RequireLayoutCycle();

            OnInvalidated();
        }

        protected virtual void OnAddChild(VisualNode widget, BindableObject childNativeControl)
        {
        }

        protected virtual void OnInvalidated()
        {
        }

        protected virtual void OnMigrating(VisualNode newNode)
        {

        }

        protected virtual void OnMigrated(VisualNode newNode)
        {
            if (_animatables != null && newNode._animatables != null)
            {
                if (!_skipAnimationMigration)
                {
                    foreach (var newAnimatableProperty in newNode._animatables)
                    {
                        if (_animatables.TryGetValue(newAnimatableProperty.Key, out var oldAnimatableProperty))
                        {
                            if (oldAnimatableProperty.Animation.GetType() ==
                                newAnimatableProperty.Value.Animation.GetType())
                            {
                                if (oldAnimatableProperty.IsEnabled.GetValueOrDefault())
                                {
                                    newAnimatableProperty.Value.Animation.MigrateFrom(oldAnimatableProperty.Animation);
                                }
                            }
                        }
                    }
                }
            }

            _animatables = null;
        }

        protected virtual void OnBeforeMount()
        {
        }

        protected virtual void OnMount()
        {
            _isMounted = true;
        }

        protected virtual void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
        {
        }

        protected virtual void OnUnmount()
        {
            _isMounted = false;
            Parent = null;

            foreach (var child in Children)
            {
                if (child._isMounted)
                {
                    child.Unmount();
                }
            }

            //ReleaseNodeToPool(this);
        }

        protected virtual void OnUpdate()
        {
            _stateChanged = false;
        }

        protected virtual IEnumerable<VisualNode?> RenderChildren()
        {
            yield break;
        }

        private void RequireLayoutCycle()
        {
            if (IsLayoutCycleRequired)
                return;

            IsLayoutCycleRequired = true;
            Parent?.RequireLayoutCycle();
            OnLayoutCycleRequested();
        }

        IEnumerable<T> IAutomationItemContainer.Descendants<T>()
        {
            var queue = new Queue<VisualNode>(16);
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                IReadOnlyList<VisualNode> children = queue.Dequeue().Children;
                for (var i = 0; i < children.Count; i++)
                {
                    VisualNode child = children[i];
                    if (child is T childT)
                        yield return childT;

                    if (child is IVisualNodeWithNativeControl childVisualNodeWithNativeControl)
                    {
                        var childNativeControl = childVisualNodeWithNativeControl.GetNativeControl<BindableObject>();

                        if (childNativeControl is T childNativeControlAsT)
                        {
                            yield return childNativeControlAsT;
                        }
                    }

                    if (child is IAutomationItemContainer childAsAutomationItemContainer)
                    {
                        foreach (var foundElementOfTypeT in childAsAutomationItemContainer.Descendants<T>())
                        {
                            yield return foundElementOfTypeT;
                        }
                    }

                    queue.Enqueue(child);
                }
            }
        }

        protected Dictionary<BindableProperty, object?>? _propertiesToSet;
        protected Dictionary<BindableProperty, IPropertyValue?>? _propertyValuesToSet;

        public void SetProperty(BindableProperty property, object? value)
        {
            _propertiesToSet ??= [];
            _propertiesToSet[property] = value;
        }

        public void SetProperty(BindableProperty property, IPropertyValue? value)
        {
            _propertyValuesToSet ??= [];
            _propertyValuesToSet[property] = value;
        }

        public bool HasPropertySet(BindableProperty property)
        {
            if (_propertiesToSet == null && _propertyValuesToSet == null)
                return false;
            return _propertiesToSet?.ContainsKey(property) == true ||
                _propertyValuesToSet?.ContainsKey(property) == true;
        }
    }

    public interface IVisualNodeWithNativeControl : IVisualNode
    {
        TResult? GetNativeControl<TResult>() where TResult : BindableObject;

        void Attach(BindableObject nativeControl);

        bool Animate();
    }

    public abstract class VisualNode<T> : VisualNode, IVisualNodeWithNativeControl where T : BindableObject, new()
    {
        protected T? _nativeControl;

        private Action<T?>? _componentRefAction;

        protected VisualNode(Action<T?>? componentRefAction = null)
        {
            _componentRefAction = componentRefAction;
        }

        protected T? NativeControl { get => _nativeControl; }

        internal Action<T?> ComponentRefAction
        {
            set => _componentRefAction = value;
        }

        protected override void MergeWith(VisualNode newNode)
        {
            if (newNode == this)
                return;

            if (newNode.GetType() == GetType())
            {
                OnMigrating(newNode);
                ((VisualNode<T>)newNode)._nativeControl = this._nativeControl;
                ((VisualNode<T>)newNode)._isMounted = this._nativeControl != null;
                ((VisualNode<T>)newNode)._componentRefAction?.Invoke(NativeControl);

                OnMigrated(newNode);

                base.MergeWith(newNode);

                _nativeControl = null;
            }
            else
            {
                this.Unmount();
            }
        }

        protected override void OnMigrated(VisualNode newNode)
        {
            if (NativeControl != null)
            {
                OnDetachNativeEvents();

                if (_propertiesToSet != null)
                {
                    foreach (var attachedProperty in _propertiesToSet)
                    {
                        if (newNode.HasPropertySet(attachedProperty.Key))
                        {
                            continue;
                        }

                        NativeControl.ResetValue(attachedProperty.Key);
                    }
                }

                if (_propertyValuesToSet != null)
                {
                    foreach (var attachedProperty in _propertyValuesToSet)
                    {
                        if (newNode.HasPropertySet(attachedProperty.Key))
                        {
                            continue;
                        }

                        NativeControl.ResetValue(attachedProperty.Key);
                    }
                }
            }

            _propertiesToSet?.Clear();
            _propertyValuesToSet?.Clear();

            base.OnMigrated(newNode);
        }

        internal override void OnLayout(IComponentWithState? containerComponent = null)
        {
            bool addToParent = !_isMounted && Parent != null;

            base.OnLayout(containerComponent);

            Validate.EnsureNotNull(_nativeControl);

            if (addToParent)
            {
                Parent?.AddChild(this, _nativeControl);
            }            
        }

        protected override void OnMount()
        {
            _nativeControl ??= new T();
            _componentRefAction?.Invoke(NativeControl);

            base.OnMount();
        }

        protected override void OnUnmount()
        {
            if (_nativeControl != null)
            {
                OnDetachNativeEvents();

                Parent?.RemoveChild(this, _nativeControl);

                _nativeControl = null;
                _componentRefAction?.Invoke(null);
            }

            base.OnUnmount();
        }

        protected override void OnUpdate()
        {
            if (NativeControl == null)
            {
                throw new InvalidOperationException();
            }

            if (_propertiesToSet != null)
            {
                foreach (var property in _propertiesToSet)
                {
                    SetPropertyValue(NativeControl, property.Key, property.Value);
                }
            }

            if (_propertyValuesToSet != null)
            {
                foreach (var property in _propertyValuesToSet)
                {
                    SetPropertyValue(NativeControl, property.Key, property.Value);
                }
            }

            OnAttachNativeEvents();

            base.OnUpdate();
        }

        protected virtual void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            if (PropertyChangedAction != null)
            {
                NativeControl.PropertyChanged += NativeControl_PropertyChanged;
            }
            if (PropertyChangingAction != null)
            {
                NativeControl.PropertyChanging += NativeControl_PropertyChanging;
            }
        }

        protected virtual void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
                NativeControl.PropertyChanging -= NativeControl_PropertyChanging;
            }
        }

        private void NativeControl_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            PropertyChangedAction?.Invoke(sender, e);
        }

        private void NativeControl_PropertyChanging(object? sender, Microsoft.Maui.Controls.PropertyChangingEventArgs e)
        {
            PropertyChangingAction?.Invoke(sender, new System.ComponentModel.PropertyChangingEventArgs(e.PropertyName));
        }

        TResult? IVisualNodeWithNativeControl.GetNativeControl<TResult>() where TResult : class
        {
            if (_nativeControl == null)
            {
                return default;
            }

            return (_nativeControl as TResult) ??
                throw new InvalidOperationException($"Unable to convert from type {typeof(T)} to type {typeof(TResult)} when getting the native control");
        }

        void IVisualNodeWithNativeControl.Attach(BindableObject nativeControl)
        {
            _nativeControl = (T?)nativeControl;
        }

        bool IVisualNodeWithNativeControl.Animate()
        {
            if (_isMounted)
            {
                bool animated = false;
                if (_animatables != null)
                {
                    foreach (var animatable in _animatables)
                    {
                        if (animatable.Value.IsEnabled.GetValueOrDefault() &&
                            !animatable.Value.Animation.IsCompleted())
                        {
                            var newValue = animatable.Value.Animation.GetCurrentValue();

                            Validate.EnsureNotNull(NativeControl);

                            if (newValue is IPropertyValue propertyValue)
                            {
                                newValue = propertyValue.GetValue();
                            }

                            Validate.EnsureNotNull(NativeControl);

                            var oldValue = NativeControl.GetValue(animatable.Key);

                            if (!CompareUtils.AreEquals(oldValue, newValue))
                            {
                                NativeControl.SetValue(animatable.Key, newValue);
                            }

                            SetProperty(animatable.Key, newValue);

                            animated = true;
                        }
                    }
                }

                return animated;
            }

            return false;
        }

        internal override void CommitAnimations()
        {
            if (_animatables?.Any(_ => _.Value.IsEnabled.GetValueOrDefault() && !_.Value.Animation.IsCompleted()) == true)
            {
                var pageHost = ((IVisualNode)this).GetPageHost();
                pageHost?.RequestAnimationFrame(this);

                ((IVisualNodeWithNativeControl)this).Animate();
            }
        }

        Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
        {
            if (_nativeControl is Microsoft.Maui.Controls.Page containerPage)
            {
                return containerPage;
            }

            return ((IVisualNode?)Parent)?.GetContainerPage();
        }
    }

    internal static class NativeControlExtensions
    {
        public static bool SetPropertyValue(this BindableObject dependencyObject, BindableProperty property, object? newValue)
        {
            var oldValue = dependencyObject.GetValue(property);

            if (!CompareUtils.AreEquals(oldValue, newValue))
            {
#if DEBUG
                //System.Diagnostics.Debug.WriteLine($"{dependencyObject.GetType()} set property {property.PropertyName} to {newValue}");
#endif
                var propertiesBag = (HashSet<BindableProperty>?)dependencyObject.GetValue(VisualNode._mauiReactorPropertiesBagKey.BindableProperty);
                if (propertiesBag == null)
                {
                    dependencyObject.SetValue(VisualNode._mauiReactorPropertiesBagKey, propertiesBag = []);
                }

                propertiesBag.Add(property);

                dependencyObject.SetValue(property, newValue);

                return true;
            }

            return false;
        }

        public static void ResetValue(this BindableObject dependencyObject, BindableProperty property)
        {
            var propertiesBag = (HashSet<BindableProperty>?)dependencyObject.GetValue(VisualNode._mauiReactorPropertiesBagKey.BindableProperty);
            if (propertiesBag != null &&
                propertiesBag.Contains(property))
            {
                dependencyObject.ClearValue(property);
            }
        }
    }
}