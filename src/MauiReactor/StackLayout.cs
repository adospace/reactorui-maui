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
    public partial interface IStackLayout
    {
        Microsoft.Maui.Controls.StackOrientation Orientation { get; set; }


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

        Microsoft.Maui.Controls.StackOrientation IStackLayout.Orientation { get; set; } = (Microsoft.Maui.Controls.StackOrientation)Microsoft.Maui.Controls.StackLayout.OrientationProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIStackLayout = (IStackLayout)this;
            if (NativeControl.Orientation != thisAsIStackLayout.Orientation) NativeControl.Orientation = thisAsIStackLayout.Orientation;


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
            stacklayout.Orientation = orientation;
            return stacklayout;
        }


    }
}
