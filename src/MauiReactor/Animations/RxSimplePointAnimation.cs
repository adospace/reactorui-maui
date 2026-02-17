namespace MauiReactor.Animations
{
    public class RxSimplePointAnimation : RxPointAnimation
    {
        public RxSimplePointAnimation(Point targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public Point TargetPoint { get; }
        public Point? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;
        internal override object GetCurrentValue() => CurrentValue();

        public override Point CurrentValue()
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

            if (elapsedTime >= duration)
            {
                _isCompleted = true;
                return TargetPoint;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            return new Point(
                StartPoint.Value.X + (TargetPoint.X - StartPoint.Value.X) * easingValue,
                StartPoint.Value.Y + (TargetPoint.Y - StartPoint.Value.Y) * easingValue
                );
        }

        private double Completion()
        {
            if (StartPoint == null)
                return 1.0;

            var currentValue = CurrentValue();

            var v = ((TargetPoint.X != StartPoint.Value.X) ? (currentValue.X - StartPoint.Value.X) / (TargetPoint.X - StartPoint.Value.X) : 1.0) *
                ((TargetPoint.Y != StartPoint.Value.Y) ? (currentValue.Y - StartPoint.Value.Y) / (TargetPoint.Y - StartPoint.Value.Y) : 1.0);

            return v;

        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            var previousSimplePointAnimation = ((RxSimplePointAnimation)previousAnimation);
            StartPoint = previousSimplePointAnimation.CurrentValue();

            if (!previousSimplePointAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration * previousSimplePointAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }

}
