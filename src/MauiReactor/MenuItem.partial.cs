namespace MauiReactor;

public partial class MenuItem
{
    public MenuItem(string text)
        => this.Text(text);
}

public partial class Component
{
    public MenuItem MenuItem(string text) =>
        GetNodeFromPool<MenuItem>().Text(text);

}
