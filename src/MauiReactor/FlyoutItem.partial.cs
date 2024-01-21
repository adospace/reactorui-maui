namespace MauiReactor;

public partial class FlyoutItem
{
    public FlyoutItem(string title) => this.Title(title);

    public FlyoutItem(string title, string icon) => this.Title(title).Icon(icon);
}


public partial class Component
{
    public static FlyoutItem FlyoutItem(string title) 
        => new FlyoutItem().Title(title);

    public static FlyoutItem FlyoutItem(string title, string icon) 
        => new FlyoutItem().Title(title).Icon(icon);

    public static FlyoutItem FlyoutItem(string title, params VisualNode?[]? children)
        => FlyoutItem(children).Title(title);

    public static FlyoutItem FlyoutItem(string title, string icon, params VisualNode?[]? children)
        => FlyoutItem(children).Title(title).Icon(icon);
}
