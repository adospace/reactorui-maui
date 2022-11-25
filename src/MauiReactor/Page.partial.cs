using MauiReactor.Internals;

namespace MauiReactor
{
    public partial class Page<T>
    {
        public Page(string title) => this.Title(title);

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.MenuBarItem menuBarItem)
            {
                NativeControl.MenuBarItems.Insert(widget.ChildIndex, menuBarItem);
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Insert(widget.ChildIndex, toolbarItem);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.MenuBarItem menuBarItem)
            {
                NativeControl.MenuBarItems.Remove(menuBarItem);
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Remove(toolbarItem);
            }


            base.OnRemoveChild(widget, childControl);
        }

    }
}
