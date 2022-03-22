namespace MauiReactor.Animations
{
    public class RxSimpleThicknessAnimation : RxThicknessAnimation
    {
        public RxSimpleThicknessAnimation(Thickness targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public Thickness TargetPoint { get; }
        public Thickness? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted;

        public override Thickness CurrentValue()
        {
            if (StartPoint == null)
                return TargetPoint;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime;
            var duration = Duration ?? DefaultDuration;

            if (elapsedTime >= duration)
            {
                _isCompleted = true;
                return TargetPoint;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            return new Thickness(
                StartPoint.Value.Left + (TargetPoint.Left - StartPoint.Value.Left) * easingValue,
                StartPoint.Value.Top + (TargetPoint.Top - StartPoint.Value.Top) * easingValue,
                StartPoint.Value.Right + (TargetPoint.Right - StartPoint.Value.Right) * easingValue,
                StartPoint.Value.Bottom + (TargetPoint.Bottom - StartPoint.Value.Bottom) * easingValue
                );
        }

        private double Completion()
        {
            if (StartPoint == null)
                return 1.0;
            
            var currentValue = CurrentValue();

            return Math.Pow(
                Math.Pow((currentValue.Left - StartPoint.Value.Left) / (TargetPoint.Left - StartPoint.Value.Left), 2.0) +
                Math.Pow((currentValue.Top - StartPoint.Value.Top) / (TargetPoint.Top - StartPoint.Value.Top), 2.0) +
                Math.Pow((currentValue.Right - StartPoint.Value.Right) / (TargetPoint.Right - StartPoint.Value.Right), 2.0) +
                Math.Pow((currentValue.Bottom - StartPoint.Value.Bottom) / (TargetPoint.Bottom - StartPoint.Value.Bottom), 2.0),
                0.25);
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousDoubleAnimation = ((RxSimpleThicknessAnimation)previousAnimation);
            StartPoint = previousDoubleAnimation.TargetPoint;

            if (!previousDoubleAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration - duration * previousDoubleAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
