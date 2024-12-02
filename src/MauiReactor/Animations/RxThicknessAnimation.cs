using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Animations
{
    public abstract class RxThicknessAnimation : RxTweenAnimation
    {
        public RxThicknessAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract Thickness CurrentValue();

        internal override object GetCurrentValue() => CurrentValue();
    }
    
}
