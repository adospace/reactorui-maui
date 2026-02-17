using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;

namespace MauiReactor;

public partial interface ITimePicker
{
    //Action<TimeSpan>? TimeSelectedAction { get; set; }
}

public partial class TimePicker<T>
{
    //Action<TimeSpan>? ITimePicker.TimeSelectedAction { get; set; }

    //partial void OnReset()
    //{
    //    var thisAsITimePicker = (ITimePicker)this;
    //    thisAsITimePicker.TimeSelectedAction = null;
    //}

    //protected override void OnAttachNativeEvents()
    //{
    //    Validate.EnsureNotNull(NativeControl);

    //    var thisAsITimePicker = (ITimePicker)this;

    //    if (thisAsITimePicker.TimeSelectedAction != null)
    //    {
    //        NativeControl.PropertyChanged += NativeControl_PropertyChanged;
    //    }

    //    base.OnAttachNativeEvents();
    //}

    //private void NativeControl_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    //{
    //    if (NativeControl != null && e.PropertyName == nameof(ITimePicker.Time))
    //    {
    //        var thisAsITimePicker = (ITimePicker)this;
    //        thisAsITimePicker.TimeSelectedAction?.Invoke(NativeControl.Time);
    //    }
    //}

    //protected override void OnDetachNativeEvents()
    //{
    //    if (NativeControl != null)
    //    {
    //        NativeControl.PropertyChanged -= NativeControl_PropertyChanged;
    //    }

    //    base.OnDetachNativeEvents();
    //}
}

public partial class TimePickerExtensions
{
    public static T OnTimeSelected<T>(this T inputView, Action<TimeSpan?>? action) where T : ITimePicker
    {
        inputView.TimeSelectedEvent = new SyncEventCommand<TimeChangedEventArgs>((s, args) => action?.Invoke(args.NewTime));
        return inputView;
    }

    public static T OnTimeSelected<T>(this T timePicker, Func<TimeSpan?, Task>? timeSelectedAction, bool runInBackground = false)
        where T : ITimePicker
    {
        if (timeSelectedAction != null)
        {
            timePicker.TimeSelectedEvent = new AsyncEventCommand<TimeChangedEventArgs>(executeWithArgs: (args) => timeSelectedAction.Invoke(args.NewTime), runInBackground);
        }
        return timePicker;
    }
}
