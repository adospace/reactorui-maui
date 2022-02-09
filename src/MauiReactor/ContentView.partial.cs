using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IContentView
    {

    }

    public partial class ContentView<T>
    {

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is View view)
            {
                NativeControl.Content = view;
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is View)
            {
                NativeControl.Content = null;
            }

            base.OnRemoveChild(widget, childControl);
        }

    }
}
