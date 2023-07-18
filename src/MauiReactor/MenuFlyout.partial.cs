using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class MenuFlyout<T> : FlyoutBase<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.MenuItem menuFlyoutItem)
        {
            NativeControl.Insert(widget.ChildIndex, menuFlyoutItem);
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.MenuItem menuFlyoutItem)
        {
            NativeControl.Remove(menuFlyoutItem);
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}
