using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static partial class CheckBoxExtensions
{
    public static T OnCheckedChanged<T>(this T checkBox, Action<bool>? checkedChangedAction)
        where T : ICheckBox
    {
        if (checkedChangedAction != null)
        {
            checkBox.CheckedChangedActionWithArgs = (s, e) => checkedChangedAction?.Invoke(e.Value);
        }
        return checkBox;
    }
}
