using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IFlyoutPage
    {
        VisualNode? Flyout { get; set; }
    }

    public partial class FlyoutPage<T> : IEnumerable
    {
        VisualNode? IFlyoutPage.Flyout { get; set; }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            var thisAsIFlyoutPage = (IFlyoutPage)this;

            var children = base.RenderChildren();

            if (thisAsIFlyoutPage.Flyout != null)
            {
                children = children.Concat(new[] { thisAsIFlyoutPage.Flyout });
            }

            return children;
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            var thisAsIShell = (IFlyoutPage)this;

            if (childControl is Microsoft.Maui.Controls.Page page)
            {
                if (widget == thisAsIShell.Flyout)
                {
                    NativeControl.Flyout = page;
                }
                else
                {
                    NativeControl.Detail = page;
                }
            }
            else
            {
                throw new InvalidOperationException($"FlyoutPage expected type Microsoft.Maui.Controls.Page but received '{childControl.GetType()}'");
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIShell = (IFlyoutPage)this;

            if (widget == thisAsIShell.Flyout)
            {
                NativeControl.Flyout = null;
            }
            else
            {
                //Detail cannot be set to null once a value is set. (Parameter 'value')
                //NativeControl.Detail = null;
            }

            base.OnRemoveChild(widget, childControl);
        }
    }

    public partial class FlyoutPageExtensions
    {
        public static T Flyout<T>(this T shell, VisualNode flyout) where T : IFlyoutPage
        {
            shell.Flyout = flyout;
            return shell;
        }

        public static T OnIsPresentedChanged<T>(this T flyoutPage, Action<bool> isPresentedChangedAction) where T : IFlyoutPage
        {
            flyoutPage.IsPresentedChangedActionWithArgs = (sender, args) => isPresentedChangedAction(((sender as Microsoft.Maui.Controls.FlyoutPage) ?? throw new InvalidOperationException()).IsPresented);
            return flyoutPage;
        }
    }
}
