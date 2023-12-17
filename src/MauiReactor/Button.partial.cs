namespace MauiReactor;

public partial class Button
{
    public Button(string text) => this.Text(text);

    public Button(string text, Action onClick) => this.Text(text).OnClicked(onClick);
}

public partial class Component
{
    public Button Button(string text) => GetNodeFromPool<Button>().Text(text);

    public Button Button(string text, Action onClick) => GetNodeFromPool<Button>().Text(text).OnClicked(onClick);
}