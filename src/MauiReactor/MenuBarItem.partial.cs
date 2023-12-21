using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class MenuBarItem
{
    public MenuBarItem(string text)
    {
        this.Text(text);
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.MenuFlyoutItem menuFlyoutItem)
        {
            NativeControl.Insert(widget.ChildIndex, menuFlyoutItem);
        }

        base.OnAddChild(widget, childNativeControl);
    }


    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is Microsoft.Maui.Controls.MenuFlyoutItem menuFlyoutItem)
        {
            NativeControl.Remove(menuFlyoutItem);
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}


public partial class Component
{
    public static MenuBarItem MenuBarItem(string text) =>
        GetNodeFromPool<MenuBarItem>().Text(text);

}