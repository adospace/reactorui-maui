using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IShellItem
    {
        Microsoft.Maui.Controls.ShellSection CurrentItem { get; set; }


    }
    public partial class ShellItem<T> : ShellGroupItem<T>, IShellItem where T : Microsoft.Maui.Controls.ShellItem, new()
    {
        public ShellItem()
        {

        }

        public ShellItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.ShellSection IShellItem.CurrentItem { get; set; } = (Microsoft.Maui.Controls.ShellSection)Microsoft.Maui.Controls.ShellItem.CurrentItemProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShellItem = (IShellItem)this;
            if (NativeControl.CurrentItem != thisAsIShellItem.CurrentItem) NativeControl.CurrentItem = thisAsIShellItem.CurrentItem;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class ShellItem : ShellItem<Microsoft.Maui.Controls.ShellItem>
    {
        public ShellItem()
        {

        }

        public ShellItem(Action<Microsoft.Maui.Controls.ShellItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ShellItemExtensions
    {
        public static T CurrentItem<T>(this T shellitem, Microsoft.Maui.Controls.ShellSection currentItem) where T : IShellItem
        {
            shellitem.CurrentItem = currentItem;
            return shellitem;
        }


    }
}
