namespace MauiReactor.Animations;

public class Animatable
{
    public Animatable(object key, RxAnimation animation)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Animation = animation ?? throw new ArgumentNullException(nameof(animation));
    }

    public RxAnimation Animation { get; }
    public bool? IsEnabled { get; internal set; }
    public object Key { get; }
}