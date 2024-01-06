//using MauiReactor.Shapes;

namespace MauiReactor;

public partial class ImageButton<T>
{
    partial void OnReset()
    {
        var thisAsIImageButton = (IImageButton)this;
        System.Diagnostics.Debug.Assert(thisAsIImageButton.ClickedAction == null);
        System.Diagnostics.Debug.Assert(thisAsIImageButton.ClickedActionWithArgs == null);
    }
}

public partial class ImageButton
{
    public ImageButton(string imageSource) => this.Source(imageSource);
    public ImageButton(Uri imageSource) => this.Source(imageSource);

    
}

public partial class Component
{
    public static ImageButton ImageButton(string imageSource) =>
        GetNodeFromPool<ImageButton>().Source(imageSource);

    public static ImageButton ImageButton(Uri imageSource) =>
        GetNodeFromPool<ImageButton>().Source(imageSource);
}