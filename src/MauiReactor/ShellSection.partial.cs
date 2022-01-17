using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class ShellSection<T> : IEnumerable
    {
        private readonly Dictionary<BindableObject, Microsoft.Maui.Controls.ShellContent> _elementItemMap = new();

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.ShellContent shellContent)
            {
                NativeControl.Items.Insert(widget.ChildIndex, shellContent);
                _elementItemMap[childControl] = shellContent;
            }
            else if (childControl is Microsoft.Maui.Controls.Page page)
            {
                var shellContentItem = new Microsoft.Maui.Controls.ShellContent() { Content = page };
                NativeControl.Items.Insert(widget.ChildIndex, shellContentItem);
                _elementItemMap[childControl] = shellContentItem;
            }
             
            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (_elementItemMap.TryGetValue(childControl, out var item))
            {
                NativeControl.Items.Remove(item);
            }

            base.OnRemoveChild(widget, childControl);
        }

    }
}
