using MauiReactor.Internals;

namespace MauiReactor;

public abstract partial class Layout<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.IView control)
        {
            //NOTE: Even if not advisable a component author could decide to returns
            //      null in the OnRender() overload.
            //      In that case the number of children in the NativeControl couldn't
            //      match the list of children in the visual tree.
            //      Let's tolerate it just appending the controls at the end of the list.
            //      OnRemoveChild() belowe soens't use the ChildIndex so no problem
            if (widget.ChildIndex < NativeControl.Children.Count)
            {
                NativeControl.Children.Insert(widget.ChildIndex, control);
            }
            else
            {
                NativeControl.Children.Add(control);
            }
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnMoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.IView control)
        {
            var targetIndex = widget.ChildIndex;

            // Fast path: already where it belongs. Reordering a list usually moves a few
            // items and leaves the rest in place, and this keeps those at O(1) instead of
            // an IndexOf scan each.
            if (targetIndex >= 0 &&
                targetIndex < NativeControl.Children.Count &&
                ReferenceEquals(NativeControl.Children[targetIndex], control))
            {
                base.OnMoveChild(widget, childControl);
                return;
            }

            var currentIndex = NativeControl.Children.IndexOf(control);

            if (currentIndex >= 0 &&
                targetIndex >= 0 &&
                targetIndex != currentIndex &&
                targetIndex < NativeControl.Children.Count)
            {
                NativeControl.Children.RemoveAt(currentIndex);
                NativeControl.Children.Insert(targetIndex, control);
            }
        }

        base.OnMoveChild(widget, childControl);
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
