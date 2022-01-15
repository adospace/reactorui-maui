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
    public partial interface IScrollView
    {
        Microsoft.Maui.ScrollOrientation Orientation { get; set; }
        Microsoft.Maui.ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        Microsoft.Maui.ScrollBarVisibility VerticalScrollBarVisibility { get; set; }

        Action? ScrolledAction { get; set; }
        Action<ScrolledEventArgs>? ScrolledActionWithArgs { get; set; }

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

        Microsoft.Maui.ScrollOrientation IScrollView.Orientation { get; set; } = (Microsoft.Maui.ScrollOrientation)Microsoft.Maui.Controls.ScrollView.OrientationProperty.DefaultValue;
        Microsoft.Maui.ScrollBarVisibility IScrollView.HorizontalScrollBarVisibility { get; set; } = (Microsoft.Maui.ScrollBarVisibility)Microsoft.Maui.Controls.ScrollView.HorizontalScrollBarVisibilityProperty.DefaultValue;
        Microsoft.Maui.ScrollBarVisibility IScrollView.VerticalScrollBarVisibility { get; set; } = (Microsoft.Maui.ScrollBarVisibility)Microsoft.Maui.Controls.ScrollView.VerticalScrollBarVisibilityProperty.DefaultValue;

        Action? IScrollView.ScrolledAction { get; set; }
        Action<ScrolledEventArgs>? IScrollView.ScrolledActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIScrollView = (IScrollView)this;
            if (NativeControl.Orientation != thisAsIScrollView.Orientation) NativeControl.Orientation = thisAsIScrollView.Orientation;
            if (NativeControl.HorizontalScrollBarVisibility != thisAsIScrollView.HorizontalScrollBarVisibility) NativeControl.HorizontalScrollBarVisibility = thisAsIScrollView.HorizontalScrollBarVisibility;
            if (NativeControl.VerticalScrollBarVisibility != thisAsIScrollView.VerticalScrollBarVisibility) NativeControl.VerticalScrollBarVisibility = thisAsIScrollView.VerticalScrollBarVisibility;


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
            thisAsIScrollView.ScrolledActionWithArgs?.Invoke(e);
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
        public static T Orientation<T>(this T scrollview, Microsoft.Maui.ScrollOrientation orientation) where T : IScrollView
        {
            scrollview.Orientation = orientation;
            return scrollview;
        }

        public static T HorizontalScrollBarVisibility<T>(this T scrollview, Microsoft.Maui.ScrollBarVisibility horizontalScrollBarVisibility) where T : IScrollView
        {
            scrollview.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
            return scrollview;
        }

        public static T VerticalScrollBarVisibility<T>(this T scrollview, Microsoft.Maui.ScrollBarVisibility verticalScrollBarVisibility) where T : IScrollView
        {
            scrollview.VerticalScrollBarVisibility = verticalScrollBarVisibility;
            return scrollview;
        }


        public static T OnScrolled<T>(this T scrollview, Action scrolledAction) where T : IScrollView
        {
            scrollview.ScrolledAction = scrolledAction;
            return scrollview;
        }

        public static T OnScrolled<T>(this T scrollview, Action<ScrolledEventArgs> scrolledActionWithArgs) where T : IScrollView
        {
            scrollview.ScrolledActionWithArgs = scrolledActionWithArgs;
            return scrollview;
        }
    }
}
