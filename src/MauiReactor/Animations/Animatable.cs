namespace MauiReactor.Animations;

public class Animatable
{
    private readonly Func<IVisualNode, RxAnimation, object?> _animateAction;

    public Animatable(object key, RxAnimation animation, Func<IVisualNode, RxAnimation, object?> action)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Animation = animation ?? throw new ArgumentNullException(nameof(animation));
        _animateAction = action ?? throw new ArgumentNullException(nameof(action));
    }

    public RxAnimation Animation { get; }
    public bool? IsEnabled { get; internal set; }
    public object Key { get; }

    internal object? Animate(IVisualNode visualNode)
    {
        return _animateAction(visualNode, Animation);
    }
}