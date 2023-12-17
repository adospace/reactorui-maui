//using MauiReactor.Shapes;

namespace MauiReactor;

public partial class ImageButton
{
    public ImageButton(string imageSource) => this.Source(imageSource);
    public ImageButton(Uri imageSource) => this.Source(imageSource);
}

public partial class Component
{
    public ImageButton ImageButton(string imageSource) =>
        GetNodeFromPool<ImageButton>().Source(imageSource);

    public ImageButton ImageButton(Uri imageSource) =>
        GetNodeFromPool<ImageButton>().Source(imageSource);
}