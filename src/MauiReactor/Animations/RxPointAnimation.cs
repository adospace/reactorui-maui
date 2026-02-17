namespace MauiReactor.Animations
{
    public abstract class RxPointAnimation : RxTweenAnimation
    {
        public RxPointAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract Point CurrentValue();
    }
}
