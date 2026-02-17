using System.Numerics;

namespace MauiReactor.Animations
{
    public abstract class RxVector2Animation : RxTweenAnimation
    {
        public RxVector2Animation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract Vector2 CurrentValue();
    }
}
