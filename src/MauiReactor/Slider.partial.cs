using MauiReactor.Internals;
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
        slider.ValueChangedEvent = new SyncEventCommand<ValueChangedEventArgs>((sender, args) => valueChangedActionWithArgs?.Invoke(args.NewValue));
        return slider;
    }

    public static T OnValueChanged<T>(this T slider, Func<double, Task>? valueChangedAction, bool runInBackground = false)
        where T : ISlider
    {
        if (valueChangedAction != null)
        {
            slider.ValueChangedEvent = new AsyncEventCommand<ValueChangedEventArgs>(executeWithArgs: (args) => valueChangedAction.Invoke(args.NewValue), runInBackground);
        }    
            
        return slider;
    }

}
