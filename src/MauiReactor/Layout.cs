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
    public partial interface ILayout
    {
        Microsoft.Maui.Thickness Padding { get; set; }


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

        Microsoft.Maui.Thickness ILayout.Padding { get; set; } = (Microsoft.Maui.Thickness)Microsoft.Maui.Controls.Layout.PaddingProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsILayout = (ILayout)this;
            if (NativeControl.Padding != thisAsILayout.Padding) NativeControl.Padding = thisAsILayout.Padding;


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
            layout.Padding = padding;
            return layout;
        }
        public static T Padding<T>(this T layout, double leftRight, double topBottom) where T : ILayout
        {
            layout.Padding = new Thickness(leftRight, topBottom);
            return layout;
        }
        public static T Padding<T>(this T layout, double uniformSize) where T : ILayout
        {
            layout.Padding = new Thickness(uniformSize);
            return layout;
        }


    }
}
