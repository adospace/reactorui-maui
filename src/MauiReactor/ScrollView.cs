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
    public partial interface IScrollView : Compatibility.ILayout
    {
        PropertyValue<Microsoft.Maui.ScrollOrientation>? Orientation { get; set; }
        PropertyValue<Microsoft.Maui.ScrollBarVisibility>? HorizontalScrollBarVisibility { get; set; }
        PropertyValue<Microsoft.Maui.ScrollBarVisibility>? VerticalScrollBarVisibility { get; set; }

        Action? ScrolledAction { get; set; }
        Action<object?, ScrolledEventArgs>? ScrolledActionWithArgs { get; set; }

    }
    public partial class ScrollView<T> : Compatibility.Layout<T>, IScrollView where T : Microsoft.Maui.Controls.ScrollView, new()
    {
        public ScrollView()
        {

        }

        public ScrollView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.ScrollOrientation>? IScrollView.Orientation { get; set; }
        PropertyValue<Microsoft.Maui.ScrollBarVisibility>? IScrollView.HorizontalScrollBarVisibility { get; set; }
        PropertyValue<Microsoft.Maui.ScrollBarVisibility>? IScrollView.VerticalScrollBarVisibility { get; set; }

        Action? IScrollView.ScrolledAction { get; set; }
        Action<object?, ScrolledEventArgs>? IScrollView.ScrolledActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIScrollView = (IScrollView)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ScrollView.OrientationProperty, thisAsIScrollView.Orientation);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ScrollView.HorizontalScrollBarVisibilityProperty, thisAsIScrollView.HorizontalScrollBarVisibility);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ScrollView.VerticalScrollBarVisibilityProperty, thisAsIScrollView.VerticalScrollBarVisibility);


            base.OnUpdate();

            OnEndUpdate();
        }


        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIScrollView = (IScrollView)this;
            if (thisAsIScrollView.ScrolledAction != null || thisAsIScrollView.ScrolledActionWithArgs != null)
            {
                NativeControl.Scrolled += NativeControl_Scrolled;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_Scrolled(object? sender, ScrolledEventArgs e)
        {
            var thisAsIScrollView = (IScrollView)this;
            thisAsIScrollView.ScrolledAction?.Invoke();
            thisAsIScrollView.ScrolledActionWithArgs?.Invoke(sender, e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.Scrolled -= NativeControl_Scrolled;
            }

            base.OnDetachNativeEvents();
        }

    }

    public partial class ScrollView : ScrollView<Microsoft.Maui.Controls.ScrollView>
    {
        public ScrollView()
        {

        }

        public ScrollView(Action<Microsoft.Maui.Controls.ScrollView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ScrollViewExtensions
    {
        public static T Orientation<T>(this T scrollView, Microsoft.Maui.ScrollOrientation orientation) where T : IScrollView
        {
            scrollView.Orientation = new PropertyValue<Microsoft.Maui.ScrollOrientation>(orientation);
            return scrollView;
        }

        public static T Orientation<T>(this T scrollView, Func<Microsoft.Maui.ScrollOrientation> orientationFunc) where T : IScrollView
        {
            scrollView.Orientation = new PropertyValue<Microsoft.Maui.ScrollOrientation>(orientationFunc);
            return scrollView;
        }



        public static T HorizontalScrollBarVisibility<T>(this T scrollView, Microsoft.Maui.ScrollBarVisibility horizontalScrollBarVisibility) where T : IScrollView
        {
            scrollView.HorizontalScrollBarVisibility = new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(horizontalScrollBarVisibility);
            return scrollView;
        }

        public static T HorizontalScrollBarVisibility<T>(this T scrollView, Func<Microsoft.Maui.ScrollBarVisibility> horizontalScrollBarVisibilityFunc) where T : IScrollView
        {
            scrollView.HorizontalScrollBarVisibility = new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(horizontalScrollBarVisibilityFunc);
            return scrollView;
        }



        public static T VerticalScrollBarVisibility<T>(this T scrollView, Microsoft.Maui.ScrollBarVisibility verticalScrollBarVisibility) where T : IScrollView
        {
            scrollView.VerticalScrollBarVisibility = new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(verticalScrollBarVisibility);
            return scrollView;
        }

        public static T VerticalScrollBarVisibility<T>(this T scrollView, Func<Microsoft.Maui.ScrollBarVisibility> verticalScrollBarVisibilityFunc) where T : IScrollView
        {
            scrollView.VerticalScrollBarVisibility = new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(verticalScrollBarVisibilityFunc);
            return scrollView;
        }




        public static T OnScrolled<T>(this T scrollView, Action scrolledAction) where T : IScrollView
        {
            scrollView.ScrolledAction = scrolledAction;
            return scrollView;
        }

        public static T OnScrolled<T>(this T scrollView, Action<object?, ScrolledEventArgs> scrolledActionWithArgs) where T : IScrollView
        {
            scrollView.ScrolledActionWithArgs = scrolledActionWithArgs;
            return scrollView;
        }
    }
}
