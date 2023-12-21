namespace MauiReactor;

public partial class Label
{
    public Label(object? text) => this.Text(text?.ToString() ?? string.Empty);

    public Label(Func<string> textFunc) => this.Text(textFunc);
}

public partial class Component
{
    public static Label Label(object? text) =>
        GetNodeFromPool<Label>().Text(text?.ToString() ?? string.Empty);

    public static Label Label(object? text, params VisualNode[] children)
        => Label(children).Text(text?.ToString() ?? string.Empty);

    public static Label Label(Func<string> textFunc) =>
        GetNodeFromPool<Label>().Text(textFunc);
}
