using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor.Compatibility
{
    public partial interface ILayout
    {
        bool IsClippedToBounds { get; set; }
        bool CascadeInputTransparent { get; set; }
        Microsoft.Maui.Thickness Padding { get; set; }

        Action? LayoutChangedAction { get; set; }
        Action<EventArgs>? LayoutChangedActionWithArgs { get; set; }

    }
    public abstract partial class Layout<T> : View<T>, ILayout where T : Microsoft.Maui.Controls.Compatibility.Layout, new()
    {
        protected Layout()
        {

        }

        protected Layout(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        bool ILayout.IsClippedToBounds { get; set; } = (bool)Microsoft.Maui.Controls.Compatibility.Layout.IsClippedToBoundsProperty.DefaultValue;
        bool ILayout.CascadeInputTransparent { get; set; } = (bool)Microsoft.Maui.Controls.Compatibility.Layout.CascadeInputTransparentProperty.DefaultValue;
        Microsoft.Maui.Thickness ILayout.Padding { get; set; } = (Microsoft.Maui.Thickness)Microsoft.Maui.Controls.Compatibility.Layout.PaddingProperty.DefaultValue;

        Action? ILayout.LayoutChangedAction { get; set; }
        Action<EventArgs>? ILayout.LayoutChangedActionWithArgs { get; set; }

        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsILayout = (ILayout)this;
            if (NativeControl.IsClippedToBounds != thisAsILayout.IsClippedToBounds) NativeControl.IsClippedToBounds = thisAsILayout.IsClippedToBounds;
            if (NativeControl.CascadeInputTransparent != thisAsILayout.CascadeInputTransparent) NativeControl.CascadeInputTransparent = thisAsILayout.CascadeInputTransparent;
            if (NativeControl.Padding != thisAsILayout.Padding) NativeControl.Padding = thisAsILayout.Padding;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();

        protected override void OnAttachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsILayout = (ILayout)this;
            if (thisAsILayout.LayoutChangedAction != null || thisAsILayout.LayoutChangedActionWithArgs != null)
            {
                NativeControl.LayoutChanged += NativeControl_LayoutChanged;
            }

            base.OnAttachNativeEvents();
        }

        private void NativeControl_LayoutChanged(object? sender, EventArgs e)
        {
            var thisAsILayout = (ILayout)this;
            thisAsILayout.LayoutChangedAction?.Invoke();
            thisAsILayout.LayoutChangedActionWithArgs?.Invoke(e);
        }

        protected override void OnDetachNativeEvents()
        {
            if (NativeControl != null)
            {
                NativeControl.LayoutChanged -= NativeControl_LayoutChanged;
            }

            base.OnDetachNativeEvents();
        }

    }


    public static partial class LayoutExtensions
    {
        public static T IsClippedToBounds<T>(this T layout, bool isClippedToBounds) where T : ILayout
        {
            layout.IsClippedToBounds = isClippedToBounds;
            return layout;
        }

        public static T CascadeInputTransparent<T>(this T layout, bool cascadeInputTransparent) where T : ILayout
        {
            layout.CascadeInputTransparent = cascadeInputTransparent;
            return layout;
        }

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


        public static T OnLayoutChanged<T>(this T layout, Action layoutchangedAction) where T : ILayout
        {
            layout.LayoutChangedAction = layoutchangedAction;
            return layout;
        }

        public static T OnLayoutChanged<T>(this T layout, Action<EventArgs> layoutchangedActionWithArgs) where T : ILayout
        {
            layout.LayoutChangedActionWithArgs = layoutchangedActionWithArgs;
            return layout;
        }
    }
}
