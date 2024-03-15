using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class DatePickerExtensions
{
    public static T OnDateSelected<T>(this T datePicker, Action<DateTime>? dateSelectedAction) where T : IDatePicker
    {
        if (dateSelectedAction != null)
        {
            datePicker.DateSelectedActionWithArgs = (sender, args) => dateSelectedAction?.Invoke(args.NewDate);
        }
        return datePicker;
    }
}
