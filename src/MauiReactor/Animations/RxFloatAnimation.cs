namespace MauiReactor.Animations
{
    public class RxFloatAnimation : RxTweenAnimation
    {
        public RxFloatAnimation(float targetValue, Easing? easing = null, float? duration = null) : base(easing, duration)
        {
            TargetValue = targetValue;
            //System.Diagnostics.Debug.WriteLine($"RxFloatAnimation(TargetValue={TargetValue})");
        }

        public float TargetValue { get; }
        public float? StartValue { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartValue == null || StartValue.Value == TargetValue;

        internal override object GetCurrentValue() => CurrentValue();

        public float CurrentValue()
        {
            if (StartValue == null)
                return TargetValue;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime - InitialDelay.GetValueOrDefault();

            if (elapsedTime < 0)
            {
                return StartValue.Value;
            }

            var duration = Duration ?? DefaultDuration;

            System.Diagnostics.Debug.Assert(elapsedTime >= 0);

            if (elapsedTime >= duration)
            {
                //System.Diagnostics.Debug.WriteLine($"RxFloatAnimation(Completed StartTime={StartTime} CurrentTime={currentTime} ElapsedTime={elapsedTime} Duration={duration})");
                _isCompleted = true;
                return TargetValue;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            var v = (float)(StartValue.Value + (TargetValue - StartValue.Value) * easingValue);

            //System.Diagnostics.Debug.WriteLine($"RxFloatAnimation(EasingValue={easingValue} CurrentValue={v} StartValue={StartValue} TargetValue={TargetValue} StartTime={StartTime} CurrentTime={currentTime} ElapsedTime={elapsedTime})");

            return v;
        }

        private float Completion()
        {
            if (StartValue == null)
                return 1.0f;

            return (CurrentValue() - StartValue.Value) / (TargetValue - StartValue.Value);
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            var previousFloatAnimation = ((RxFloatAnimation)previousAnimation);

            StartValue = previousFloatAnimation.CurrentValue();
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxFloatAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");

            if (!previousFloatAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration * previousFloatAnimation.Completion());
                //System.Diagnostics.Debug.WriteLine($"previousCompletion={previousFloatAnimation.Completion()} -> completion={Completion()}");
                
                System.Diagnostics.Debug.Assert(StartTime > 0);
            }

            base.OnMigrateFrom(previousAnimation);
        }
    }

}