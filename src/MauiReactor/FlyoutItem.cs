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
    public partial interface IFlyoutItem : IShellItem
    {
        PropertyValue<bool>? IsVisible { get; set; }


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

        PropertyValue<bool>? IFlyoutItem.IsVisible { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIFlyoutItem = (IFlyoutItem)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlyoutItem.IsVisibleProperty, thisAsIFlyoutItem.IsVisible);


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
        public static T IsVisible<T>(this T flyoutItem, bool isVisible) where T : IFlyoutItem
        {
            flyoutItem.IsVisible = new PropertyValue<bool>(isVisible);
            return flyoutItem;
        }

        public static T IsVisible<T>(this T flyoutItem, Func<bool> isVisibleFunc) where T : IFlyoutItem
        {
            flyoutItem.IsVisible = new PropertyValue<bool>(isVisibleFunc);
            return flyoutItem;
        }




    }
}
