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
        public override bool IsCompleted() => _isCompleted;

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

            return Math.Pow(
                Math.Pow((CurrentValue().X - StartPoint.Value.X) / (TargetPoint.X - StartPoint.Value.X), 2.0) +
                Math.Pow((CurrentValue().Y - StartPoint.Value.Y) / (TargetPoint.Y - StartPoint.Value.Y), 2.0), 0.50);
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousDoubleAnimation = ((RxSimplePointAnimation)previousAnimation);
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
