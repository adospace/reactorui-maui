using MauiReactor.Internals;
using Microsoft.Maui.Controls;

namespace MauiReactor;

public partial class Image
{
    public Image(string imageSource) => this.Source(imageSource);
    public Image(Uri imageSource) => this.Source(imageSource);
}

public partial class Image<T>
{
    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        NativeControl.EnsureNotNull();
        if (childControl is ImageSource imageSource)
        {
            NativeControl.Source = imageSource;
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        NativeControl.EnsureNotNull();
        if (childControl is ImageSource imageSource &&
            NativeControl.Source == imageSource)
        {
            NativeControl.Source = null;
        }

        base.OnRemoveChild(widget, childControl);
    }
}

public partial class Component
{
    public static Image Image(string imageSource) =>
        new Image().Source(imageSource);

    public static Image Image(Uri imageSource) =>
        new Image().Source(imageSource);
}
