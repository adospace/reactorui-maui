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
    public partial interface IFlyoutItem
    {
        bool IsVisible { get; set; }


    }

    public partial class FlyoutItem<T> : ShellItem<T>, IFlyoutItem where T : Microsoft.Maui.Controls.FlyoutItem, new()
    {
        public FlyoutItem()
        {

        }

        public FlyoutItem(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        bool IFlyoutItem.IsVisible { get; set; } = (bool)Microsoft.Maui.Controls.FlyoutItem.IsVisibleProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIFlyoutItem = (IFlyoutItem)this;
            if (NativeControl.IsVisible != thisAsIFlyoutItem.IsVisible) NativeControl.IsVisible = thisAsIFlyoutItem.IsVisible;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class FlyoutItem : FlyoutItem<Microsoft.Maui.Controls.FlyoutItem>
    {
        public FlyoutItem()
        {

        }

        public FlyoutItem(Action<Microsoft.Maui.Controls.FlyoutItem?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class FlyoutItemExtensions
    {
        public static T IsVisible<T>(this T flyoutitem, bool isVisible) where T : IFlyoutItem
        {
            flyoutitem.IsVisible = isVisible;
            return flyoutitem;
        }


    }
}
