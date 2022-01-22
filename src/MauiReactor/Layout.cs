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
    public partial interface ILayout : IView
    {
        PropertyValue<Microsoft.Maui.Thickness>? Padding { get; set; }


    }
    public abstract partial class Layout<T> : View<T>, ILayout where T : Microsoft.Maui.Controls.Layout, new()
    {
        protected Layout()
        {

        }

        protected Layout(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Thickness>? ILayout.Padding { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsILayout = (ILayout)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Layout.PaddingProperty, thisAsILayout.Padding);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }


    public static partial class LayoutExtensions
    {
        public static T Padding<T>(this T layout, Microsoft.Maui.Thickness padding) where T : ILayout
        {
            layout.Padding = new PropertyValue<Microsoft.Maui.Thickness>(padding);
            return layout;
        }

        public static T Padding<T>(this T layout, Func<Microsoft.Maui.Thickness> paddingFunc) where T : ILayout
        {
            layout.Padding = new PropertyValue<Microsoft.Maui.Thickness>(paddingFunc);
            return layout;
        }
        public static T Padding<T>(this T layout, double leftRight, double topBottom) where T : ILayout
        {
            layout.Padding = new PropertyValue<Microsoft.Maui.Thickness>(new Thickness(leftRight, topBottom));
            return layout;
        }
        public static T Padding<T>(this T layout, double uniformSize) where T : ILayout
        {
            layout.Padding = new PropertyValue<Microsoft.Maui.Thickness>(new Thickness(uniformSize));
            return layout;
        }




    }
}
