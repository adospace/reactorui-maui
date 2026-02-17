namespace MauiReactor.Animations
{
    public enum ColorTransitionModel
    { 
        RGB,

        HSL,
    }

    public class RxSimpleColorAnimation : RxColorAnimation
    {
        public RxSimpleColorAnimation(Color targetColor, ColorTransitionModel colorTransitionModel = ColorTransitionModel.HSL, Easing? easing = null, double? duration = null)
            : base(easing, duration)
        {
            TargetColor = targetColor;
            TransitionModel = colorTransitionModel;
        }

        public Color TargetColor { get; }
        public ColorTransitionModel TransitionModel { get; }
        public Color? StartColor { get; private set; }

        private bool _isCompleted;
        public override bool IsCompleted() => _isCompleted || StartColor == null || StartColor == TargetColor;
        internal override object GetCurrentValue() => CurrentValue();

        public override Color CurrentValue()
        {
            if (StartColor == null)
                return TargetColor;

            var currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            var elapsedTime = currentTime - StartTime - InitialDelay.GetValueOrDefault();

            if (elapsedTime < 0)
            {
                return StartColor;
            }

            var duration = Duration ?? DefaultDuration;

            if (elapsedTime >= duration)
            {
                _isCompleted = true;
                return TargetColor;
            }

            var easing = Easing ?? Easing.Linear;

            var easingValue = easing.Ease(elapsedTime / duration);

            if (TransitionModel == ColorTransitionModel.RGB)
            {
                return Color.FromRgba(
                   StartColor.Red + (TargetColor.Red - StartColor.Red) * easingValue,
                   StartColor.Green + (TargetColor.Green - StartColor.Green) * easingValue,
                   StartColor.Blue + (TargetColor.Blue - StartColor.Blue) * easingValue,
                   StartColor.Alpha + (TargetColor.Alpha - StartColor.Alpha) * easingValue
                   );
            }

            return Color.FromHsla(
                StartColor.GetHue() + (TargetColor.GetHue() - StartColor.GetHue()) * easingValue,
                StartColor.GetSaturation() + (TargetColor.GetSaturation() - StartColor.GetSaturation()) * easingValue,
                StartColor.GetLuminosity() + (TargetColor.GetLuminosity() - StartColor.GetLuminosity()) * easingValue,
                StartColor.Alpha + (TargetColor.Alpha - StartColor.Alpha) * easingValue
                );
        }

        private double Completion()
        {
            if (StartColor == null)
                return 1.0;

            var currentValue = CurrentValue();


            if (TransitionModel == ColorTransitionModel.RGB)
            {
                return ((TargetColor.Red != StartColor.Red) ? (currentValue.Red - StartColor.Red) / (TargetColor.Red - StartColor.Red) : 1.0) *
                    ((TargetColor.Green != StartColor.Green) ? (currentValue.Green - StartColor.Green) / (TargetColor.Green - StartColor.Green) : 1.0) *
                    ((TargetColor.Blue != StartColor.Blue) ? (currentValue.Blue - StartColor.Blue) / (TargetColor.Blue - StartColor.Blue) : 1.0) *
                    ((TargetColor.Alpha != StartColor.Alpha) ? (currentValue.Alpha - StartColor.Alpha) / (TargetColor.Alpha - StartColor.Alpha) : 1.0);

                //return Math.Pow(
                //    Math.Pow((currentValue.Red - StartColor.Red) / (TargetColor.Red - StartColor.Red), 2.0) +
                //    Math.Pow((currentValue.Green - StartColor.Green) / (TargetColor.Green - StartColor.Green), 2.0) +
                //    Math.Pow((currentValue.Blue - StartColor.Blue) / (TargetColor.Blue - StartColor.Blue), 2.0) +
                //    Math.Pow((currentValue.Alpha - StartColor.Alpha) / (TargetColor.Alpha - StartColor.Alpha), 2.0), 0.25);
            }

            return ((TargetColor.GetHue() != StartColor.GetHue()) ? (currentValue.GetHue() - StartColor.GetHue()) / (TargetColor.GetHue() - StartColor.GetHue()) : 1.0) *
                ((TargetColor.GetSaturation() != StartColor.GetSaturation()) ? (currentValue.GetSaturation() - StartColor.GetSaturation()) / (TargetColor.GetSaturation() - StartColor.GetSaturation()) : 1.0) *
                ((TargetColor.GetLuminosity() != StartColor.GetLuminosity()) ? (currentValue.GetLuminosity() - StartColor.GetLuminosity()) / (TargetColor.GetLuminosity() - StartColor.GetLuminosity()) : 1.0) *
                ((TargetColor.Alpha != StartColor.Alpha) ? (currentValue.Alpha - StartColor.Alpha) / (TargetColor.Alpha - StartColor.Alpha) : 1.0);

            //return Math.Pow(
            //    Math.Pow((currentValue.GetHue() - StartColor.GetHue()) / (TargetColor.GetHue() - StartColor.GetHue()), 2.0) +
            //    Math.Pow((currentValue.GetSaturation() - StartColor.GetSaturation()) / (TargetColor.GetSaturation() - StartColor.GetSaturation()), 2.0) +
            //    Math.Pow((currentValue.GetLuminosity() - StartColor.GetLuminosity()) / (TargetColor.GetLuminosity() - StartColor.GetLuminosity()), 2.0) +
            //    Math.Pow((currentValue.Alpha - StartColor.Alpha) / (TargetColor.Alpha - StartColor.Alpha), 2.0), 0.25);

        }

        protected override void OnMigrateFrom(RxAnimation previousAnimation)
        {
            //System.Diagnostics.Debug.Assert(previousAnimation != this);
            //System.Diagnostics.Debug.WriteLine($"Migrate StartValue from {StartValue} to {((RxDoubleAnimation)previousAnimation).TargetValue} (TargetValue={TargetValue})");
            var previousSimpleColorAnimation = ((RxSimpleColorAnimation)previousAnimation);
            StartColor = previousSimpleColorAnimation.CurrentValue();

            if (!previousSimpleColorAnimation.IsCompleted())
            {
                var duration = Duration ?? DefaultDuration;
                StartTime -= (long)(duration * previousSimpleColorAnimation.Completion());
            }

            base.OnMigrateFrom(previousAnimation);
        }

    }
}
