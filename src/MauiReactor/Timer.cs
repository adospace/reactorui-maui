using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public interface ITimer
    {
        PropertyValue<bool>? IsEnabled { get; set; }

        PropertyValue<TimeSpan>? Interval { get; set; }

        Action? TickAction { get; set; }
        
        Action<System.Timers.ElapsedEventArgs>? TickActionWithArgs { get; set; }
    }

    public class Timer : VisualNode<MauiReactor.Internals.Timer>, ITimer
    {
        public Timer(int interval, Action onTick) => this.Interval(interval).OnTick(onTick);
        
        public Timer(TimeSpan interval, Action onTick) => this.Interval(interval).OnTick(onTick);

        public PropertyValue<bool>? IsEnabled { get; set; }
        public PropertyValue<TimeSpan>? Interval { get; set; }
        Action? ITimer.TickAction { get; set; }
        Action<System.Timers.ElapsedEventArgs>? ITimer.TickActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBorder = (ITimer)this;
            SetPropertyValue(NativeControl, MauiReactor.Internals.Timer.IntervalProperty, thisAsIBorder.Interval);
            SetPropertyValue(NativeControl, MauiReactor.Internals.Timer.IsEnabledProperty, thisAsIBorder.IsEnabled);


            base.OnUpdate();
        }

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIButton = (ITimer)this;
            if (thisAsIButton.TickAction != null || thisAsIButton.TickActionWithArgs != null)
            {
                NativeControl.Tick += NativeControl_Tick;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tick(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var thisAsIButton = (ITimer)this;
            thisAsIButton.TickAction?.Invoke();
            thisAsIButton.TickActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Tick -= NativeControl_Tick;
            }

            base.OnDetachNativeEvents();
        }

        protected override void OnUnmount()
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Dispose();

            base.OnUnmount();
        }
    }

    public static class TimerExtensions
    {
        public static T IsEnabled<T>(this T timer, bool isEnabled) where T : ITimer
        {
            timer.IsEnabled = new PropertyValue<bool>(isEnabled);
            return timer;
        }
        public static T IsEnabled<T>(this T timer, Func<bool> isEnabledFunc) where T : ITimer
        {
            timer.IsEnabled = new PropertyValue<bool>(isEnabledFunc);
            return timer;
        }

        public static T Interval<T>(this T timer, int interval) where T : ITimer
        {
            timer.Interval = new PropertyValue<TimeSpan>(TimeSpan.FromMilliseconds(interval));
            return timer;
        }

        public static T Interval<T>(this T timer, TimeSpan interval) where T : ITimer
        {
            timer.Interval = new PropertyValue<TimeSpan>(interval);
            return timer;
        }

        public static T OnTick<T>(this T button, Action tickAction) where T : ITimer
        {
            button.TickAction = tickAction;
            return button;
        }

        public static T OnTick<T>(this T button, Action<System.Timers.ElapsedEventArgs> tickActionWithArgs) where T : ITimer
        {
            button.TickActionWithArgs = tickActionWithArgs;
            return button;
        }
    }

}

namespace MauiReactor.Internals
{
    public sealed class Timer : BindableObject, IDisposable
    {
        private System.Timers.Timer? _internalTimer;

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(Timer), false,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var timer = (Timer)bindableObject;
                timer.SetupTimer();
            }));

        public static readonly BindableProperty IntervalProperty = BindableProperty.Create(nameof(Interval), typeof(TimeSpan), typeof(Timer), TimeSpan.FromSeconds(1));

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);        
        }

        public TimeSpan Interval
        {
            get => (TimeSpan)GetValue(IntervalProperty);
            set => SetValue(IntervalProperty, value);
        }

        public event EventHandler<System.Timers.ElapsedEventArgs>? Tick;

        private void SetupTimer()
        {
            if (IsEnabled)
            {
                StartTimer();
            }
            else
            {
                StopTimer();
            }
        }

        public void StopTimer()
        {
            if (_internalTimer != null)
            {
                _internalTimer.Enabled = false;
            }
        }

        public void StartTimer()
        {
            if (_internalTimer == null)
            {
                _internalTimer = new System.Timers.Timer(300)
                {
                    AutoReset = true
                };
                _internalTimer.Elapsed += OnTick;
            }

            _internalTimer.Enabled = true;
        }

        public void Dispose()
        {
            StopTimer();
            _internalTimer?.Dispose();
        }

        private void OnTick(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Dispatch(() => Tick?.Invoke(this, e));
        }
    }
}

