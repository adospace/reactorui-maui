namespace MauiReactor;

public partial class TapGestureRecognizer
{
    public TapGestureRecognizer(Action onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Action onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public TapGestureRecognizer(Action<object?, EventArgs> onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Action<object?, EventArgs> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

}


public partial class Component
{
    public static TapGestureRecognizer TapGestureRecognizer(Action onTap) =>
        TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Action onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public static TapGestureRecognizer TapGestureRecognizer(Action<object?, EventArgs> onTap) =>
        TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Action<object?, EventArgs> onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

}
