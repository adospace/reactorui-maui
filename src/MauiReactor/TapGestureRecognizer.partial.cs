namespace MauiReactor;

public partial class TapGestureRecognizer
{
    public TapGestureRecognizer(Action onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Action onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);
    public TapGestureRecognizer(Action<EventArgs> onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Action<EventArgs> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public TapGestureRecognizer(Action<object?, EventArgs> onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Action<object?, EventArgs> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);
    
    public TapGestureRecognizer(Func<Task> onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Func<Task> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);
    public TapGestureRecognizer(Func<EventArgs, Task> onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Func<EventArgs, Task> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public TapGestureRecognizer(Func<object?, EventArgs, Task> onTap) => this.OnTapped(onTap);
    public TapGestureRecognizer(Func<object?, EventArgs, Task> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);


}


public partial class Component
{
    public static TapGestureRecognizer TapGestureRecognizer(Action onTap) =>
        TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Action onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);
    public static TapGestureRecognizer TapGestureRecognizer(Action<EventArgs> onTap) =>
    TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Action<EventArgs> onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public static TapGestureRecognizer TapGestureRecognizer(Action<object?, EventArgs> onTap) =>
        TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Action<object?, EventArgs> onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);


    public static TapGestureRecognizer TapGestureRecognizer(Func<Task> onTap) =>
        TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Func<Task> onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);
    public static TapGestureRecognizer TapGestureRecognizer(Func<EventArgs, Task> onTap) =>
    TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Func<EventArgs, Task> onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    public static TapGestureRecognizer TapGestureRecognizer(Func<object?, EventArgs, Task> onTap) =>
        TapGestureRecognizer().OnTapped(onTap);

    public static TapGestureRecognizer TapGestureRecognizer(Func<object?, EventArgs, Task> onTap, int numberOfTapsRequired) =>
        TapGestureRecognizer().OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

}
