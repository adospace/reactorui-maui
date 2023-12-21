//using MauiReactor.Shapes;

namespace MauiReactor;

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