namespace MauiReactor.Animations
{
    public abstract class RxPointFAnimation : RxTweenAnimation
    {
        public RxPointFAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract PointF CurrentValue();
    }
}
