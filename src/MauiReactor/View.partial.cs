using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IView
{
    List<IGestureRecognizer>? GestureRecognizers { get; set; }
}

public abstract partial class View<T>
{
    List<IGestureRecognizer>? IView.GestureRecognizers { get; set; }

 
    protected override void OnChildAdd(VisualNode node)
    {
        if (node is IGestureRecognizer gestureRecognizer)
        {
            var thisAsIView = (IView)this;
            thisAsIView.GestureRecognizers ??= [];
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
        view.HorizontalOptions(LayoutOptions.Start);

        return view;
    }

    public static T HCenter<T>(this T view) where T : IView
    {
        view.HorizontalOptions(LayoutOptions.Center);
        return view;
    }

    public static T HEnd<T>(this T view) where T : IView
    {
        view.HorizontalOptions  (LayoutOptions.End);
        return view;
    }

    public static T HFill<T>(this T view) where T : IView
    {
        view.HorizontalOptions(LayoutOptions.Fill);
        return view;
    }

    public static T VStart<T>(this T view) where T : IView
    {
        view.VerticalOptions(LayoutOptions.Start);
        return view;
    }

    public static T VCenter<T>(this T view) where T : IView
    {
        view.VerticalOptions(LayoutOptions.Center);
        return view;
    }

    public static T VEnd<T>(this T view) where T : IView
    {
        view.VerticalOptions(LayoutOptions.End);
        return view;
    }

    public static T VFill<T>(this T view) where T : IView
    {
        view.VerticalOptions(LayoutOptions.Fill);
        return view;
    }

    public static T Center<T>(this T view) where T : IView
    {
        view.HorizontalOptions(LayoutOptions.Center);
        view.VerticalOptions(LayoutOptions.Center);
        return view;
    }

    public static T OnTapped<T>(this T view, Action? action, int numberOfTapsRequired = 1) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new TapGestureRecognizer(action, numberOfTapsRequired));
        }

        return view;
    }

    public static T OnTapped<T>(this T view, Action<EventArgs>? action, int numberOfTapsRequired = 1) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new TapGestureRecognizer(action, numberOfTapsRequired));
        }

        return view;
    }

    public static T OnTapped<T>(this T view, Action<object?, EventArgs>? action, int numberOfTapsRequired = 1) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new TapGestureRecognizer(action, numberOfTapsRequired));
        }

        return view;
    }

    public static T OnTapped<T>(this T view, Func<Task>? action, int numberOfTapsRequired = 1) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new TapGestureRecognizer(action, numberOfTapsRequired));
        }

        return view;
    }

    public static T OnTapped<T>(this T view, Func<EventArgs, Task>? action, int numberOfTapsRequired = 1) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new TapGestureRecognizer(action, numberOfTapsRequired));
        }

        return view;
    }

    public static T OnTapped<T>(this T view, Func<object?, EventArgs, Task>? action, int numberOfTapsRequired = 1) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new TapGestureRecognizer(action, numberOfTapsRequired));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<SwipedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<SwipedEventArgs>? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<SwipedEventArgs>? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<object?, SwipedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<object?, SwipedEventArgs>? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Action<object?, SwipedEventArgs>? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<Task>? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<Task>? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<SwipedEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<SwipedEventArgs, Task>? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<SwipedEventArgs, Task>? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<object?, SwipedEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<object?, SwipedEventArgs, Task>? action, SwipeDirection direction) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction));
        }

        return view;
    }

    public static T OnSwiped<T>(this T view, Func<object?, SwipedEventArgs, Task>? action, SwipeDirection direction, uint threshold) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            view.GestureRecognizers.Add(new SwipeGestureRecognizer(action, direction, threshold));
        }

        return view;
    }



    public static T OnPointerEntered<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }
    public static T OnPointerEntered<T>(this T view, Action<PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }



    public static T OnPointerEntered<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }
    public static T OnPointerEntered<T>(this T view, Func<PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Func<object?, PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }

    public static T OnPointerEntered<T>(this T view, Func<Point?,Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerEntered(action);
        }

        return view;
    }





    public static T OnPointerExited<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Action<PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Func<PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Func<object?, PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }

    public static T OnPointerExited<T>(this T view, Func<Point?, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerExited(action);
        }

        return view;
    }


    public static T OnPointerMoved<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Action<PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }


    public static T OnPointerMoved<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Func<PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Func<object?, PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }

    public static T OnPointerMoved<T>(this T view, Func<Point?, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerMoved(action);
        }

        return view;
    }


    public static T OnPointerPressed<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }

    public static T OnPointerPressed<T>(this T view, Action<PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }

    public static T OnPointerPressed<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }

    public static T OnPointerPressed<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }


    public static T OnPointerPressed<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }

    public static T OnPointerPressed<T>(this T view, Func<PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }

    public static T OnPointerPressed<T>(this T view, Func<object?, PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }

    public static T OnPointerPressed<T>(this T view, Func<Point?, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerPressed(action);
        }

        return view;
    }


    public static T OnPointerReleased<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }

    public static T OnPointerReleased<T>(this T view, Action<PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }

    public static T OnPointerReleased<T>(this T view, Action<object?, PointerEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }

    public static T OnPointerReleased<T>(this T view, Action<Point?>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }


    public static T OnPointerReleased<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }

    public static T OnPointerReleased<T>(this T view, Func<PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }

    public static T OnPointerReleased<T>(this T view, Func<object?, PointerEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }

    public static T OnPointerReleased<T>(this T view, Func<Point?, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PointerGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPointerReleased(action);
        }

        return view;
    }




    public static T OnPinchUpdated<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }
    
    public static T OnPinchUpdated<T>(this T view, Action<PinchGestureUpdatedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }

    public static T OnPinchUpdated<T>(this T view, Action<object?, PinchGestureUpdatedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }

    public static T OnPinchUpdated<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }

    public static T OnPinchUpdated<T>(this T view, Func<PinchGestureUpdatedEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }

    public static T OnPinchUpdated<T>(this T view, Func<object?, PinchGestureUpdatedEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PinchGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPinchUpdated(action);
        }

        return view;
    }


    public static T OnDropCompleted<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDropCompleted(action);
        }

        return view;
    }

    public static T OnDropCompleted<T>(this T view, Action<DropCompletedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDropCompleted(action);
        }

        return view;
    }

    public static T OnDropCompleted<T>(this T view, Action<object?, DropCompletedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDropCompleted(action);
        }

        return view;
    }



    public static T OnDragStarting<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }

    public static T OnDragStarting<T>(this T view, Action<DragStartingEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }

    public static T OnDragStarting<T>(this T view, Action<object?, DragStartingEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }


    public static T OnDragStarting<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }

    public static T OnDragStarting<T>(this T view, Func<DragStartingEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }

    public static T OnDragStarting<T>(this T view, Func<object?, DragStartingEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DragGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragStarting(action);
        }

        return view;
    }



    public static T CanDrag<T>(this T view, bool canDrag) where T : IView
    {
        view.GestureRecognizers ??= [];
        var gesture = view.GestureRecognizers
            .OfType<DragGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = []);
        }

        gesture.CanDrag(canDrag);

        return view;
    }

    public static T CanDrag<T>(this T view, Func<bool> canDragFunc) where T : IView
    {
        view.GestureRecognizers ??= [];
        var gesture = view.GestureRecognizers
            .OfType<DragGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = []);
        }

        gesture.CanDrag(canDragFunc);

        return view;
    }



    public static T OnPanUpdated<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }

    public static T OnPanUpdated<T>(this T view, Action<PanUpdatedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }

    public static T OnPanUpdated<T>(this T view, Action<object?, PanUpdatedEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }


    public static T OnPanUpdated<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }

    public static T OnPanUpdated<T>(this T view, Func<PanUpdatedEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }

    public static T OnPanUpdated<T>(this T view, Func<object?, PanUpdatedEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<PanGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnPanUpdated(action);
        }

        return view;
    }




    public static T OnDrop<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDrop(action);
        }

        return view;
    }

    public static T OnDrop<T>(this T view, Action<DropEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDrop(action);
        }

        return view;
    }

    public static T OnDrop<T>(this T view, Action<object?, DropEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDrop(action);
        }

        return view;
    }


    public static T OnDrop<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDrop(action);
        }

        return view;
    }

    public static T OnDrop<T>(this T view, Func<DropEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDrop(action);
        }

        return view;
    }

    public static T OnDrop<T>(this T view, Func<object?, DropEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDrop(action);
        }

        return view;
    }



    public static T OnDragLeave<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }

    public static T OnDragLeave<T>(this T view, Action<DragEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }

    public static T OnDragLeave<T>(this T view, Action<object?, DragEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }



    public static T OnDragLeave<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }

    public static T OnDragLeave<T>(this T view, Func<DragEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }

    public static T OnDragLeave<T>(this T view, Func<object?, DragEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragLeave(action);
        }

        return view;
    }


    public static T OnDragOver<T>(this T view, Action? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragOver(action);
        }

        return view;
    }

    public static T OnDragOver<T>(this T view, Action<DragEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragOver(action);
        }

        return view;
    }

    public static T OnDragOver<T>(this T view, Action<object?, DragEventArgs>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragOver(action);
        }

        return view;
    }



    public static T OnDragOver<T>(this T view, Func<Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragOver(action);
        }

        return view;
    }

    public static T OnDragOver<T>(this T view, Func<DragEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragOver(action);
        }

        return view;
    }

    public static T OnDragOver<T>(this T view, Func<object?, DragEventArgs, Task>? action) where T : IView
    {
        if (action != null)
        {
            view.GestureRecognizers ??= [];
            var gesture = view.GestureRecognizers
                .OfType<DropGestureRecognizer>()
                .FirstOrDefault();
            if (gesture == null)
            {
                view.GestureRecognizers.Add(gesture = []);
            }

            gesture.OnDragOver(action);
        }

        return view;
    }




    public static T AllowDrop<T>(this T view, bool allowDrop) where T : IView
    {
        view.GestureRecognizers ??= [];
        var gesture = view.GestureRecognizers
            .OfType<DropGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = []);
        }

        gesture.AllowDrop(allowDrop);
    
        return view;
    }

    public static T AllowDrop<T>(this T view, Func<bool> allowDropFunc) where T : IView
    {
        view.GestureRecognizers ??= [];
        var gesture = view.GestureRecognizers
            .OfType<DropGestureRecognizer>()
            .FirstOrDefault();
        if (gesture == null)
        {
            view.GestureRecognizers.Add(gesture = []);
        }

        gesture.AllowDrop(allowDropFunc);

        return view;
    }

}
