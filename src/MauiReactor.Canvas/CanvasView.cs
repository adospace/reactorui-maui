using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas
{

    public partial interface ICanvasView : IView
    {


    }


    public partial class CanvasView<T> : View<T>, ICanvasView, IEnumerable where T : Internals.CanvasView, new()
    {
        public CanvasView()
        {

        }

        public CanvasView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        protected override bool SupportChildIndexing => true;

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.InsertChild(widget.ChildIndex, node);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.RemoveChild(node);
            }

            base.OnRemoveChild(widget, childControl);
        }


        protected override void OnUpdate()
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Invalidate();

            base.OnUpdate();
        }
    }


    public partial class CanvasView : CanvasView<Internals.CanvasView>
    {
        public CanvasView()
        {

        }

        public CanvasView(Action<Internals.CanvasView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }


    public static partial class GraphicsViewExtensions
    {

    }
}
