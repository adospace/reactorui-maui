using Microsoft.Maui.Controls;

namespace MauiReactor;

public partial class Image
{
    public Image(string imageSource) => this.Source(imageSource);
    public Image(Uri imageSource) => this.Source(imageSource);
}

public partial class Component
{
    public Image Image(string imageSource) =>
        GetNodeFromPool<Image>().Source(imageSource);

    public Image Image(Uri imageSource) =>
        GetNodeFromPool<Image>().Source(imageSource);
}
