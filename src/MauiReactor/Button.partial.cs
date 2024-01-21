namespace MauiReactor;

public partial class Button
{
    public Button(string text) => this.Text(text);

    public Button(string text, Action onClick) => this.Text(text).OnClicked(onClick);
}

public partial class Component
{
    public static Button Button(string text) 
        => new Button().Text(text);

    public static Button Button(string text, Action onClick) 
        => new Button().Text(text).OnClicked(onClick);
}