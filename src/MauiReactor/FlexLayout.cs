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
    public partial interface IFlexLayout : ILayout
    {
        PropertyValue<Microsoft.Maui.Layouts.FlexDirection>? Direction { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexJustify>? JustifyContent { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexAlignContent>? AlignContent { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexAlignItems>? AlignItems { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexPosition>? Position { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexWrap>? Wrap { get; set; }


    }
    public partial class FlexLayout<T> : Layout<T>, IFlexLayout where T : Microsoft.Maui.Controls.FlexLayout, new()
    {
        public FlexLayout()
        {

        }

        public FlexLayout(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Layouts.FlexDirection>? IFlexLayout.Direction { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexJustify>? IFlexLayout.JustifyContent { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexAlignContent>? IFlexLayout.AlignContent { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexAlignItems>? IFlexLayout.AlignItems { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexPosition>? IFlexLayout.Position { get; set; }
        PropertyValue<Microsoft.Maui.Layouts.FlexWrap>? IFlexLayout.Wrap { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIFlexLayout = (IFlexLayout)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlexLayout.DirectionProperty, thisAsIFlexLayout.Direction);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlexLayout.JustifyContentProperty, thisAsIFlexLayout.JustifyContent);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlexLayout.AlignContentProperty, thisAsIFlexLayout.AlignContent);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlexLayout.AlignItemsProperty, thisAsIFlexLayout.AlignItems);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlexLayout.PositionProperty, thisAsIFlexLayout.Position);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.FlexLayout.WrapProperty, thisAsIFlexLayout.Wrap);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class FlexLayout : FlexLayout<Microsoft.Maui.Controls.FlexLayout>
    {
        public FlexLayout()
        {

        }

        public FlexLayout(Action<Microsoft.Maui.Controls.FlexLayout?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class FlexLayoutExtensions
    {
        public static T Direction<T>(this T flexlayout, Microsoft.Maui.Layouts.FlexDirection direction) where T : IFlexLayout
        {
            flexlayout.Direction = new PropertyValue<Microsoft.Maui.Layouts.FlexDirection>(direction);
            return flexlayout;
        }
        public static T Direction<T>(this T flexlayout, Func<Microsoft.Maui.Layouts.FlexDirection> directionFunc) where T : IFlexLayout
        {
            flexlayout.Direction = new PropertyValue<Microsoft.Maui.Layouts.FlexDirection>(directionFunc);
            return flexlayout;
        }



        public static T JustifyContent<T>(this T flexlayout, Microsoft.Maui.Layouts.FlexJustify justifyContent) where T : IFlexLayout
        {
            flexlayout.JustifyContent = new PropertyValue<Microsoft.Maui.Layouts.FlexJustify>(justifyContent);
            return flexlayout;
        }
        public static T JustifyContent<T>(this T flexlayout, Func<Microsoft.Maui.Layouts.FlexJustify> justifyContentFunc) where T : IFlexLayout
        {
            flexlayout.JustifyContent = new PropertyValue<Microsoft.Maui.Layouts.FlexJustify>(justifyContentFunc);
            return flexlayout;
        }



        public static T AlignContent<T>(this T flexlayout, Microsoft.Maui.Layouts.FlexAlignContent alignContent) where T : IFlexLayout
        {
            flexlayout.AlignContent = new PropertyValue<Microsoft.Maui.Layouts.FlexAlignContent>(alignContent);
            return flexlayout;
        }
        public static T AlignContent<T>(this T flexlayout, Func<Microsoft.Maui.Layouts.FlexAlignContent> alignContentFunc) where T : IFlexLayout
        {
            flexlayout.AlignContent = new PropertyValue<Microsoft.Maui.Layouts.FlexAlignContent>(alignContentFunc);
            return flexlayout;
        }



        public static T AlignItems<T>(this T flexlayout, Microsoft.Maui.Layouts.FlexAlignItems alignItems) where T : IFlexLayout
        {
            flexlayout.AlignItems = new PropertyValue<Microsoft.Maui.Layouts.FlexAlignItems>(alignItems);
            return flexlayout;
        }
        public static T AlignItems<T>(this T flexlayout, Func<Microsoft.Maui.Layouts.FlexAlignItems> alignItemsFunc) where T : IFlexLayout
        {
            flexlayout.AlignItems = new PropertyValue<Microsoft.Maui.Layouts.FlexAlignItems>(alignItemsFunc);
            return flexlayout;
        }



        public static T Position<T>(this T flexlayout, Microsoft.Maui.Layouts.FlexPosition position) where T : IFlexLayout
        {
            flexlayout.Position = new PropertyValue<Microsoft.Maui.Layouts.FlexPosition>(position);
            return flexlayout;
        }
        public static T Position<T>(this T flexlayout, Func<Microsoft.Maui.Layouts.FlexPosition> positionFunc) where T : IFlexLayout
        {
            flexlayout.Position = new PropertyValue<Microsoft.Maui.Layouts.FlexPosition>(positionFunc);
            return flexlayout;
        }



        public static T Wrap<T>(this T flexlayout, Microsoft.Maui.Layouts.FlexWrap wrap) where T : IFlexLayout
        {
            flexlayout.Wrap = new PropertyValue<Microsoft.Maui.Layouts.FlexWrap>(wrap);
            return flexlayout;
        }
        public static T Wrap<T>(this T flexlayout, Func<Microsoft.Maui.Layouts.FlexWrap> wrapFunc) where T : IFlexLayout
        {
            flexlayout.Wrap = new PropertyValue<Microsoft.Maui.Layouts.FlexWrap>(wrapFunc);
            return flexlayout;
        }




    }
}
