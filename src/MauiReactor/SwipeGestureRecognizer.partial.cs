
namespace MauiReactor;

public partial class SwipeGestureRecognizer
{
    public SwipeGestureRecognizer(Action onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Action onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Action onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);

    public SwipeGestureRecognizer(Action<object?, SwipedEventArgs> onSwiped) => this.OnSwiped(onSwiped);
    public SwipeGestureRecognizer(Action<object?, SwipedEventArgs> onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);
    public SwipeGestureRecognizer(Action<object?, SwipedEventArgs> onSwiped, SwipeDirection direction, uint threshold) => this.OnSwiped(onSwiped).Direction(direction).Threshold(threshold);

}
