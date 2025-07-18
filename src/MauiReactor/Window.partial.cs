﻿using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class Window
{
    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        if (childNativeControl is Microsoft.Maui.Controls.Page page)
        {
            Validate.EnsureNotNull(NativeControl);
            NativeControl.Page = page;
        }
        else if (childNativeControl is Microsoft.Maui.Controls.TitleBar)
        {
            Validate.EnsureNotNull(NativeControl);
            NativeControl.TitleBar = childNativeControl as Microsoft.Maui.Controls.TitleBar;
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        

        base.OnRemoveChild(widget, childNativeControl);
    }
}
