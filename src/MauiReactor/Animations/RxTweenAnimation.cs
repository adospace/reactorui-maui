namespace MauiReactor.Animations
{
    public abstract class RxTweenAnimation : RxAnimation
    {
        public const double DefaultDuration = 300.0;
        public RxTweenAnimation(Easing? easing = null, double? duration = null, double? initialDelay = null)
        {
            Easing = easing;
            Duration = duration;
            InitialDelay = initialDelay;
        }

        public double? Duration { get; internal set; }
        public Easing? Easing { get; internal set; }
        public double? InitialDelay { get; internal set; }
    }
}
