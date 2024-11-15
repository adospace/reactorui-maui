using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class GestureElement<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.GestureRecognizer gestureRecognizer)
        {
            NativeControl.GestureRecognizers.Add(gestureRecognizer);
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.GestureRecognizer gestureRecognizer)
        {
            NativeControl.GestureRecognizers.Remove(gestureRecognizer);
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}
