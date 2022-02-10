using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial class ShellContent
    {
        public ShellContent(string title)
            => this.Title(title);

        public ShellContent(VisualNode content)
        {
            _internalChildren.Add(content);
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.Page page)
                NativeControl.Content = page;

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (NativeControl.Content == childControl)
                NativeControl.Content = null;

            base.OnRemoveChild(widget, childControl);
        }

    }

}
