using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class Shell<T> : IEnumerable
    {
        private readonly Dictionary<BindableObject, Microsoft.Maui.Controls.ShellItem> _elementItemMap = new();
        private readonly Dictionary<BindableObject, Microsoft.Maui.Controls.ToolbarItem> _elementToolbarItemMap = new();

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.ShellItem shellItem)
            {
                NativeControl.Items.Insert(widget.ChildIndex, shellItem);
                _elementItemMap[childControl] = shellItem;
            }
            else if (childControl is Microsoft.Maui.Controls.Page page)
            {
                var shellContentItem = new Microsoft.Maui.Controls.ShellContent() { Content = page };
                NativeControl.Items.Insert(widget.ChildIndex, shellContentItem);
                _elementItemMap[childControl] = shellContentItem;
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Add(toolbarItem);
                _elementToolbarItemMap[childControl] = toolbarItem;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (_elementItemMap.TryGetValue(childControl, out var item))
                NativeControl.Items.Remove(item);
            else if (_elementToolbarItemMap.TryGetValue(childControl, out var toolbarItem))
                NativeControl.ToolbarItems.Remove(toolbarItem);

            base.OnRemoveChild(widget, childControl);
        }
    }
}
