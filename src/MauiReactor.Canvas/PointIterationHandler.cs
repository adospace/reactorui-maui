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
    public partial interface IPointIterationHandler : ICanvasNode
    {
        Action? TapAction { get; set; }
    }

    public partial class PointIterationHandler<T> : CanvasNode<T>, IPointIterationHandler, IEnumerable where T : Internals.PointIterationHandler, new()
    {
        public PointIterationHandler()
        {

        }

        public PointIterationHandler(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Action? IPointIterationHandler.TapAction { get; set; }


        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _internalChildren;
        }

        protected override void OnAttachNativeEvents()
        {
            var thisAsIPointIterationHandler = (IPointIterationHandler)this;

            if (thisAsIPointIterationHandler.TapAction != null)
            {
                Validate.EnsureNotNull(NativeControl);

                NativeControl.Tap += NativeControl_Tap;
            }


            base.OnAttachNativeEvents();
        }

        private void NativeControl_Tap(object? sender, EventArgs e)
        {
            var thisAsIPointIterationHandler = (IPointIterationHandler)this;
            thisAsIPointIterationHandler.TapAction?.Invoke();
        }

        protected override void OnDetachNativeEvents()
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Tap -= NativeControl_Tap;

            base.OnDetachNativeEvents();
        }
    }

    public partial class PointIterationHandler : PointIterationHandler<Internals.PointIterationHandler>
    {
        public PointIterationHandler()
        {

        }

        public PointIterationHandler(Action<Internals.PointIterationHandler?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class PointIterationHandlerExtensions
    {
        public static T OnTap<T>(this T node, Action? action) where T : IPointIterationHandler
        {
            node.TapAction = action;
            return node;
        }
    }

}
