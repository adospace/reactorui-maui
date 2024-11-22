using MauiReactor.Internals;

namespace MauiReactor;

public partial class Button
{
    public Button(string text) => this.Text(text);

    public Button(string text, Action onClick) => this.Text(text).OnClicked(onClick);
}

public partial class Button<T>
{

}

public partial class ButtonExtensions
{

    //public static T OnClicked<T>(this T button, Func<Task>? clickedAction)
    //    where T : IButton
    //{
    //    button.ClickedAction = new AsyncEventCommand(clickedAction);
    //    return button;
    //}
}

public partial class Component
{
    public static Button Button(string text) 
        => new Button().Text(text);

    public static Button Button(string text, Action onClick) 
        => new Button().Text(text).OnClicked(onClick);
}