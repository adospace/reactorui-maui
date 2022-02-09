namespace MauiReactor
{
    public partial class Image
    {
        public Image(string imageSource) => this.Source(imageSource);
        public Image(Uri imageSource) => this.Source(imageSource);
    }
}
