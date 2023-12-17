namespace MauiReactor;

public partial class FlyoutItem
{
    public FlyoutItem(string title) => this.Title(title);

    public FlyoutItem(string title, string icon) => this.Title(title).Icon(icon);
}


public partial class Component
{
    public FlyoutItem FlyoutItem(string title) => GetNodeFromPool<FlyoutItem>().Title(title);
    
    public FlyoutItem FlyoutItem(string title, string icon) => GetNodeFromPool<FlyoutItem>().Title(title).Icon(icon);
}
