using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class SwipeItems
{

    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        if (childNativeControl is Microsoft.Maui.Controls.ISwipeItem swipeItem)
        {
            Validate.EnsureNotNull(NativeControl);
            NativeControl.Add(swipeItem);
        }
        else
        {
            throw new NotSupportedException($"Unable to add value of type '{childNativeControl.GetType()}' under {typeof(SwipeItems)}");
        }
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        if (childNativeControl is Microsoft.Maui.Controls.ISwipeItem swipeItem)
        {
            Validate.EnsureNotNull(NativeControl);
            NativeControl.Remove(swipeItem);
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}
