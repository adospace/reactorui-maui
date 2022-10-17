namespace MauiReactor.Animations
{
    public abstract class RxCornerRadiusAnimation : RxTweenAnimation
    {
        public RxCornerRadiusAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract CornerRadius CurrentValue();
    }
    
}
