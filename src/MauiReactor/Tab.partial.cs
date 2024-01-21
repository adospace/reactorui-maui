namespace MauiReactor;

public partial class Tab
{
    public Tab(string title) => this.Title(title);

    public Tab(string title, string icon) => this.Title(title).Icon(icon);

    public Tab(string title, Uri iconUri) => this.Title(title).Icon(iconUri);
}


public partial class Component
{
    public static Tab Tab(string title) =>
        new Tab().Title(title);

    public static Tab Tab(string title, string icon) =>
        new Tab().Title(title).Icon(icon);

    public static Tab Tab(string title, Uri iconUri) =>
        new Tab().Title(title).Icon(iconUri);
}
