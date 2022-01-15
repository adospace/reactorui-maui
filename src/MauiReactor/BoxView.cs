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
    public partial interface IBoxView
    {
        Microsoft.Maui.Graphics.Color Color { get; set; }
        Microsoft.Maui.CornerRadius CornerRadius { get; set; }


    }
    public partial class BoxView<T> : View<T>, IBoxView where T : Microsoft.Maui.Controls.BoxView, new()
    {
        public BoxView()
        {

        }

        public BoxView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Graphics.Color IBoxView.Color { get; set; } = (Microsoft.Maui.Graphics.Color)Microsoft.Maui.Controls.BoxView.ColorProperty.DefaultValue;
        Microsoft.Maui.CornerRadius IBoxView.CornerRadius { get; set; } = (Microsoft.Maui.CornerRadius)Microsoft.Maui.Controls.BoxView.CornerRadiusProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIBoxView = (IBoxView)this;
            if (NativeControl.Color != thisAsIBoxView.Color) NativeControl.Color = thisAsIBoxView.Color;
            if (NativeControl.CornerRadius != thisAsIBoxView.CornerRadius) NativeControl.CornerRadius = thisAsIBoxView.CornerRadius;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class BoxView : BoxView<Microsoft.Maui.Controls.BoxView>
    {
        public BoxView()
        {

        }

        public BoxView(Action<Microsoft.Maui.Controls.BoxView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class BoxViewExtensions
    {
        public static T Color<T>(this T boxview, Microsoft.Maui.Graphics.Color color) where T : IBoxView
        {
            boxview.Color = color;
            return boxview;
        }

        public static T CornerRadius<T>(this T boxview, Microsoft.Maui.CornerRadius cornerRadius) where T : IBoxView
        {
            boxview.CornerRadius = cornerRadius;
            return boxview;
        }


    }
}
