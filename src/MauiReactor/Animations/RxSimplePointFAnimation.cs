namespace MauiReactor.Animations
{
    public class RxSimplePointFAnimation : RxPointFAnimation
    {
        public RxSimplePointFAnimation(PointF targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public PointF TargetPoint { get; }
        public PointF? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;

        public override PointF CurrentValue()
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

            return new PointF(
                (float)(StartPoint.Value.X + (TargetPoint.X - StartPoint.Value.X) * easingValue),
                (float)(StartPoint.Value.Y + (TargetPoint.Y - StartPoint.Value.Y) * easingValue)
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
            var previousDoubleAnimation = ((RxSimplePointFAnimation)previousAnimation);
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
