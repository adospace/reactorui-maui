using System;
using System.Collections.Generic;
using System.Text;


namespace MauiReactor.Animations
{
    public abstract class RxColorAnimation : RxTweenAnimation
    {
        public RxColorAnimation(Easing easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract Color CurrentValue();
    }
}
