namespace MauiReactor.Animations
{
    public class RxSimpleCornerRadiusFAnimation : RxCornerRadiusFAnimation
    {
        public RxSimpleCornerRadiusFAnimation(CornerRadiusF targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public CornerRadiusF TargetPoint { get; }
        public CornerRadiusF? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;

        public override CornerRadiusF CurrentValue()
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

            var v = new CornerRadiusF(
                (float)(StartPoint.Value.TopLeft + (TargetPoint.TopLeft - StartPoint.Value.TopLeft) * easingValue),
                (float)(StartPoint.Value.TopRight + (TargetPoint.TopRight - StartPoint.Value.TopRight) * easingValue),
                (float)(StartPoint.Value.BottomRight + (TargetPoint.BottomRight - StartPoint.Value.BottomRight) * easingValue),
                (float)(StartPoint.Value.BottomLeft + (TargetPoint.BottomLeft - StartPoint.Value.BottomLeft) * easingValue)
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
            var previousSimpleCornerRadiusFAnimation = ((RxSimpleCornerRadiusFAnimation)previousAnimation);

            StartPoint = previousSimpleCornerRadiusFAnimation.CurrentValue();

            if (!previousSimpleCornerRadiusFAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration * previousSimpleCornerRadiusFAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
