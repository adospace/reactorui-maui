namespace MauiReactor.Animations
{
    public abstract class RxSizeFAnimation : RxTweenAnimation
    {
        public RxSizeFAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract SizeF CurrentValue();
    }
    
}
