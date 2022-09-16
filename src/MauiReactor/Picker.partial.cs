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
        IEnumerable<string>? ItemsSource { get; set; }
    }

    public partial class Picker<T>
    {
        IEnumerable<string>? IPicker.ItemsSource { get; set; }

        partial void OnEndUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPicker = (IPicker)this;
            NativeControl.ItemsSource = thisAsIPicker.ItemsSource?.ToList();
        }
    }


    public static partial class PickerExtensions
    {
        public static T ItemsSource<T>(this T picker, IEnumerable<string>? itemsSource) where T : IPicker
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