using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface IPicker
{
    IReadOnlyList<string>? ItemsSource { get; set; }
}

public partial class Picker<T>
{
    IReadOnlyList<string>? IPicker.ItemsSource { get; set; }

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIPicker = (IPicker)this;
        if (NativeControl.ItemsSource == null ||
            thisAsIPicker.ItemsSource == null ||
            NativeControl.ItemsSource.Count != thisAsIPicker.ItemsSource.Count ||
            !NativeControl.ItemsSource.Cast<object>().SequenceEqual(thisAsIPicker.ItemsSource))
        {
            if (thisAsIPicker.ItemsSource is System.Collections.IList list)
            {
                NativeControl.ItemsSource = list;
            }
            else
            {
                NativeControl.ItemsSource = thisAsIPicker.ItemsSource?.ToList();
            }
        }
        base.OnUpdate();
    }
}


public static partial class PickerExtensions
{
    public static T ItemsSource<T>(this T picker, IReadOnlyList<string>? itemsSource) where T : IPicker
    {
        picker.ItemsSource = itemsSource;
        return picker;
    }

    public static T OnSelectedIndexChanged<T>(this T picker, Action<int>? selectedIndexChangedAction) where T : IPicker
    {
        picker.SelectedIndexChangedEvent = new SyncEventCommand<EventArgs>((sender, args) =>
        {
            if (sender is Microsoft.Maui.Controls.Picker picker)
            {
                selectedIndexChangedAction?.Invoke(picker.SelectedIndex);
            }
        });
        return picker;
    }

    public static T OnSelectedIndexChanged<T>(this T picker, Func<int, Task>? selectedIndexChangedAction) where T : IPicker
    {
        picker.SelectedIndexChangedEvent = new AsyncEventCommand<EventArgs>((sender, args) =>
        {
            if (sender is Microsoft.Maui.Controls.Picker picker &&
                selectedIndexChangedAction != null)
            {
                return selectedIndexChangedAction.Invoke(picker.SelectedIndex);
            }

            return Task.CompletedTask;
        });
        return picker;
    }
}