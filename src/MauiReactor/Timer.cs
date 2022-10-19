using MauiReactor.Internals;

namespace MauiReactor
{
    public interface ITimer
    {
        PropertyValue<bool>? IsEnabled { get; set; }

        PropertyValue<TimeSpan>? Interval { get; set; }

        PropertyValue<TimeSpan>? DueTime { get; set; }

        Action? TickAction { get; set; }
        
        Action<EventArgs>? TickActionWithArgs { get; set; }
    }

    public class Timer : VisualNode<MauiReactor.Internals.Timer>, ITimer
    {
        public Timer(int interval, Action onTick) => this.Interval(interval).OnTick(onTick);

        public Timer(TimeSpan interval, Action onTick) => this.Interval(interval).OnTick(onTick);

        public Timer(int interval, int dueTime, Action onTick) => this.Interval(interval).DueTime(dueTime).OnTick(onTick);

        public Timer(TimeSpan interval, TimeSpan dueTime, Action onTick) => this.Interval(interval).DueTime(dueTime).OnTick(onTick);

        PropertyValue<bool>? ITimer.IsEnabled { get; set; }
        PropertyValue<TimeSpan>? ITimer.Interval { get; set; }
        PropertyValue<TimeSpan>? ITimer.DueTime { get; set; }
        Action? ITimer.TickAction { get; set; }
        Action<EventArgs>? ITimer.TickActionWithArgs { get; set; }

        protected override bool SupportChildIndexing => false;
        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsITimer = (ITimer)this;
            SetPropertyValue(NativeControl, MauiReactor.Internals.Timer.IntervalProperty, thisAsITimer.Interval);
            SetPropertyValue(NativeControl, MauiReactor.Internals.Timer.DueTimeProperty, thisAsITimer.DueTime);
            SetPropertyValue(NativeControl, MauiReactor.Internals.Timer.IsEnabledProperty, thisAsITimer.IsEnabled);


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

        private void NativeControl_Tick(object? sender, EventArgs e)
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

        public static T Interval<T>(this T timer, Func<int> intervalFunc) where T : ITimer
        {
            timer.Interval = new PropertyValue<TimeSpan>(() => TimeSpan.FromMilliseconds(intervalFunc()));
            return timer;
        }

        public static T Interval<T>(this T timer, Func<TimeSpan> intervalFunc) where T : ITimer
        {
            timer.Interval = new PropertyValue<TimeSpan>(intervalFunc);
            return timer;
        }

        public static T DueTime<T>(this T timer, int interval) where T : ITimer
        {
            timer.DueTime = new PropertyValue<TimeSpan>(TimeSpan.FromMilliseconds(interval));
            return timer;
        }

        public static T DueTime<T>(this T timer, TimeSpan interval) where T : ITimer
        {
            timer.DueTime = new PropertyValue<TimeSpan>(interval);
            return timer;
        }

        public static T DueTime<T>(this T timer, Func<int> intervalFunc) where T : ITimer
        {
            timer.DueTime = new PropertyValue<TimeSpan>(() => TimeSpan.FromMilliseconds(intervalFunc()));
            return timer;
        }

        public static T DueTime<T>(this T timer, Func<TimeSpan> intervalFunc) where T : ITimer
        {
            timer.DueTime = new PropertyValue<TimeSpan>(intervalFunc);
            return timer;
        }

        public static T OnTick<T>(this T button, Action tickAction) where T : ITimer
        {
            button.TickAction = tickAction;
            return button;
        }

        public static T OnTick<T>(this T button, Action<EventArgs> tickActionWithArgs) where T : ITimer
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
        private System.Threading.Timer? _internalTimer;

        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(Timer), false,
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var timer = (Timer)bindableObject;
                timer.SetupTimer();
            }));

        public static readonly BindableProperty IntervalProperty = BindableProperty.Create(nameof(Interval), typeof(TimeSpan), typeof(Timer), TimeSpan.FromSeconds(1),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var timer = (Timer)bindableObject;
                timer.SetupTimer();
            }));

        public static readonly BindableProperty DueTimeProperty = BindableProperty.Create(nameof(DueTime), typeof(TimeSpan), typeof(Timer), TimeSpan.FromSeconds(1),
            propertyChanged: new BindableProperty.BindingPropertyChangedDelegate((bindableObject, oldValue, newValue) =>
            {
                var timer = (Timer)bindableObject;
                timer.SetupTimer();
            }));

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

        public TimeSpan DueTime
        {
            get => (TimeSpan)GetValue(DueTimeProperty);
            set => SetValue(DueTimeProperty, value);
        }

        public event EventHandler<EventArgs>? Tick;

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
                _internalTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _internalTimer.Dispose();
                _internalTimer = null;
            }
        }

        public void StartTimer()
        {
            _internalTimer ??= new System.Threading.Timer(OnTick, null, DueTime, Interval);
        }

        public void Dispose()
        {
            StopTimer();
        }

        private void OnTick(object? state)
        {
            if (IsEnabled)
            {
                Dispatcher.Dispatch(() => Tick?.Invoke(this, EventArgs.Empty));
            }
        }
    }
}

