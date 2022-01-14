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
        private readonly List<VisualNode> _contents = new();
        private readonly Dictionary<BindableObject, Microsoft.Maui.Controls.ShellItem> _elementItemMap = new();

        public void Add(VisualNode child)
        {
            _contents.Add(child);
        }

        public IEnumerator<VisualNode> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.ShellItem shellItem)
            {
                NativeControl.Items.Insert(widget.ChildIndex, shellItem);
            }
            else if (childControl is Microsoft.Maui.Controls.Page page)
            {
                NativeControl.Items.Insert(widget.ChildIndex, new Microsoft.Maui.Controls.ShellContent() { Content = page });
            }
            else if (childControl is Microsoft.Maui.Controls.ToolbarItem toolbarItem)
            {
                NativeControl.ToolbarItems.Add(toolbarItem);
            }

            _elementItemMap[childControl] = NativeControl.Items[NativeControl.Items.Count - 1];

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            NativeControl.Items.Remove(_elementItemMap[childControl]);

            base.OnRemoveChild(widget, childControl);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            return _contents;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }
}
