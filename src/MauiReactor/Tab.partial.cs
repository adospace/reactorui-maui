namespace MauiReactor
{
    public partial class Tab
    {
        public Tab(string title) => this.Title(title);

        public Tab(string title, string icon) => this.Title(title).Icon(icon);
    }
}
