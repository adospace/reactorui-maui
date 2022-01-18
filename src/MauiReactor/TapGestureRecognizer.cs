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
    public partial interface ITapGestureRecognizer : IGestureRecognizer
    {
        PropertyValue<int>? NumberOfTapsRequired { get; set; }

        Action? TappedAction { get; set; }
        Action<object?, EventArgs>? TappedActionWithArgs { get; set; }

    }
    public sealed partial class TapGestureRecognizer : GestureRecognizer<Microsoft.Maui.Controls.TapGestureRecognizer>, ITapGestureRecognizer
    {
        public TapGestureRecognizer()
        {

        }

        public TapGestureRecognizer(Action<Microsoft.Maui.Controls.TapGestureRecognizer?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<int>? ITapGestureRecognizer.NumberOfTapsRequired { get; set; }

        Action? ITapGestureRecognizer.TappedAction { get; set; }
        Action<object?, EventArgs>? ITapGestureRecognizer.TappedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsITapGestureRecognizer = (ITapGestureRecognizer)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TapGestureRecognizer.NumberOfTapsRequiredProperty, thisAsITapGestureRecognizer.NumberOfTapsRequired);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsITapGestureRecognizer = (ITapGestureRecognizer)this;
            if (thisAsITapGestureRecognizer.TappedAction != null || thisAsITapGestureRecognizer.TappedActionWithArgs != null)
            {
                NativeControl.Tapped += NativeControl_Tapped;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tapped(object? sender, EventArgs e)
        {
            var thisAsITapGestureRecognizer = (ITapGestureRecognizer)this;
            thisAsITapGestureRecognizer.TappedAction?.Invoke();
            thisAsITapGestureRecognizer.TappedActionWithArgs?.Invoke(sender, e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Tapped -= NativeControl_Tapped;
            }

            base.OnDetachNativeEvents();
        }

    }


    public static partial class TapGestureRecognizerExtensions
    {
        public static T NumberOfTapsRequired<T>(this T tapgesturerecognizer, int numberOfTapsRequired) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.NumberOfTapsRequired = new PropertyValue<int>(numberOfTapsRequired);
            return tapgesturerecognizer;
        }
        public static T NumberOfTapsRequired<T>(this T tapgesturerecognizer, Func<int> numberOfTapsRequiredFunc) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.NumberOfTapsRequired = new PropertyValue<int>(numberOfTapsRequiredFunc);
            return tapgesturerecognizer;
        }




        public static T OnTapped<T>(this T tapgesturerecognizer, Action tappedAction) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.TappedAction = tappedAction;
            return tapgesturerecognizer;
        }

        public static T OnTapped<T>(this T tapgesturerecognizer, Action<object?, EventArgs> tappedActionWithArgs) where T : ITapGestureRecognizer
        {
            tapgesturerecognizer.TappedActionWithArgs = tappedActionWithArgs;
            return tapgesturerecognizer;
        }
    }
}
