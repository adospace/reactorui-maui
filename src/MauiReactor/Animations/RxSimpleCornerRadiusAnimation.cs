namespace MauiReactor.Animations
{
    public class RxSimpleCornerRadiusAnimation : RxCornerRadiusAnimation
    {
        public RxSimpleCornerRadiusAnimation(CornerRadius targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public CornerRadius TargetPoint { get; }
        public CornerRadius? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;

        public override CornerRadius CurrentValue()
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

            var v = new CornerRadius(
                (double)(StartPoint.Value.TopLeft + (TargetPoint.TopLeft - StartPoint.Value.TopLeft) * easingValue),
                (double)(StartPoint.Value.TopRight + (TargetPoint.TopRight - StartPoint.Value.TopRight) * easingValue),
                (double)(StartPoint.Value.BottomRight + (TargetPoint.BottomRight - StartPoint.Value.BottomRight) * easingValue),
                (double)(StartPoint.Value.BottomLeft + (TargetPoint.BottomLeft - StartPoint.Value.BottomLeft) * easingValue)
                );

            //System.Diagnostics.Debug.WriteLine($"RxSimpleThicknessAnimation(EasingValue={easingValue} CurrentValue={v} StartValue={StartPoint} TargetValue={TargetPoint} StartTime={StartTime} CurrentTime={currentTime} ElapsedTime={elapsedTime})");

            return v;
        }

        private double Completion()
        {
            if (StartPoint == null)
                return 1.0;

            var currentValue = CurrentValue();

            var v = ((TargetPoint.TopLeft != StartPoint.Value.TopLeft) ? (currentValue.TopLeft - StartPoint.Value.TopLeft) / (TargetPoint.TopLeft - StartPoint.Value.TopLeft) : 1.0) *
                ((TargetPoint.TopRight != StartPoint.Value.TopRight) ? (currentValue.TopRight - StartPoint.Value.TopRight) / (TargetPoint.TopRight - StartPoint.Value.TopRight) : 1.0) *
                ((TargetPoint.BottomRight != StartPoint.Value.BottomRight) ? (currentValue.BottomRight - StartPoint.Value.BottomRight) / (TargetPoint.BottomRight - StartPoint.Value.BottomRight) : 1.0) *
                ((TargetPoint.BottomLeft != StartPoint.Value.BottomLeft) ? (currentValue.BottomLeft - StartPoint.Value.BottomLeft) / (TargetPoint.BottomLeft - StartPoint.Value.BottomLeft) : 1.0);

            return v;
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            var previousDoubleAnimation = ((RxSimpleCornerRadiusAnimation)previousAnimation);

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
