namespace MauiReactor.Animations;

public class Animatable
{
    private readonly Action<RxAnimation> _animateAction;

    public Animatable(object key, RxAnimation animation, Action<RxAnimation> action)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Animation = animation ?? throw new ArgumentNullException(nameof(animation));
        _animateAction = action ?? throw new ArgumentNullException(nameof(action));
    }

    public RxAnimation Animation { get; }
    public bool? IsEnabled { get; internal set; }
    public object Key { get; }

    internal void Animate()
    {
        _animateAction(Animation);
    }
}