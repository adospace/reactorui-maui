
namespace MauiReactor;

public partial class SwipeGestureRecognizer
{
    public SwipeGestureRecognizer(Action onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Action onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Action onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);

    public SwipeGestureRecognizer(Action<SwipedEventArgs> onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Action<SwipedEventArgs> onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Action<SwipedEventArgs> onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);

    public SwipeGestureRecognizer(Action<object?, SwipedEventArgs> onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Action<object?, SwipedEventArgs> onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Action<object?, SwipedEventArgs> onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);


    public SwipeGestureRecognizer(Func<Task> onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Func<Task> onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Func<Task> onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);

    public SwipeGestureRecognizer(Func<SwipedEventArgs, Task> onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Func<SwipedEventArgs, Task> onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Func<SwipedEventArgs, Task> onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);

    public SwipeGestureRecognizer(Func<object?, SwipedEventArgs, Task> onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Func<object?, SwipedEventArgs, Task> onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Func<object?, SwipedEventArgs, Task> onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);



}
