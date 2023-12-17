using MauiReactor.Internals;

namespace MauiReactor;

public abstract partial class Layout<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.IView control)
        {
            NativeControl.Children.Insert(widget.ChildIndex, control);
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.IView control)
        {
            NativeControl.Children.Remove(control);
        }

        base.OnRemoveChild(widget, childControl);
    }
}
