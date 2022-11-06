using MauiReactor.Internals;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{
    public partial interface IPointInterationHandler : ICanvasNode
    {
        PropertyValue<bool>? IsEnabled { get; set; }
        Action? TapAction { get; set; }
    }

    public partial class PointInterationHandler<T> : CanvasNode<T>, IPointInterationHandler, IEnumerable where T : Internals.PointInterationHandler, new()
    {
        public PointInterationHandler()
        {

        }

        public PointInterationHandler(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Action? IPointInterationHandler.TapAction { get; set; }


        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        protected override void OnAttachNativeEvents()
        {
            var thisAsIPointIterationHandler = (IPointInterationHandler)this;

            if (thisAsIPointIterationHandler.TapAction != null)
            {
                Validate.EnsureNotNull(NativeControl);

                NativeControl.Tap += NativeControl_Tap;
            }


            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tap(object? sender, EventArgs e)
        {
            var thisAsIPointIterationHandler = (IPointInterationHandler)this;
            thisAsIPointIterationHandler.TapAction?.Invoke();
        }

        protected override void OnDetachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Tap -= NativeControl_Tap;

            base.OnDetachNativeEvents();
        }

        PropertyValue<bool>? IPointInterationHandler.IsEnabled { get; set; }

        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPointIterationHandler = (IPointInterationHandler)this;

            SetPropertyValue(NativeControl, Internals.PointInterationHandler.IsEnabledProperty, thisAsIPointIterationHandler.IsEnabled);

            base.OnUpdate();
        }
    }

    public partial class PointInterationHandler : PointInterationHandler<Internals.PointInterationHandler>
    {
        public PointInterationHandler()
        {

        }

        public PointInterationHandler(Action<Internals.PointInterationHandler?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class PointInterationHandlerExtensions
    {

        public static T IsEnabled<T>(this T node, bool value) where T : IPointInterationHandler
        {
            node.IsEnabled = new PropertyValue<bool>(value);
            return node;
        }

        public static T IsEnabled<T>(this T node, Func<bool> valueFunc) where T : IPointInterationHandler
        {
            node.IsEnabled = new PropertyValue<bool>(valueFunc);
            return node;
        }       
        
        public static T OnTap<T>(this T node, Action? action) where T : IPointInterationHandler
        {
            node.TapAction = action;
            return node;
        }

    }

}
