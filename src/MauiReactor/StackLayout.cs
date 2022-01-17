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
    public partial interface IStackLayout : IStackBase
    {
        PropertyValue<Microsoft.Maui.Controls.StackOrientation>? Orientation { get; set; }


    }
    public partial class StackLayout<T> : StackBase<T>, IStackLayout where T : Microsoft.Maui.Controls.StackLayout, new()
    {
        public StackLayout()
        {

        }

        public StackLayout(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Controls.StackOrientation>? IStackLayout.Orientation { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIStackLayout = (IStackLayout)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.StackLayout.OrientationProperty, thisAsIStackLayout.Orientation);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class StackLayout : StackLayout<Microsoft.Maui.Controls.StackLayout>
    {
        public StackLayout()
        {

        }

        public StackLayout(Action<Microsoft.Maui.Controls.StackLayout?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class StackLayoutExtensions
    {
        public static T Orientation<T>(this T stacklayout, Microsoft.Maui.Controls.StackOrientation orientation) where T : IStackLayout
        {
            stacklayout.Orientation = new PropertyValue<Microsoft.Maui.Controls.StackOrientation>(orientation);
            return stacklayout;
        }
        public static T Orientation<T>(this T stacklayout, Func<Microsoft.Maui.Controls.StackOrientation> orientationFunc) where T : IStackLayout
        {
            stacklayout.Orientation = new PropertyValue<Microsoft.Maui.Controls.StackOrientation>(orientationFunc);
            return stacklayout;
        }




    }
}
