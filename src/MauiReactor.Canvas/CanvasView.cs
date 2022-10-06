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
                NativeControl.Children.Insert(widget.ChildIndex, node);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Internals.CanvasNode node)
            {
                NativeControl.Children.Remove(node);
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


        //public static T Drawable<T>(this T graphicsView, Microsoft.Maui.Graphics.IDrawable drawable) where T : IGraphicsView
        //{
        //    graphicsView.Drawable = new PropertyValue<Microsoft.Maui.Graphics.IDrawable>(drawable);
        //    return graphicsView;
        //}


        //public static T Drawable<T>(this T graphicsView, Func<Microsoft.Maui.Graphics.IDrawable> drawableFunc) where T : IGraphicsView
        //{
        //    graphicsView.Drawable = new PropertyValue<Microsoft.Maui.Graphics.IDrawable>(drawableFunc);
        //    return graphicsView;
        //}










        //public static T OnStartHoverInteraction<T>(this T graphicsView, Action? startHoverInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.StartHoverInteractionAction = startHoverInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnStartHoverInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? startHoverInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.StartHoverInteractionActionWithArgs = startHoverInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnMoveHoverInteraction<T>(this T graphicsView, Action? moveHoverInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.MoveHoverInteractionAction = moveHoverInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnMoveHoverInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? moveHoverInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.MoveHoverInteractionActionWithArgs = moveHoverInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnEndHoverInteraction<T>(this T graphicsView, Action? endHoverInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.EndHoverInteractionAction = endHoverInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnEndHoverInteraction<T>(this T graphicsView, Action<object?, EventArgs>? endHoverInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.EndHoverInteractionActionWithArgs = endHoverInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnStartInteraction<T>(this T graphicsView, Action? startInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.StartInteractionAction = startInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnStartInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? startInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.StartInteractionActionWithArgs = startInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnDragInteraction<T>(this T graphicsView, Action? dragInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.DragInteractionAction = dragInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnDragInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? dragInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.DragInteractionActionWithArgs = dragInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnEndInteraction<T>(this T graphicsView, Action? endInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.EndInteractionAction = endInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnEndInteraction<T>(this T graphicsView, Action<object?, TouchEventArgs>? endInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.EndInteractionActionWithArgs = endInteractionActionWithArgs;
        //    return graphicsView;
        //}

        //public static T OnCancelInteraction<T>(this T graphicsView, Action? cancelInteractionAction) where T : IGraphicsView
        //{
        //    graphicsView.CancelInteractionAction = cancelInteractionAction;
        //    return graphicsView;
        //}

        //public static T OnCancelInteraction<T>(this T graphicsView, Action<object?, EventArgs>? cancelInteractionActionWithArgs) where T : IGraphicsView
        //{
        //    graphicsView.CancelInteractionActionWithArgs = cancelInteractionActionWithArgs;
        //    return graphicsView;
        //}

    }
}
