using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class MenuFlyoutSubItem
    {
        public MenuFlyoutSubItem(string text)
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
            else if (childNativeControl is Microsoft.Maui.Controls.MenuFlyoutSeparator menuFlyoutSeparator)
            {
                NativeControl.Remove(menuFlyoutSeparator);
            }

            base.OnRemoveChild(widget, childNativeControl);
        }
    }
}
