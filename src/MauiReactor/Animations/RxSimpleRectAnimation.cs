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
        public override bool IsCompleted()
        {
            //System.Diagnostics.Debug.WriteLine($"RxSimpleRectAnimation.IsCompleted(_isCompleted={_isCompleted}) StartPoint = {(StartPoint == null ? string.Empty : StartPoint.Value.X)} TargetPoint = {TargetPoint.X} => {_isCompleted || StartPoint == null || StartPoint.Value == TargetPoint}");

            return _isCompleted || StartPoint == null || StartPoint.Value == TargetPoint;
        }
        

        public override Rect CurrentValue()
        {
            if (StartPoint == null)
                return TargetPoint;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime;
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
           
            //System.Diagnostics.Debug.WriteLine($"RxSimpleRectAnimation.CurrentValue() From {StartPoint.Value.X} to {TargetPoint.X} (Current={currentValue} Elapsed={easingValue})");

            return currentValue;
        }

        private double Completion()
        {
            if (StartPoint == null)
                return 1.0;

            var currentValue = CurrentValue();

            return Math.Pow(
                Math.Pow((currentValue.X - StartPoint.Value.X) / (TargetPoint.X - StartPoint.Value.X), 2.0) +
                Math.Pow((currentValue.Y - StartPoint.Value.Y) / (TargetPoint.Y - StartPoint.Value.Y), 2.0) +
                Math.Pow((currentValue.Width - StartPoint.Value.Width) / (TargetPoint.Width - StartPoint.Value.Width), 2.0) +
                Math.Pow((currentValue.Height - StartPoint.Value.Height) / (TargetPoint.Height - StartPoint.Value.Height), 2.0), 
                0.25);
        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousRectAnimation = ((RxSimpleRectAnimation)previousAnimation);
            //StartPoint = previousDoubleAnimation.TargetPoint;
            //StartTime = previousDoubleAnimation.StartTime;
            //if (!previousDoubleAnimation.IsCompleted())
            //{
            //    var duration = Duration ?? DefaultDuration;
            //    StartTime -= (long)(duration - duration * previousDoubleAnimation.Completion());
            //    System.Diagnostics.Debug.Assert(StartTime >= 0);
            //}
            StartPoint = previousRectAnimation.TargetPoint;

            if (previousRectAnimation?.IsCompleted() == false)
            {
                //StartPoint = previousRectAnimation.StartPoint;
                //TargetPoint = previousRectAnimation.TargetPoint;
                //StartTime = previousRectAnimation.StartTime;
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
