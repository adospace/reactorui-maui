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
    public partial interface IFrame : IContentView
    {
        PropertyValue<Microsoft.Maui.Graphics.Color>? BorderColor { get; set; }
        PropertyValue<bool>? HasShadow { get; set; }
        PropertyValue<float>? CornerRadius { get; set; }


    }
    public partial class Frame<T> : ContentView<T>, IFrame where T : Microsoft.Maui.Controls.Frame, new()
    {
        public Frame()
        {

        }

        public Frame(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Graphics.Color>? IFrame.BorderColor { get; set; }
        PropertyValue<bool>? IFrame.HasShadow { get; set; }
        PropertyValue<float>? IFrame.CornerRadius { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIFrame = (IFrame)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Frame.BorderColorProperty, thisAsIFrame.BorderColor);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Frame.HasShadowProperty, thisAsIFrame.HasShadow);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Frame.CornerRadiusProperty, thisAsIFrame.CornerRadius);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class Frame : Frame<Microsoft.Maui.Controls.Frame>
    {
        public Frame()
        {

        }

        public Frame(Action<Microsoft.Maui.Controls.Frame?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class FrameExtensions
    {
        public static T BorderColor<T>(this T frame, Microsoft.Maui.Graphics.Color borderColor) where T : IFrame
        {
            frame.BorderColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(borderColor);
            return frame;
        }
        public static T BorderColor<T>(this T frame, Func<Microsoft.Maui.Graphics.Color> borderColorFunc) where T : IFrame
        {
            frame.BorderColor = new PropertyValue<Microsoft.Maui.Graphics.Color>(borderColorFunc);
            return frame;
        }



        public static T HasShadow<T>(this T frame, bool hasShadow) where T : IFrame
        {
            frame.HasShadow = new PropertyValue<bool>(hasShadow);
            return frame;
        }
        public static T HasShadow<T>(this T frame, Func<bool> hasShadowFunc) where T : IFrame
        {
            frame.HasShadow = new PropertyValue<bool>(hasShadowFunc);
            return frame;
        }



        public static T CornerRadius<T>(this T frame, float cornerRadius) where T : IFrame
        {
            frame.CornerRadius = new PropertyValue<float>(cornerRadius);
            return frame;
        }
        public static T CornerRadius<T>(this T frame, Func<float> cornerRadiusFunc) where T : IFrame
        {
            frame.CornerRadius = new PropertyValue<float>(cornerRadiusFunc);
            return frame;
        }




    }
}
