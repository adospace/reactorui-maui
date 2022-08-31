using MauiReactor.Animations.Internals;
using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Animations
{
    public interface IAnimationController
    {
        PropertyValue<bool>? IsEnabled { get; set; }

        PropertyValue<bool>? IsPaused { get; set; }

        Action<bool>? IsEnabledChangedAction { get; set; }

        Action<bool>? IsPausedChangedAction { get; set; }

    }

    public class AnimationController : VisualNode<MauiReactor.Animations.Internals.AnimationController>, IAnimationController, IEnumerable
    {
        PropertyValue<bool>? IAnimationController.IsEnabled { get; set; }

        PropertyValue<bool>? IAnimationController.IsPaused { get; set; }

        Action<bool>? IAnimationController.IsEnabledChangedAction { get; set; }
        Action<bool>? IAnimationController.IsPausedChangedAction { get; set; }


        protected readonly List<VisualNode> _internalChildren = new();

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }


        protected override bool SupportChildIndexing => false;

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MauiReactor.Animations.Internals.Animation animation)
            {
                NativeControl.Children.Insert(widget.ChildIndex, animation);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MauiReactor.Animations.Internals.Animation animation)
            {
                NativeControl.Children.Remove(animation);
            }

            base.OnRemoveChild(widget, childControl);
        }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIAnimationController = (IAnimationController)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.AnimationController.IsEnabledProperty, thisAsIAnimationController.IsEnabled);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.AnimationController.IsPausedProperty, thisAsIAnimationController.IsPaused);


            base.OnUpdate();
        }

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIAnimationController = (IAnimationController)this;
            if (thisAsIAnimationController.IsEnabledChangedAction != null)
            {
                NativeControl.IsEnabledChanged += NativeControl_IsEnabledChanged;
                NativeControl.IsPausedChanged += NativeControl_IsPausedChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_IsEnabledChanged(object? sender, GenericAnimationEventArgs<bool> e)
        {
            var thisAsIAnimationController = (IAnimationController)this;
            thisAsIAnimationController.IsEnabledChangedAction?.Invoke(e.Value);
        }

        private void NativeControl_IsPausedChanged(object? sender, GenericAnimationEventArgs<bool> e)
        {
            var thisAsIAnimationController = (IAnimationController)this;
            thisAsIAnimationController.IsPausedChangedAction?.Invoke(e.Value);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.IsEnabledChanged -= NativeControl_IsEnabledChanged;
                NativeControl.IsPausedChanged -= NativeControl_IsPausedChanged;
            }

            base.OnDetachNativeEvents();
        }

        protected override void OnUnmount()
        {
            Validate.EnsureNotNull(NativeControl);

            //NativeControl.IsEnabled = false;

            base.OnUnmount();
        }
    }

    public static class AnimationControllerExtensions
    {
        public static T IsEnabled<T>(this T animationController, bool isEnabled) where T : IAnimationController
        {
            animationController.IsEnabled = new PropertyValue<bool>(isEnabled);
            return animationController;
        }

        public static T IsEnabled<T>(this T timer, Func<bool> isEnabledFunc) where T : IAnimationController
        {
            timer.IsEnabled = new PropertyValue<bool>(isEnabledFunc);
            return timer;
        }
        public static T IsPaused<T>(this T animationController, bool isPaused) where T : IAnimationController
        {
            animationController.IsPaused = new PropertyValue<bool>(isPaused);
            return animationController;
        }
        public static T IsPaused<T>(this T animationController, Func<bool> isPausedFunc) where T : IAnimationController
        {
            animationController.IsPaused = new PropertyValue<bool>(isPausedFunc);
            return animationController;
        }

        public static T OnIsEnabledChanged<T>(this T animationController, Action<bool> isEnabledChangedAction) where T : IAnimationController
        {
            animationController.IsEnabledChangedAction = isEnabledChangedAction;
            return animationController;
        }

        public static T OnIsPausedChanged<T>(this T animationController, Action<bool> isPausedChangedAction) where T : IAnimationController
        {
            animationController.IsPausedChangedAction = isPausedChangedAction;
            return animationController;
        }
    }
}

namespace MauiReactor.Animations
{
    public interface IAnimation
    {
    }

    public abstract class Animation<T> : VisualNode<T>, IAnimation where T : MauiReactor.Animations.Internals.Animation, new()

    {
    }

    public static class AnimationExtensions
    { 
    
    }
}

namespace MauiReactor.Animations
{
    public interface IAnimationContainer
    {
    }

    public abstract class AnimationContainer<T> : VisualNode<T>, IAnimationContainer, IEnumerable where T : MauiReactor.Animations.Internals.AnimationContainer, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MauiReactor.Animations.Internals.Animation animation)
            {
                NativeControl.Children.Insert(widget.ChildIndex, animation);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MauiReactor.Animations.Internals.Animation animation)
            {
                NativeControl.Children.Remove(animation);
            }

            base.OnRemoveChild(widget, childControl);
        }

    }

    public static class AnimationContainerExtensions
    {

    }
}

namespace MauiReactor.Animations
{
    public interface IParallelAnimation : IAnimationContainer
    {
    }

    public class ParallelAnimation<T> : AnimationContainer<MauiReactor.Animations.Internals.ParallelAnimation>, IParallelAnimation

    {


    }

    public static class ParallelAnimationExtensions
    {

    }
}

namespace MauiReactor.Animations
{
    public interface ISequenceAnimation : IAnimationContainer
    {
    }

    public class SequenceAnimation<T> : AnimationContainer<MauiReactor.Animations.Internals.SequenceAnimation>, ISequenceAnimation

    {


    }

    public static class SequenceAnimationExtensions
    {

    }
}

namespace MauiReactor.Animations
{
    public interface ITweenAnimation : IAnimation
    {
        PropertyValue<double>? Duration { get; set; }
        PropertyValue<double>? InitialDelay { get; set; }
        PropertyValue<bool>? Loop { get; set; }
        PropertyValue<int?>? IterationCount { get; set; }
    }

    public abstract class TweenAnimation<T> : Animation<T>, ITweenAnimation where T : MauiReactor.Animations.Internals.TweenAnimation, new ()
    {
        PropertyValue<double>? ITweenAnimation.Duration { get; set; }
        PropertyValue<double>? ITweenAnimation.InitialDelay { get; set; }
        PropertyValue<bool>? ITweenAnimation.Loop { get; set; }
        PropertyValue<int?>? ITweenAnimation.IterationCount { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsITweenAnimation = (ITweenAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.TweenAnimation.DurationProperty, thisAsITweenAnimation.Duration);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.TweenAnimation.InitialDelayProperty, thisAsITweenAnimation.InitialDelay);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.TweenAnimation.LoopProperty, thisAsITweenAnimation.Loop);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.TweenAnimation.IterationCountProperty, thisAsITweenAnimation.IterationCount);


            base.OnUpdate();
        }

    }

    public static class TweenAnimationExtensions
    {
        public static T Duration<T>(this T timer, double duration) where T : ITweenAnimation
        {
            timer.Duration = new PropertyValue<double>(duration);
            return timer;
        }
        public static T Duration<T>(this T timer, Func<double> durationFunc) where T : ITweenAnimation
        {
            timer.Duration = new PropertyValue<double>(durationFunc);
            return timer;
        }
        public static T Duration<T>(this T timer, TimeSpan duration) where T : ITweenAnimation
        {
            timer.Duration = new PropertyValue<double>(duration.TotalMilliseconds);
            return timer;
        }

        public static T InitialDelay<T>(this T timer, double initialDelay) where T : ITweenAnimation
        {
            timer.InitialDelay = new PropertyValue<double>(initialDelay);
            return timer;
        }
        public static T InitialDelay<T>(this T timer, Func<double> initialDelayFunc) where T : ITweenAnimation
        {
            timer.InitialDelay = new PropertyValue<double>(initialDelayFunc);
            return timer;
        }
        public static T InitialDelay<T>(this T timer, TimeSpan initialDelay) where T : ITweenAnimation
        {
            timer.InitialDelay = new PropertyValue<double>(initialDelay.TotalMilliseconds);
            return timer;
        }
        public static T IterationCount<T>(this T timer, int? iterationCount) where T : ITweenAnimation
        {
            timer.IterationCount = new PropertyValue<int?>(iterationCount);
            return timer;
        }
        public static T IterationCount<T>(this T timer, Func<int?> iterationCountFunc) where T : ITweenAnimation
        {
            timer.IterationCount = new PropertyValue<int?>(iterationCountFunc);
            return timer;
        }
        public static T Loop<T>(this T timer, bool loop) where T : ITweenAnimation
        {
            timer.Loop = new PropertyValue<bool>(loop);
            return timer;
        }
        public static T Loop<T>(this T timer, Func<bool> loopFunc) where T : ITweenAnimation
        {
            timer.Loop = new PropertyValue<bool>(loopFunc);
            return timer;
        }
    }
}

namespace MauiReactor.Animations
{
    public interface IDoubleAnimation : ITweenAnimation
    {
        PropertyValue<double>? StartValue { get; set; }
        PropertyValue<double>? TargetValue { get; set; }

        Action<double>? TickAction { get; set; }
    }

    public class DoubleAnimation<T> : TweenAnimation<T>, IDoubleAnimation where T : MauiReactor.Animations.Internals.DoubleAnimation, new()
    {
        PropertyValue<double>? IDoubleAnimation.StartValue { get; set; }
        PropertyValue<double>? IDoubleAnimation.TargetValue { get; set; }
        Action<double>? IDoubleAnimation.TickAction { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIDoubleAnimation = (IDoubleAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.DoubleAnimation.StartValueProperty, thisAsIDoubleAnimation.StartValue);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.DoubleAnimation.TargetValueProperty, thisAsIDoubleAnimation.TargetValue);


            base.OnUpdate();
        }


        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIDoubleAnimation = (IDoubleAnimation)this;
            if (thisAsIDoubleAnimation.TickAction != null)
            {
                NativeControl.Tick += NativeControl_Tick;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tick(object? sender, GenericAnimationEventArgs<double> e)
        {
            var thisAsIDoubleAnimation = (IDoubleAnimation)this;
            thisAsIDoubleAnimation.TickAction?.Invoke(e.Value);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Tick -= NativeControl_Tick;
            }

            base.OnDetachNativeEvents();
        }
    }

    public class DoubleAnimation : DoubleAnimation<MauiReactor.Animations.Internals.DoubleAnimation>
    { }

    public static class DoubleAnimationExtensions
    {
        public static T StartValue<T>(this T timer, double startValue) where T : IDoubleAnimation
        {
            timer.StartValue = new PropertyValue<double>(startValue);
            return timer;
        }
        public static T StartValue<T>(this T timer, Func<double> startValueFunc) where T : IDoubleAnimation
        {
            timer.StartValue = new PropertyValue<double>(startValueFunc);
            return timer;
        }

        public static T TargetValue<T>(this T timer, double targetValue) where T : IDoubleAnimation
        {
            timer.TargetValue = new PropertyValue<double>(targetValue);
            return timer;
        }
        public static T TargetValue<T>(this T timer, Func<double> targetValueFunc) where T : IDoubleAnimation
        {
            timer.TargetValue = new PropertyValue<double>(targetValueFunc);
            return timer;
        }

        public static T OnTick<T>(this T button, Action<double> tickAction) where T : IDoubleAnimation
        {
            button.TickAction = tickAction;
            return button;
        }
    }
}

namespace MauiReactor.Animations.Internals
{
    public sealed class AnimationController : BindableObject
    {
        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(AnimationController), false,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var animationController = (AnimationController)bindableObject;
                animationController.IsPaused = false;
                if ((bool)newValue)
                {
                    animationController.Start();
                }
                else
                {
                    animationController.Stop();
                }
            }));

        public static readonly BindableProperty IsPausedProperty = BindableProperty.Create(nameof(IsPaused), typeof(bool), typeof(AnimationController), false,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var animationController = (AnimationController)bindableObject;
                if ((bool)newValue)
                {
                    animationController.Pause();
                }
                else
                {
                    animationController.Resume();
                }
            }));
        
        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public bool IsPaused
        {
            get => (bool)GetValue(IsPausedProperty);
            set => SetValue(IsPausedProperty, value);
        }

        public event EventHandler<GenericAnimationEventArgs<bool>>? IsEnabledChanged;
        public event EventHandler<GenericAnimationEventArgs<bool>>? IsPausedChanged;

        public List<Animation> Children { get; } = new();

        private double _time;
        private double _elapsedTime;

        private void Start()
        {
            _time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            _elapsedTime = 0;
            Application.Current?.Dispatcher.Dispatch(OnTick);

            IsEnabledChanged?.Invoke(this, new GenericAnimationEventArgs<bool>(true));
        }

        private void Stop()
        {
            IsEnabledChanged?.Invoke(this, new GenericAnimationEventArgs<bool>(false));
        }

        private void Pause()
        {
            IsPausedChanged?.Invoke(this, new GenericAnimationEventArgs<bool>(true));
        }

        private void Resume()
        {
            if (IsEnabled)
            {
                _time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                Application.Current?.Dispatcher.Dispatch(OnTick);
            }
            IsPausedChanged?.Invoke(this, new GenericAnimationEventArgs<bool>(false));
        }

        private void OnTick()
        {
            var newTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            _elapsedTime += newTime - _time;
            _time = newTime;

            bool allCompleted = true;
            foreach (var child in Children)
            {
                if (!child.Progress(_elapsedTime, out var _))
                {
                    allCompleted = false;
                }
            }

            if (allCompleted)
            {
                IsEnabled = false;
                IsPaused = false;
            }

            if (IsEnabled && !IsPaused)
            {
                //Application.Current?.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(1), OnTick);
                Application.Current?.Dispatcher.Dispatch(OnTick);
            }
        }
    }

    public abstract class Animation : BindableObject
    {
        public abstract bool Progress(double elapsedTime, out double remainingTime);
    }

    public abstract class AnimationContainer : Animation
    {
        public List<Animation> Children { get; } = new();
    }

    public class ParallelAnimation : AnimationContainer
    {
        public override bool Progress(double elapsedTime, out double remainingTime)
        {
            remainingTime = 0;
            bool completed = true;
            foreach (var child in Children)
            {
                if (!child.Progress(elapsedTime, out var childRemainingTime))
                {
                    completed = false;
                }
                remainingTime = Math.Min(remainingTime, childRemainingTime);
            }

            return completed;
        }
    }

    public class SequenceAnimation : AnimationContainer
    {
        public override bool Progress(double elapsedTime, out double remainingTime)
        {
            remainingTime = elapsedTime;
            foreach (var child in Children)
            {
                if (!child.Progress(elapsedTime, out var childRemainingTime))
                {
                    return false;
                }

                remainingTime -= childRemainingTime;

                System.Diagnostics.Debug.Assert(remainingTime > 0);
            }

            return true;
        }
    }

    public abstract class TweenAnimation : Animation
    {
        private double? _lastFiredTickOffset;

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(double), typeof(TweenAnimation), 600.0,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public static readonly BindableProperty InitialDelayProperty = BindableProperty.Create(nameof(InitialDelay), typeof(double), typeof(TweenAnimation), 0.0,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                
            }));

        public static readonly BindableProperty LoopProperty = BindableProperty.Create(nameof(Loop), typeof(bool), typeof(TweenAnimation), false,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public static readonly BindableProperty IterationCountProperty = BindableProperty.Create(nameof(IterationCount), typeof(int?), typeof(TweenAnimation), 1,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public double Duration
        {
            get => (double)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public double InitialDelay
        {
            get => (double)GetValue(InitialDelayProperty);
            set => SetValue(InitialDelayProperty, value);
        }
        public bool Loop
        {
            get => (bool)GetValue(LoopProperty);
            set => SetValue(LoopProperty, value);
        }
        public int? IterationCount
        {
            get => (int?)GetValue(IterationCountProperty);
            set => SetValue(IterationCountProperty, value);
        }

        public override bool Progress(double elapsedTime, out double remainingTime)
        {
            if (elapsedTime <= InitialDelay)
            {
                FireTick(0.0);
                remainingTime = 0.0;
                return false;
            }

            elapsedTime -= InitialDelay;

            if (IterationCount != null && elapsedTime >= IterationCount.Value * Duration)
            {
                FireTick(1.0);
                remainingTime = elapsedTime - IterationCount.Value * Duration;
                return true;
            }

            var iterationIndex = (int)(elapsedTime / Duration);
            elapsedTime -= iterationIndex * Duration;

            if (Loop)
            {
                if (iterationIndex % 2 == 0)
                {
                    FireTick(elapsedTime / Duration);
                }
                else
                {
                    FireTick(1.0 - elapsedTime / Duration);
                }
            }
            else
            {
                FireTick(elapsedTime / Duration);
            }

            remainingTime = 0.0;
            return false;
        }

        private void FireTick(double offset)
        {
            if (_lastFiredTickOffset != null &&
                Math.Abs(_lastFiredTickOffset.Value - offset) < 0.00001)
            {
                return;
            }

            _lastFiredTickOffset = offset;
            OnFireTick(offset);
        }

        protected abstract void OnFireTick(double offset);
    }

    public class GenericAnimationEventArgs<T> : EventArgs
    { 
        public GenericAnimationEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }

    public class DoubleAnimation : TweenAnimation
    {
        public static readonly BindableProperty StartValueProperty = BindableProperty.Create(nameof(StartValue), typeof(double), typeof(DoubleAnimation), 0.0,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public static readonly BindableProperty TargetValueProperty = BindableProperty.Create(nameof(TargetValue), typeof(double), typeof(DoubleAnimation), 1.0,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public double StartValue
        {
            get => (double)GetValue(StartValueProperty);
            set => SetValue(StartValueProperty, value);
        }

        public double TargetValue
        {
            get => (double)GetValue(TargetValueProperty);
            set => SetValue(TargetValueProperty, value);
        }


        public event EventHandler<GenericAnimationEventArgs<double>>? Tick;

        protected override void OnFireTick(double offset)
        {
            Tick?.Invoke(this, new GenericAnimationEventArgs<double>(StartValue + (TargetValue - StartValue) * offset));
        }
    }
}
