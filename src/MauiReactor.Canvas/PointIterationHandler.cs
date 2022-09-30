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
        protected readonly List<VisualNode> _internalChildren = new();

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


        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalChildren.GetEnumerator();
        }

        public void Add(params VisualNode?[]? animations)
        {
            if (animations is null)
            {
                return;
            }

            foreach (var node in animations)
            {
                if (node != null)
                {
                    _internalChildren.Add(node);
                }
            }
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Child = node;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node &&
                node == NativeControl.Child)
            {
                NativeControl.Child = null;
            }

            base.OnRemoveChild(widget, childControl);
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
