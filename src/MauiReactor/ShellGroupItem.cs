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
    public partial interface IShellGroupItem : IBaseShellItem
    {
        PropertyValue<Microsoft.Maui.Controls.FlyoutDisplayOptions>? FlyoutDisplayOptions { get; set; }


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

        PropertyValue<Microsoft.Maui.Controls.FlyoutDisplayOptions>? IShellGroupItem.FlyoutDisplayOptions { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShellGroupItem = (IShellGroupItem)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ShellGroupItem.FlyoutDisplayOptionsProperty, thisAsIShellGroupItem.FlyoutDisplayOptions);


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
            shellgroupitem.FlyoutDisplayOptions = new PropertyValue<Microsoft.Maui.Controls.FlyoutDisplayOptions>(flyoutDisplayOptions);
            return shellgroupitem;
        }
        public static T FlyoutDisplayOptions<T>(this T shellgroupitem, Func<Microsoft.Maui.Controls.FlyoutDisplayOptions> flyoutDisplayOptionsFunc) where T : IShellGroupItem
        {
            shellgroupitem.FlyoutDisplayOptions = new PropertyValue<Microsoft.Maui.Controls.FlyoutDisplayOptions>(flyoutDisplayOptionsFunc);
            return shellgroupitem;
        }




    }
}
