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
    public TapGestureRecognizer TapGestureRecognizer(Action onTap) =>
        GetNodeFromPool<TapGestureRecognizer>().OnTapped(onTap);

    public TapGestureRecognizer TapGestureRecognizer(Action onTap, int numberOfTapsRequired) =>
        GetNodeFromPool<TapGestureRecognizer>().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public TapGestureRecognizer TapGestureRecognizer(Action<object?, EventArgs> onTap) =>
        GetNodeFromPool<TapGestureRecognizer>().OnTapped(onTap);

    public TapGestureRecognizer TapGestureRecognizer(Action<object?, EventArgs> onTap, int numberOfTapsRequired) =>
        GetNodeFromPool<TapGestureRecognizer>().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

}
