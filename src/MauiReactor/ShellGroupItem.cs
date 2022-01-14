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
    public partial interface IShellGroupItem
    {
        Microsoft.Maui.Controls.FlyoutDisplayOptions FlyoutDisplayOptions { get; set; }


    }

    public partial class ShellGroupItem<T> : BaseShellItem<T>, IShellGroupItem where T : Microsoft.Maui.Controls.ShellGroupItem, new()
    {
        public ShellGroupItem()
        {

        }

        public ShellGroupItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.FlyoutDisplayOptions IShellGroupItem.FlyoutDisplayOptions { get; set; } = (Microsoft.Maui.Controls.FlyoutDisplayOptions)Microsoft.Maui.Controls.ShellGroupItem.FlyoutDisplayOptionsProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShellGroupItem = (IShellGroupItem)this;
            if (NativeControl.FlyoutDisplayOptions != thisAsIShellGroupItem.FlyoutDisplayOptions) NativeControl.FlyoutDisplayOptions = thisAsIShellGroupItem.FlyoutDisplayOptions;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class ShellGroupItem : ShellGroupItem<Microsoft.Maui.Controls.ShellGroupItem>
    {
        public ShellGroupItem()
        {

        }

        public ShellGroupItem(Action<Microsoft.Maui.Controls.ShellGroupItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ShellGroupItemExtensions
    {
        public static T FlyoutDisplayOptions<T>(this T shellgroupitem, Microsoft.Maui.Controls.FlyoutDisplayOptions flyoutDisplayOptions) where T : IShellGroupItem
        {
            shellgroupitem.FlyoutDisplayOptions = flyoutDisplayOptions;
            return shellgroupitem;
        }


    }
}
