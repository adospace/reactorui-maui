using MauiReactor.Internals;

namespace MauiReactor;

public partial class Label
{
    public Label(object? text) => this.Text(text?.ToString() ?? string.Empty);

    public Label(Func<string> textFunc) => this.Text(textFunc);
}

public partial class Label<T>
{

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is FormattedString formattedString)
        {
            NativeControl.FormattedText = formattedString;
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is FormattedString formattedString &&
            NativeControl.FormattedText == formattedString)
        {
            NativeControl.FormattedText = null;
        }

        base.OnRemoveChild(widget, childControl);
    }

}

public partial class Component
{
    public static Label Label(object? text) =>
        new Label().Text(text?.ToString() ?? string.Empty);

    public static Label Label(object? text, params VisualNode?[]? children)
        => Label(children).Text(text?.ToString() ?? string.Empty);

    public static Label Label(Func<string> textFunc) =>
        new Label().Text(textFunc);
}
