namespace MauiReactor;

public partial class MenuItem
{
    public MenuItem(string text)
        => this.Text(text);
}

public partial class Component
{
    public static MenuItem MenuItem(string text) =>
        new MenuItem().Text(text);

}
