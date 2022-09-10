using MauiReactor.Animations.Internals;
using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public AnimationController()
        {

        }

        public AnimationController(Action<MauiReactor.Animations.Internals.AnimationController?> componentRefAction)
            : base(componentRefAction)
        {

        }

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
        PropertyValue<double>? InitialDelay { get; set; }
        PropertyValue<bool>? Loop { get; set; }
        PropertyValue<int?>? IterationCount { get; set; }
    }

    public abstract class Animation<T> : VisualNode<T>, IAnimation where T : MauiReactor.Animations.Internals.Animation, new()

    {
        public Animation()
        {

        }

        public Animation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<double>? IAnimation.InitialDelay { get; set; }
        PropertyValue<bool>? IAnimation.Loop { get; set; }
        PropertyValue<int?>? IAnimation.IterationCount { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIAnimation = (IAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.Animation.InitialDelayProperty, thisAsIAnimation.InitialDelay);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.Animation.LoopProperty, thisAsIAnimation.Loop);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.Animation.IterationCountProperty, thisAsIAnimation.IterationCount);


            base.OnUpdate();
        }
    }

    public static class AnimationExtensions
    {
        public static T InitialDelay<T>(this T timer, double initialDelay) where T : IAnimation
        {
            timer.InitialDelay = new PropertyValue<double>(initialDelay);
            return timer;
        }
        public static T InitialDelay<T>(this T timer, Func<double> initialDelayFunc) where T : IAnimation
        {
            timer.InitialDelay = new PropertyValue<double>(initialDelayFunc);
            return timer;
        }
        public static T InitialDelay<T>(this T timer, TimeSpan initialDelay) where T : IAnimation
        {
            timer.InitialDelay = new PropertyValue<double>(initialDelay.TotalMilliseconds);
            return timer;
        }
        public static T IterationCount<T>(this T timer, int? iterationCount) where T : IAnimation
        {
            timer.IterationCount = new PropertyValue<int?>(iterationCount);
            return timer;
        }
        public static T IterationCount<T>(this T timer, Func<int?> iterationCountFunc) where T : IAnimation
        {
            timer.IterationCount = new PropertyValue<int?>(iterationCountFunc);
            return timer;
        }
        public static T Loop<T>(this T timer, bool loop) where T : IAnimation
        {
            timer.Loop = new PropertyValue<bool>(loop);
            return timer;
        }
        public static T Loop<T>(this T timer, Func<bool> loopFunc) where T : IAnimation
        {
            timer.Loop = new PropertyValue<bool>(loopFunc);
            return timer;
        }
    }
}

namespace MauiReactor.Animations
{
    public interface IAnimationContainer
    {
    }

    public abstract class AnimationContainer<T> : Animation<T>, IAnimationContainer, IEnumerable where T : MauiReactor.Animations.Internals.AnimationContainer, new()
    {
        protected readonly List<VisualNode> _internalChildren = new();

        public AnimationContainer()
        {

        }

        public AnimationContainer(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }


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
                NativeControl.InsertChild(widget.ChildIndex, animation);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is MauiReactor.Animations.Internals.Animation animation)
            {
                NativeControl.RemoveChild(animation);
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

    public class ParallelAnimation<T> : AnimationContainer<T>, IParallelAnimation where T : MauiReactor.Animations.Internals.ParallelAnimation, new ()
    {
        public ParallelAnimation()
        {

        }

        public ParallelAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }


    }

    public class ParallelAnimation : AnimationContainer<MauiReactor.Animations.Internals.ParallelAnimation>

    {
        public ParallelAnimation()
        {

        }

        public ParallelAnimation(Action<MauiReactor.Animations.Internals.ParallelAnimation?> componentRefAction)
            : base(componentRefAction)
        {

        }
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

    public class SequenceAnimation<T> : AnimationContainer<T>, ISequenceAnimation where T : MauiReactor.Animations.Internals.SequenceAnimation, new()

    {
        public SequenceAnimation()
        {

        }

        public SequenceAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

    }

    public class SequenceAnimation : AnimationContainer<MauiReactor.Animations.Internals.SequenceAnimation>

    {
        public SequenceAnimation()
        {

        }

        public SequenceAnimation(Action<MauiReactor.Animations.Internals.SequenceAnimation?> componentRefAction)
            : base(componentRefAction)
        {

        }

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
        PropertyValue<Easing>? Easing { get; set; }
    }

    public abstract class TweenAnimation<T> : Animation<T>, ITweenAnimation where T : MauiReactor.Animations.Internals.TweenAnimation, new ()
    {
        public TweenAnimation()
        {

        }

        public TweenAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<double>? ITweenAnimation.Duration { get; set; }
        PropertyValue<Easing>? ITweenAnimation.Easing { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsITweenAnimation = (ITweenAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.TweenAnimation.DurationProperty, thisAsITweenAnimation.Duration);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.TweenAnimation.EasingProperty, thisAsITweenAnimation.Easing);


            base.OnUpdate();
        }

    }

    public static class TweenAnimationExtensions
    {
        public static T Duration<T>(this T animation, double duration) where T : ITweenAnimation
        {
            animation.Duration = new PropertyValue<double>(duration);
            return animation;
        }
        public static T Duration<T>(this T animation, Func<double> durationFunc) where T : ITweenAnimation
        {
            animation.Duration = new PropertyValue<double>(durationFunc);
            return animation;
        }
        public static T Duration<T>(this T animation, TimeSpan duration) where T : ITweenAnimation
        {
            animation.Duration = new PropertyValue<double>(duration.TotalMilliseconds);
            return animation;
        }
        public static T Easing<T>(this T animation, Easing easing) where T : ITweenAnimation
        {
            animation.Easing = new PropertyValue<Easing>(easing);
            return animation;
        }
        public static T Easing<T>(this T animation, Func<Easing> easingFunc) where T : ITweenAnimation
        {
            animation.Easing = new PropertyValue<Easing>(easingFunc);
            return animation;
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
        public DoubleAnimation()
        {

        }

        public DoubleAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }
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
    {
        public DoubleAnimation()
        {

        }

        public DoubleAnimation(Action<MauiReactor.Animations.Internals.DoubleAnimation?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

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

namespace MauiReactor.Animations
{
    public interface IPathAnimation : ITweenAnimation
    {
        PropertyValue<Point>? StartPoint { get; set; }
        PropertyValue<Point>? EndPoint { get; set; }
    }

    public abstract class PathAnimation<T> : TweenAnimation<T>, IPathAnimation where T : MauiReactor.Animations.Internals.PathAnimation, new()
    {
        public PathAnimation()
        {

        }

        public PathAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Point>? IPathAnimation.StartPoint { get; set; }
        PropertyValue<Point>? IPathAnimation.EndPoint { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPathAnimation = (IPathAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.PathAnimation.StartPointProperty, thisAsIPathAnimation.StartPoint);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.PathAnimation.EndPointProperty, thisAsIPathAnimation.EndPoint);


            base.OnUpdate();
        }

    }

    public static class PathAnimationExtensions
    {
        public static T StartPoint<T>(this T animation, Point pt) where T : IPathAnimation
        {
            animation.StartPoint = new PropertyValue<Point>(pt);
            return animation;
        }
        public static T StartPoint<T>(this T animation, Func<Point> ptFunc) where T : IPathAnimation
        {
            animation.StartPoint = new PropertyValue<Point>(ptFunc);
            return animation;
        }
        public static T EndPoint<T>(this T animation, Point pt) where T : IPathAnimation
        {
            animation.EndPoint = new PropertyValue<Point>(pt);
            return animation;
        }
        public static T EndPoint<T>(this T animation, Func<Point> ptFunc) where T : IPathAnimation
        {
            animation.EndPoint = new PropertyValue<Point>(ptFunc);
            return animation;
        }
    }
}

namespace MauiReactor.Animations
{
    public interface IQuadraticBezierPathAnimation : IPathAnimation
    {
        PropertyValue<Point>? ControlPoint { get; set; }

        Action<Point>? TickAction { get; set; }
    }

    public class QuadraticBezierPathAnimation<T> : PathAnimation<T>, IQuadraticBezierPathAnimation where T : MauiReactor.Animations.Internals.QuadraticBezierPathAnimation, new()
    {
        public QuadraticBezierPathAnimation()
        {

        }

        public QuadraticBezierPathAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }
        PropertyValue<Point>? IQuadraticBezierPathAnimation.ControlPoint { get; set; }

        Action<Point>? IQuadraticBezierPathAnimation.TickAction { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIQuadraticBezierPathAnimation = (IQuadraticBezierPathAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.QuadraticBezierPathAnimation.ControlPointProperty, thisAsIQuadraticBezierPathAnimation.ControlPoint);


            base.OnUpdate();
        }


        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIQuadraticBezierPathAnimation = (IQuadraticBezierPathAnimation)this;
            if (thisAsIQuadraticBezierPathAnimation.TickAction != null)
            {
                NativeControl.Tick += NativeControl_Tick;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tick(object? sender, GenericAnimationEventArgs<Point> e)
        {
            var thisAsIQuadraticBezierPathAnimation = (IQuadraticBezierPathAnimation)this;
            thisAsIQuadraticBezierPathAnimation.TickAction?.Invoke(e.Value);
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

    public class QuadraticBezierPathAnimation : QuadraticBezierPathAnimation<MauiReactor.Animations.Internals.QuadraticBezierPathAnimation>
    {
        public QuadraticBezierPathAnimation()
        {

        }

        public QuadraticBezierPathAnimation(Action<MauiReactor.Animations.Internals.QuadraticBezierPathAnimation?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class QuadraticBezierPathAnimationExtensions
    {
        public static T ControlPoint<T>(this T animation, Point point) where T : IQuadraticBezierPathAnimation
        {
            animation.ControlPoint = new PropertyValue<Point>(point);
            return animation;
        }
        public static T ControlPoint<T>(this T timer, Func<Point> pointFunc) where T : IQuadraticBezierPathAnimation
        {
            timer.ControlPoint = new PropertyValue<Point>(pointFunc);
            return timer;
        }
    }
}

namespace MauiReactor.Animations
{
    public interface ICubicBezierPathAnimation : IPathAnimation
    {
        PropertyValue<Point>? ControlPoint1 { get; set; }
        PropertyValue<Point>? ControlPoint2 { get; set; }

        Action<Point>? TickAction { get; set; }
    }

    public class CubicBezierPathAnimation<T> : PathAnimation<T>, ICubicBezierPathAnimation where T : MauiReactor.Animations.Internals.CubicBezierPathAnimation, new()
    {
        public CubicBezierPathAnimation()
        {

        }

        public CubicBezierPathAnimation(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Point>? ICubicBezierPathAnimation.ControlPoint1 { get; set; }
        PropertyValue<Point>? ICubicBezierPathAnimation.ControlPoint2 { get; set; }

        Action<Point>? ICubicBezierPathAnimation.TickAction { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsICubicBezierPathAnimation = (ICubicBezierPathAnimation)this;
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.CubicBezierPathAnimation.ControlPoint1Property, thisAsICubicBezierPathAnimation.ControlPoint1);
            SetPropertyValue(NativeControl, MauiReactor.Animations.Internals.CubicBezierPathAnimation.ControlPoint2Property, thisAsICubicBezierPathAnimation.ControlPoint2);


            base.OnUpdate();
        }


        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIQuadraticBezierPathAnimation = (IQuadraticBezierPathAnimation)this;
            if (thisAsIQuadraticBezierPathAnimation.TickAction != null)
            {
                NativeControl.Tick += NativeControl_Tick;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tick(object? sender, GenericAnimationEventArgs<Point> e)
        {
            var thisAsIQuadraticBezierPathAnimation = (IQuadraticBezierPathAnimation)this;
            thisAsIQuadraticBezierPathAnimation.TickAction?.Invoke(e.Value);
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

    public class CubicBezierPathAnimation : CubicBezierPathAnimation<MauiReactor.Animations.Internals.CubicBezierPathAnimation>
    {
        public CubicBezierPathAnimation()
        {

        }

        public CubicBezierPathAnimation(Action<MauiReactor.Animations.Internals.CubicBezierPathAnimation?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static class CubicBezierPathAnimationExtensions
    {
        public static T ControlPoint1<T>(this T animation, Point point) where T : ICubicBezierPathAnimation
        {
            animation.ControlPoint1 = new PropertyValue<Point>(point);
            return animation;
        }
        public static T ControlPoint1<T>(this T animation, Func<Point> pointFunc) where T : ICubicBezierPathAnimation
        {
            animation.ControlPoint1 = new PropertyValue<Point>(pointFunc);
            return animation;
        }
        public static T ControlPoint2<T>(this T animation, Point point) where T : ICubicBezierPathAnimation
        {
            animation.ControlPoint2 = new PropertyValue<Point>(point);
            return animation;
        }
        public static T ControlPoint2<T>(this T animation, Func<Point> pointFunc) where T : ICubicBezierPathAnimation
        {
            animation.ControlPoint2 = new PropertyValue<Point>(pointFunc);
            return animation;
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
                if (!child.Progress(_elapsedTime))
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
                if (Microsoft.Maui.Devices.DeviceInfo.Idiom == DeviceIdiom.Desktop)
                {
                    Application.Current?.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(1), OnTick);
                }
                else
                {
                    Application.Current?.Dispatcher.Dispatch(OnTick);
                }
            }
        }
    }

    public abstract class Animation : BindableObject
    {
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

        public bool Progress(double elapsedTime)
        { 
            if (elapsedTime <= InitialDelay)
            {
                OnTick(0.0);
                return false;
            }

            elapsedTime -= InitialDelay;

            var duration = GetDuration();

            if (IterationCount != null && elapsedTime >= IterationCount.Value * duration)
            {
                OnTick(1.0);
                return true;
            }

            var iterationIndex = (int)(elapsedTime / duration);
            elapsedTime -= iterationIndex * duration;

            if (Loop)
            {
                if (iterationIndex % 2 == 0)
                {
                    OnTick(elapsedTime / duration);
                }
                else
                {
                    OnTick(1.0 - elapsedTime / duration);
                }
            }
            else
            {
                OnTick(elapsedTime / duration);
            }

            return false;
        }

        internal void Tick(double offset)
            => OnTick(offset);

        protected abstract void OnTick(double offset);

        public abstract double GetDuration();
    }

    public abstract class AnimationContainer : Animation
    {
        private readonly List<Animation> _children = new();
        public IReadOnlyList<Animation> Children => _children;

        public void InsertChild(int index, Animation animation)
        {
            _children.Insert(index, animation);
            OnChildInsert(index, animation);
        }

        public void RemoveChild(Animation animation)
        {
            _children.Remove(animation);
            OnChildRemoved(animation);
        }

        protected virtual void OnChildRemoved(Animation animation)
        {
        }

        protected virtual void OnChildInsert(int index, Animation animation)
        {
        }
    }

    public class ParallelAnimation : AnimationContainer
    {
        private double? _duration;
        public override double GetDuration()
        {
            return ((double?)(_duration ??= Children.Max(_ => _.GetDuration()))).Value;
        }

        protected override void OnChildInsert(int index, Animation animation)
        {
            _duration = null;
            base.OnChildInsert(index, animation);
        }

        protected override void OnChildRemoved(Animation animation)
        {
            _duration = null;
            base.OnChildRemoved(animation);
        }

        protected override void OnTick(double offset)
        {
            foreach (var childAnimation in Children)
            {
                childAnimation.Tick(offset);
            }
        }
    }

    public class SequenceAnimation : AnimationContainer
    {
        private double? _duration;
        public override double GetDuration()
        {
            return ((double?)(_duration ??= Children.Sum(_ => _.GetDuration()))).Value;
        }

        protected override void OnChildInsert(int index, Animation animation)
        {
            _duration = null;
            base.OnChildInsert(index, animation);
        }

        protected override void OnChildRemoved(Animation animation)
        {
            _duration = null;
            base.OnChildRemoved(animation);
        }

        protected override void OnTick(double offset)
        {
            var duration = GetDuration();
            foreach (var childAnimation in Children)
            {
                var childOffset = childAnimation.GetDuration() / duration;
                if (offset > childOffset)
                {
                    offset -= childOffset;
                }
                else
                {
                    childAnimation.Tick(offset / childOffset);
                    return;
                }
            }
        }

        //public override bool Progress(double elapsedTime, out double remainingTime)
        //{
        //    if (elapsedTime <= InitialDelay)
        //    {
        //        FireTick(0.0);
        //        remainingTime = 0.0;
        //        return false;
        //    }

        //    elapsedTime -= InitialDelay;

        //    var duration = GetDuration();

        //    if (IterationCount != null && duration != null && elapsedTime >= IterationCount.Value * duration.Value)
        //    {
        //        FireTick(1.0);
        //        remainingTime = elapsedTime - IterationCount.Value * duration.Value;
        //        return true;
        //    }

        //    var iterationIndex = (int)(elapsedTime / Duration);
        //    elapsedTime -= iterationIndex * Duration;

        //    if (Loop)
        //    {
        //        if (iterationIndex % 2 == 0)
        //        {
        //            FireTick(elapsedTime / Duration);
        //        }
        //        else
        //        {
        //            FireTick(1.0 - elapsedTime / Duration);
        //        }
        //    }
        //    else
        //    {
        //        FireTick(elapsedTime / Duration);
        //    }

        //    remainingTime = 0.0;
        //    return false;
        //}

        //private bool OnProgressCore(double elapsedTime, out double remainingTime)
        //{
        //    remainingTime = elapsedTime;

        //    foreach (var child in Children)
        //    {
        //        if (!child.Progress(elapsedTime, out var childRemainingTime))
        //        {
        //            return false;
        //        }

        //        remainingTime -= childRemainingTime;
        //        elapsedTime = childRemainingTime;

        //        System.Diagnostics.Debug.Assert(remainingTime > 0);
        //    }

        //    return true;
        //}
    }

    public abstract class TweenAnimation : Animation
    {
        private double? _lastFiredTickOffset;

        public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(double), typeof(TweenAnimation), 600.0,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public double Duration
        {
            get => (double)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly BindableProperty EasingProperty = BindableProperty.Create(nameof(Easing), typeof(Easing), typeof(TweenAnimation), null,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        public override double GetDuration()
        {
            return Duration;
        }

        protected override void OnTick(double offset)
        {
            if (_lastFiredTickOffset != null &&
                Math.Abs(_lastFiredTickOffset.Value - offset) < 0.00001)
            {
                return;
            }

            _lastFiredTickOffset = offset;
            if (Easing != null)
            {
                OnFireTick(Easing.Ease(offset));
            }
            else
            {
                OnFireTick(offset);
            }            
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

    public abstract class PathAnimation : TweenAnimation
    {
        public static readonly BindableProperty StartPointProperty = BindableProperty.Create(nameof(StartPoint), typeof(Point), typeof(PathAnimation), new Point(0.0, 0.0),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public static readonly BindableProperty EndPointProperty = BindableProperty.Create(nameof(EndPoint), typeof(Point), typeof(PathAnimation), new Point(1.0, 1.0),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public Point StartPoint
        {
            get => (Point)GetValue(StartPointProperty);
            set => SetValue(StartPointProperty, value);
        }

        public Point EndPoint
        {
            get => (Point)GetValue(EndPointProperty);
            set => SetValue(EndPointProperty, value);
        }


        public event EventHandler<GenericAnimationEventArgs<Point>>? Tick;

        protected void FireTick(Point point)
            => Tick?.Invoke(this, new GenericAnimationEventArgs<Point>(point));
    }

    public class QuadraticBezierPathAnimation : PathAnimation
    {
        public static readonly BindableProperty ControlPointProperty = BindableProperty.Create(nameof(ControlPoint), typeof(Point), typeof(QuadraticBezierPathAnimation), new Point(1.0, 0.0),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public Point ControlPoint
        {
            get => (Point)GetValue(ControlPointProperty);
            set => SetValue(ControlPointProperty, value);
        }

        protected override void OnFireTick(double offset)
        {
            //https://stackoverflow.com/questions/17083580/i-want-to-do-animation-of-an-object-along-a-particular-path
            var startPoint = StartPoint;
            var endPoint = EndPoint;
            var ctrlPoint = ControlPoint;

            var t1 = offset * offset;
            var t2 = (1 - offset) * (1 - offset);
            var x = t2 * startPoint.X + 2 * (1 - offset) * offset * ctrlPoint.X + t1 * endPoint.X;
            var y = t2 * startPoint.Y + 2 * (1 - offset) * offset * ctrlPoint.Y + t1 * endPoint.Y;

            FireTick(new Point(x, y));
        }
    }

    public class CubicBezierPathAnimation : PathAnimation
    {
        public static readonly BindableProperty ControlPoint1Property = BindableProperty.Create(nameof(ControlPoint1), typeof(Point), typeof(CubicBezierPathAnimation), new Point(1.0, 0.0),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public static readonly BindableProperty ControlPoint2Property = BindableProperty.Create(nameof(ControlPoint2), typeof(Point), typeof(CubicBezierPathAnimation), new Point(0.0, 1.0),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {

            }));

        public Point ControlPoint1
        {
            get => (Point)GetValue(ControlPoint1Property);
            set => SetValue(ControlPoint1Property, value);
        }

        public Point ControlPoint2
        {
            get => (Point)GetValue(ControlPoint2Property);
            set => SetValue(ControlPoint2Property, value);
        }

        private static double CubicN(double offset, double a, double b, double c, double d)
        {
            var t2 = offset * offset;
            var t3 = t2 * offset;
            return a + (-a * 3 + offset * (3 * a - a * offset)) * offset
            + (3 * b + offset * (-6 * b + b * 3 * offset)) * offset
            + (c * 3 - c * 3 * offset) * t2
            + d * t3;
        }

        protected override void OnFireTick(double offset)
        {
            //https://stackoverflow.com/questions/17083580/i-want-to-do-animation-of-an-object-along-a-particular-path
            var startPt = StartPoint;
            var endPt = EndPoint;
            var controlPt1 = ControlPoint1;
            var controlPt2 = ControlPoint2;

            var x = CubicN(offset, startPt.X, controlPt1.X, controlPt2.X, endPt.X);
            var y = CubicN(offset, startPt.Y, controlPt1.Y, controlPt2.Y, endPt.Y);

            FireTick(new Point(x, y));
        }
    }

}
