
namespace MauiReactor
{
    public partial class SwipeGestureRecognizer
    {
        public SwipeGestureRecognizer(Action onSwiped) => this.OnSwiped(onSwiped);
        public SwipeGestureRecognizer(Action onSwiped, SwipeDirection direction) => this.OnSwiped(onSwiped).Direction(direction);

        public SwipeGestureRecognizer(Action<object?, EventArgs> onSwiped) => this.OnSwiped(onSwiped);
        public SwipeGestureRecognizer(Action<object?, EventArgs> onTap, SwipeDirection direction) => this.OnSwiped(onTap).Direction(direction);

    }
}
