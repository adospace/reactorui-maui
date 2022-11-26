using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IPage
    {
        public string? WindowTitle { get; set; }
    }


    public partial class Page<T>
    {
        public Page(string title) => this.Title(title);

        string? IPage.WindowTitle { get; set; }

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

        partial void OnEndUpdate()
        {
            var thisAsIPage = (IPage)this;

            if (thisAsIPage.WindowTitle != null && Application.Current != null)
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    Validate.EnsureNotNull(NativeControl);
                    if (NativeControl.Parent is Window parentWindow)
                    {
                        parentWindow.Title = thisAsIPage.WindowTitle;
                    }
                });
            }            
        }
    }

    public partial class PageExtensions
    {
        public static T WindowTitle<T>(this T page, string title) where T : IPage
        {
            page.WindowTitle = title;
            return page;
        }

    }
}
