namespace MauiReactor.Animations
{
    public abstract class RxThicknessFAnimation : RxTweenAnimation
    {
        public RxThicknessFAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract ThicknessF CurrentValue();
    }
    
}
