// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable

namespace MauiReactor
{
    public partial interface IFlyoutPage : IPage
    {
        PropertyValue<bool>? IsGestureEnabled { get; set; }
        PropertyValue<bool>? IsPresented { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FlyoutLayoutBehavior>? FlyoutLayoutBehavior { get; set; }

        Action? IsPresentedChangedAction { get; set; }
        Action<object?, EventArgs>? IsPresentedChangedActionWithArgs { get; set; }

    }

    public partial class FlyoutPage<T> : Page<T>, IFlyoutPage where T : Microsoft.Maui.Controls.FlyoutPage, new()
    {
        public FlyoutPage()
        {

        }

        public FlyoutPage(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<bool>? IFlyoutPage.IsGestureEnabled { get; set; }
        PropertyValue<bool>? IFlyoutPage.IsPresented { get; set; }
        PropertyValue<Microsoft.Maui.Controls.FlyoutLayoutBehavior>? IFlyoutPage.FlyoutLayoutBehavior { get; set; }

        Action? IFlyoutPage.IsPresentedChangedAction { get; set; }
        Action<object?, EventArgs>? IFlyoutPage.IsPresentedChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIFlyoutPage = (IFlyoutPage)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlyoutPage.IsGestureEnabledProperty, thisAsIFlyoutPage.IsGestureEnabled);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlyoutPage.IsPresentedProperty, thisAsIFlyoutPage.IsPresented);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlyoutPage.FlyoutLayoutBehaviorProperty, thisAsIFlyoutPage.FlyoutLayoutBehavior);


            base.OnUpdate();

            OnEndUpdate();
        }


        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIFlyoutPage = (IFlyoutPage)this;
            if (thisAsIFlyoutPage.IsPresentedChangedAction != null || thisAsIFlyoutPage.IsPresentedChangedActionWithArgs != null)
            {
                NativeControl.IsPresentedChanged += NativeControl_IsPresentedChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_IsPresentedChanged(object? sender, EventArgs e)
        {
            var thisAsIFlyoutPage = (IFlyoutPage)this;
            thisAsIFlyoutPage.IsPresentedChangedAction?.Invoke();
            thisAsIFlyoutPage.IsPresentedChangedActionWithArgs?.Invoke(sender, e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.IsPresentedChanged -= NativeControl_IsPresentedChanged;
            }

            base.OnDetachNativeEvents();
        }

    }

    public partial class FlyoutPage : FlyoutPage<Microsoft.Maui.Controls.FlyoutPage>
    {
        public FlyoutPage()
        {

        }

        public FlyoutPage(Action<Microsoft.Maui.Controls.FlyoutPage?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class FlyoutPageExtensions
    {
        public static T IsGestureEnabled<T>(this T flyoutPage, bool isGestureEnabled) where T : IFlyoutPage
        {
            flyoutPage.IsGestureEnabled = new PropertyValue<bool>(isGestureEnabled);
            return flyoutPage;
        }

        public static T IsGestureEnabled<T>(this T flyoutPage, Func<bool> isGestureEnabledFunc) where T : IFlyoutPage
        {
            flyoutPage.IsGestureEnabled = new PropertyValue<bool>(isGestureEnabledFunc);
            return flyoutPage;
        }



        public static T IsPresented<T>(this T flyoutPage, bool isPresented) where T : IFlyoutPage
        {
            flyoutPage.IsPresented = new PropertyValue<bool>(isPresented);
            return flyoutPage;
        }

        public static T IsPresented<T>(this T flyoutPage, Func<bool> isPresentedFunc) where T : IFlyoutPage
        {
            flyoutPage.IsPresented = new PropertyValue<bool>(isPresentedFunc);
            return flyoutPage;
        }



        public static T FlyoutLayoutBehavior<T>(this T flyoutPage, Microsoft.Maui.Controls.FlyoutLayoutBehavior flyoutLayoutBehavior) where T : IFlyoutPage
        {
            flyoutPage.FlyoutLayoutBehavior = new PropertyValue<Microsoft.Maui.Controls.FlyoutLayoutBehavior>(flyoutLayoutBehavior);
            return flyoutPage;
        }

        public static T FlyoutLayoutBehavior<T>(this T flyoutPage, Func<Microsoft.Maui.Controls.FlyoutLayoutBehavior> flyoutLayoutBehaviorFunc) where T : IFlyoutPage
        {
            flyoutPage.FlyoutLayoutBehavior = new PropertyValue<Microsoft.Maui.Controls.FlyoutLayoutBehavior>(flyoutLayoutBehaviorFunc);
            return flyoutPage;
        }




        public static T OnIsPresentedChanged<T>(this T flyoutPage, Action isPresentedChangedAction) where T : IFlyoutPage
        {
            flyoutPage.IsPresentedChangedAction = isPresentedChangedAction;
            return flyoutPage;
        }

        public static T OnIsPresentedChanged<T>(this T flyoutPage, Action<object?, EventArgs> isPresentedChangedActionWithArgs) where T : IFlyoutPage
        {
            flyoutPage.IsPresentedChangedActionWithArgs = isPresentedChangedActionWithArgs;
            return flyoutPage;
        }
    }
}