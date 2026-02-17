using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Animations
{

    public abstract class RxCornerRadiusFAnimation : RxTweenAnimation
    {
        public RxCornerRadiusFAnimation(Easing? easing = null, double? duration = null) : base(easing, duration)
        {
        }

        public abstract CornerRadiusF CurrentValue();
    }
    
}
