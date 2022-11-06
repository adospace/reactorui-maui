namespace MauiReactor.Animations
{
    public class RxSimpleRectAnimation : RxRectAnimation
    {
        public RxSimpleRectAnimation(Rect targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public Rect TargetPoint { get; }
        public Rect? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;


        public override Rect CurrentValue()
        {
            if (StartPoint == null)
                return TargetPoint;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime - InitialDelay.GetValueOrDefault();

            if (elapsedTime < 0)
            {
                return StartPoint.Value;
            }

            var duration = Duration ?? DefaultDuration;
            
            System.Diagnostics.Debug.Assert(elapsedTime >= 0);

            if (elapsedTime >= duration)
            {
                _isCompleted = true;
                return TargetPoint;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            var currentValue = new Rect(
                StartPoint.Value.X + (TargetPoint.X - StartPoint.Value.X) * easingValue,
                StartPoint.Value.Y + (TargetPoint.Y - StartPoint.Value.Y) * easingValue,
                StartPoint.Value.Width + (TargetPoint.Width - StartPoint.Value.Width) * easingValue,
                StartPoint.Value.Height + (TargetPoint.Height - StartPoint.Value.Height) * easingValue
                );
           
           return currentValue;
        }

        private double Completion()
        {
            if (StartPoint == null)
                return 1.0;

            var currentValue = CurrentValue();

            var v = ((TargetPoint.Left != StartPoint.Value.Left) ? (currentValue.Left - StartPoint.Value.Left) / (TargetPoint.Left - StartPoint.Value.Left) : 1.0) *
                ((TargetPoint.Top != StartPoint.Value.Top) ? (currentValue.Top - StartPoint.Value.Top) / (TargetPoint.Top - StartPoint.Value.Top) : 1.0) *
                ((TargetPoint.Right != StartPoint.Value.Right) ? (currentValue.Right - StartPoint.Value.Right) / (TargetPoint.Right - StartPoint.Value.Right) : 1.0) *
                ((TargetPoint.Bottom != StartPoint.Value.Bottom) ? (currentValue.Bottom - StartPoint.Value.Bottom) / (TargetPoint.Bottom - StartPoint.Value.Bottom) : 1.0);

            return v;
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            var previousDoubleAnimation = ((RxSimpleRectAnimation)previousAnimation);

            StartPoint = previousDoubleAnimation.CurrentValue();

            if (!previousDoubleAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration - duration * previousDoubleAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
