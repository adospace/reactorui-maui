namespace MauiReactor;

public partial class LongPressGestureRecognizer
{
    public LongPressGestureRecognizer(Action onLongPressed) => this.OnLongPressed(onLongPressed);
    public LongPressGestureRecognizer(Action onLongPressed, int minimumPressDuration) => this.OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);
    public LongPressGestureRecognizer(Action<LongPressedEventArgs> onLongPressed) => this.OnLongPressed(onLongPressed);
    public LongPressGestureRecognizer(Action<LongPressedEventArgs> onLongPressed, int minimumPressDuration) => this.OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);

    public LongPressGestureRecognizer(Func<Task> onLongPressed) => this.OnLongPressed(onLongPressed);
    public LongPressGestureRecognizer(Func<Task> onLongPressed, int minimumPressDuration) => this.OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);
    public LongPressGestureRecognizer(Func<LongPressedEventArgs, Task> onLongPressed) => this.OnLongPressed(onLongPressed);
    public LongPressGestureRecognizer(Func<LongPressedEventArgs, Task> onLongPressed, int minimumPressDuration) => this.OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);
}

public partial class Component
{
    public static LongPressGestureRecognizer LongPressGestureRecognizer(Action onLongPressed) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Action onLongPressed, int minimumPressDuration) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Action<LongPressedEventArgs> onLongPressed) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Action<LongPressedEventArgs> onLongPressed, int minimumPressDuration) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Func<Task> onLongPressed) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Func<Task> onLongPressed, int minimumPressDuration) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Func<LongPressedEventArgs, Task> onLongPressed) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed);

    public static LongPressGestureRecognizer LongPressGestureRecognizer(Func<LongPressedEventArgs, Task> onLongPressed, int minimumPressDuration) =>
        LongPressGestureRecognizer().OnLongPressed(onLongPressed).MinimumPressDuration(minimumPressDuration);
}
