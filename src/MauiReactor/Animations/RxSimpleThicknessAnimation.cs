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
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;

        public override Thickness CurrentValue()
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

            var v = new Thickness(
                StartPoint.Value.Left + (TargetPoint.Left - StartPoint.Value.Left) * easingValue,
                StartPoint.Value.Top + (TargetPoint.Top - StartPoint.Value.Top) * easingValue,
                StartPoint.Value.Right + (TargetPoint.Right - StartPoint.Value.Right) * easingValue,
                StartPoint.Value.Bottom + (TargetPoint.Bottom - StartPoint.Value.Bottom) * easingValue
                );

            //System.Diagnostics.Debug.WriteLine($"RxSimpleThicknessAnimation(EasingValue={easingValue} CurrentValue={v} StartValue={StartPoint} TargetValue={TargetPoint} StartTime={StartTime} CurrentTime={currentTime} ElapsedTime={elapsedTime})");

            return v;
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
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousSimpleThicknessAnimation = ((RxSimpleThicknessAnimation)previousAnimation);
            
            StartPoint = previousSimpleThicknessAnimation.CurrentValue();

            if (!previousSimpleThicknessAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration * previousSimpleThicknessAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
