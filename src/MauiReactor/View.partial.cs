using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IView
{
    List<IGestureRecognizer>? GestureRecognizers { get; set; }
}

public abstract partial class View<T>
{
    List<IGestureRecognizer>? IView.GestureRecognizers { get; set; }

    partial void OnReset()
    {
        var thisAsIView = (IView)this;
        thisAsIView.GestureRecognizers = null;
    }

    protected override void OnChildAdd(VisualNode node)
    {
        if (node is IGestureRecognizer gestureRecognizer)
        {
            var thisAsIView = (IView)this;
            thisAsIView.GestureRecognizers ??= new List<IGestureRecognizer>();
            thisAsIView.GestureRecognizers.Add(gestureRecognizer);
            return;
        }
        base.OnChildAdd(node);
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsIView = (IView)this;

        var children = base.RenderChildren();
        
        if (thisAsIView.GestureRecognizers != null)
        {
            children = children.Concat(thisAsIView.GestureRecognizers.Cast<VisualNode>());
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.Controls.GestureRecognizer gestureRecognizer)
        {
            NativeControl.GestureRecognizers.Add(gestureRecognizer);
        }
        else if (childControl is Microsoft.Maui.Controls.MenuFlyout menuFlyout)
        {
            NativeControl.SetPropertyValue(FlyoutBase.ContextFlyoutProperty, menuFlyout);
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.Controls.GestureRecognizer gestureRecognizer)
        {
            NativeControl.GestureRecognizers.Remove(gestureRecognizer);
        }
        else if (childControl is Microsoft.Maui.Controls.MenuFlyout _)
        {
            NativeControl.SetPropertyValue(FlyoutBase.ContextFlyoutProperty, null);
        }


        base.OnRemoveChild(widget, childControl);
    }
}


public static partial class ViewExtensions
{
    public static T HStart<T>(this T view) where T : IView
    {
        view.HorizontalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Start);

        return view;
    }

    public static T HCenter<T>(this T view) where T : IView
    {
        view.HorizontalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Center);
        return view;
    }

    public static T HEnd<T>(this T view) where T : IView
    {
        view.HorizontalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.End);
        return view;
    }

    public static T HFill<T>(this T view) where T : IView
    {
        view.HorizontalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Fill);
        return view;
    }

    public static T VStart<T>(this T view) where T : IView
    {
        view.VerticalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Start);
        return view;
    }

    public static T VCenter<T>(this T view) where T : IView
    {
        view.VerticalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Center);
        return view;
    }

    public static T VEnd<T>(this T view) where T : IView
    {
        view.VerticalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.End);
        return view;
    }

    public static T VFill<T>(this T view) where T : IView
    {
        view.VerticalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Fill);
        return view;
    }

    public static T Center<T>(this T view) where T : IView
    {
        view.HorizontalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Center);
        view.VerticalOptions = new PropertyValue<Microsoft.Maui.Controls.LayoutOptions>(LayoutOptions.Center);
        return view;
    }

    public static T OnTapped<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new TapGestureRecognizer(action));
        }

        return view;
    }

    public static T OnTapped<T>(this T view, Action<object?, EventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new TapGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<object?, SwipedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<object?, SwipedEventArgs>? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<object?, SwipedEventArgs>? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PointerGestureRecognizer());
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPinchUpdated<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PinchGestureRecognizer());
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }

    public static T OnPinchUpdated<T>(this T view, Action<object?, PinchGestureUpdatedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PinchGestureRecognizer());
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }

    public static T OnDropCompleted<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DragGestureRecognizer());
            }

            gesture.OnDropCompleted(action);
        }

        return view;
    }

    public static T OnDropCompleted<T>(this T view, Action<object?, DropCompletedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DragGestureRecognizer());
            }

            gesture.OnDropCompleted(action);
        }

        return view;
    }

    public static T OnDragStarting<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DragGestureRecognizer());
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }

    public static T OnDragStarting<T>(this T view, Action<object?, DragStartingEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DragGestureRecognizer());
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }

    public static T CanDrag<T>(this T view, bool canDrag) where T : IView
    {
        view.GestureRecognizers ??= new List<IGestureRecognizer>();
        var gesture = view.GestureRecognizers
            .OfType<DragGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = new DragGestureRecognizer());
        }

        gesture.CanDrag(canDrag);

        return view;
    }

    public static T CanDrag<T>(this T view, Func<bool> canDragFunc) where T : IView
    {
        view.GestureRecognizers ??= new List<IGestureRecognizer>();
        var gesture = view.GestureRecognizers
            .OfType<DragGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = new DragGestureRecognizer());
        }

        gesture.CanDrag(canDragFunc);

        return view;
    }

    public static T OnPanUpdated<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PanGestureRecognizer());
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }

    public static T OnPanUpdated<T>(this T view, Action<object?, PanUpdatedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new PanGestureRecognizer());
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }

    public static T OnDrop<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
            }

            gesture.OnDrop(action);
        }

        return view;
    }

    public static T OnDrop<T>(this T view, Action<object?, DropEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
            }

            gesture.OnDrop(action);
        }

        return view;
    }

    public static T OnDragLeave<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }

    public static T OnDragLeave<T>(this T view, Action<object?, DragEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }

    public static T OnDragOver<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
            }

            gesture.OnDragOver(action);
        }

        return view;
    }

    public static T OnDragOver<T>(this T view, Action<object?, DragEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
            }

            gesture.OnDragOver(action);
        }

        return view;
    }

    public static T AllowDrop<T>(this T view, bool allowDrop) where T : IView
    {
        view.GestureRecognizers ??= new List<IGestureRecognizer>();
        var gesture = view.GestureRecognizers
            .OfType<DropGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
        }

        gesture.AllowDrop(allowDrop);
    
        return view;
    }

    public static T AllowDrop<T>(this T view, Func<bool> allowDropFunc) where T : IView
    {
        view.GestureRecognizers ??= new List<IGestureRecognizer>();
        var gesture = view.GestureRecognizers
            .OfType<DropGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = new DropGestureRecognizer());
        }

        gesture.AllowDrop(allowDropFunc);

        return view;
    }

}
