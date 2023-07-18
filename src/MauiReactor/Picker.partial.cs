using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IPicker
    {
        IReadOnlyList<string>? ItemsSource { get; set; }
    }

    public partial class Picker<T>
    {
        IReadOnlyList<string>? IPicker.ItemsSource { get; set; }

        partial void OnEndUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPicker = (IPicker)this;
            if (NativeControl.ItemsSource == null ||
                thisAsIPicker.ItemsSource == null ||
                NativeControl.ItemsSource.Count != thisAsIPicker.ItemsSource.Count ||
                !NativeControl.ItemsSource.Cast<object>().SequenceEqual(thisAsIPicker.ItemsSource))
            {
                NativeControl.ItemsSource = (System.Collections.IList?)thisAsIPicker.ItemsSource;
            }
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
            picker.SelectedIndexChangedActionWithArgs = (sender, args) =>
            {
                if (sender is Microsoft.Maui.Controls.Picker picker)
                {
                    selectedIndexChangedAction?.Invoke(picker.SelectedIndex);
                }
            };
            return picker;
        }
    }
}