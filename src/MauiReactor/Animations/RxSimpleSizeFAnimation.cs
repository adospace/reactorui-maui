using System.Numerics;

namespace MauiReactor.Animations
{
    public class RxSimpleSizeFAnimation : RxSizeFAnimation
    {
        public RxSimpleSizeFAnimation(SizeF targetPoint, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetPoint = targetPoint;
        }

        public SizeF TargetPoint { get; }
        public SizeF? StartPoint { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;
        internal override object GetCurrentValue() => CurrentValue();

        public override SizeF CurrentValue()
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

            return new SizeF(
                (float)(StartPoint.Value.Width + (TargetPoint.Width - StartPoint.Value.Width) * easingValue),
                (float)(StartPoint.Value.Height + (TargetPoint.Height - StartPoint.Value.Height) * easingValue)
                );
        }

        private double Completion()
        {
            if (StartPoint == null)
                return 1.0;

            var currentValue = CurrentValue();

            var v = ((TargetPoint.Width != StartPoint.Value.Width) ? (currentValue.Width - StartPoint.Value.Width) / (TargetPoint.Width - StartPoint.Value.Width) : 1.0) *
                ((TargetPoint.Height != StartPoint.Value.Height) ? (currentValue.Height - StartPoint.Value.Height) / (TargetPoint.Height - StartPoint.Value.Height) : 1.0);

            return v;

        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            var previousSimpleSizeFAnimation = ((RxSimpleSizeFAnimation)previousAnimation);
            StartPoint = previousSimpleSizeFAnimation.CurrentValue();

            if (!previousSimpleSizeFAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration * previousSimpleSizeFAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
