using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface IPicker
{
    IReadOnlyList<object>? ItemsSource { get; set; }

    BindingBase? ItemDisplayBinding { get; set; }
}

public partial class Picker<T>
{
    IReadOnlyList<object>? IPicker.ItemsSource { get; set; }

    BindingBase? IPicker.ItemDisplayBinding { get; set; }

    protected override void OnUpdate()
    {
        var thisAsIPicker = (IPicker)this;
        var nativeControl = NativeControl.EnsureNotNull();

        if (nativeControl.ItemsSource == null ||
            thisAsIPicker.ItemsSource == null ||
            nativeControl.ItemsSource.Count != thisAsIPicker.ItemsSource.Count ||
            !nativeControl.ItemsSource.Cast<object>().SequenceEqual(thisAsIPicker.ItemsSource))
        {
            if (thisAsIPicker.ItemsSource is System.Collections.IList list)
            {
                nativeControl.ItemsSource = list;
            }
            else
            {
                nativeControl.ItemsSource = thisAsIPicker.ItemsSource?.ToList();
            }
        }

        nativeControl.ItemDisplayBinding = thisAsIPicker.ItemDisplayBinding;

        base.OnUpdate();
    }
}


public static partial class PickerExtensions
{
    public static T ItemsSource<T>(this T picker, IReadOnlyList<object>? itemsSource) where T : IPicker
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

    public static T ItemDisplayBinding<T>(this T picker, BindingBase? itemDisplayBinding) where T : IPicker
    {
        picker.ItemDisplayBinding = itemDisplayBinding;

        return picker;
    }
}