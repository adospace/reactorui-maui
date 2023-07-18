using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static partial class SliderExtensions
{
    public static T OnValueChanged<T>(this T slider, Action<double>? valueChangedActionWithArgs) where T : ISlider
    {
        slider.ValueChangedActionWithArgs = (sender, args) => valueChangedActionWithArgs?.Invoke(args.NewValue);
        return slider;
    }
}
