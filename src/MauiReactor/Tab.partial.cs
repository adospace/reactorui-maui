namespace MauiReactor;

public partial class Tab
{
    public Tab(string title) => this.Title(title);

    public Tab(string title, string icon) => this.Title(title).Icon(icon);

    public Tab(string title, Uri iconUri) => this.Title(title).Icon(iconUri);
}


public partial class Component
{
    public Tab Tab(string title) =>
        GetNodeFromPool<Tab>().Title(title);

    public Tab Tab(string title, string icon) =>
        GetNodeFromPool<Tab>().Title(title).Icon(icon);

    public Tab Tab(string title, Uri iconUri) =>
        GetNodeFromPool<Tab>().Title(title).Icon(iconUri);
}
