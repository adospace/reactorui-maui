using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class FormattedString<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.Span span)
        {
            NativeControl.Spans.Add(span);
        }


        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.Span span)
        {
            NativeControl.Spans.Remove(span);
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}
