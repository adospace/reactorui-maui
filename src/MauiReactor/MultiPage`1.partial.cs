using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor;

public abstract partial class MultiPage<T, TChild> : Page<T>, IGenericMultiPage where T : Microsoft.Maui.Controls.MultiPage<TChild>, new()
    where TChild : Microsoft.Maui.Controls.Page
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is TChild page)
            NativeControl.Children.Insert(widget.ChildIndex, page);

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is TChild page)
            NativeControl.Children.Remove(page);

        base.OnRemoveChild(widget, childControl);
    }
}