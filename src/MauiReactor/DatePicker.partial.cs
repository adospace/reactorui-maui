using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class DatePickerExtensions
{
    public static T OnDateSelected<T>(this T datePicker, Action<DateTime?>? dateSelectedAction) where T : IDatePicker
    {
        if (dateSelectedAction != null)
        {
            datePicker.DateSelectedEvent = new SyncEventCommand<DateChangedEventArgs>((args) => dateSelectedAction?.Invoke(args.NewDate));
        }
        return datePicker;
    }

    public static T OnDateSelected<T>(this T datePicker, Func<DateTime?, Task>? dateSelectedAction, bool runInBackground = false)
        where T : IDatePicker
    {
        if (dateSelectedAction != null)
        {
            datePicker.DateSelectedEvent = new AsyncEventCommand<DateChangedEventArgs>(executeWithArgs: (args) => dateSelectedAction.Invoke(args.NewDate), runInBackground);
        }
            
        return datePicker;
    }
}