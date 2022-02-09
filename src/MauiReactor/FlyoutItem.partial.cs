namespace MauiReactor
{
    public partial class FlyoutItem
    {
        public FlyoutItem(string title) => this.Title(title);

        public FlyoutItem(string title, string icon) => this.Title(title).Icon(icon);
    }
}
