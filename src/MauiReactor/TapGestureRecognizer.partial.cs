namespace MauiReactor
{
    public partial class TapGestureRecognizer
    {
        public TapGestureRecognizer(Action onTap) => this.OnTapped(onTap);
        public TapGestureRecognizer(Action onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

        public TapGestureRecognizer(Action<object?, EventArgs> onTap) => this.OnTapped(onTap);
        public TapGestureRecognizer(Action<object?, EventArgs> onTap, int numberOfTapsRequired) => this.OnTapped(onTap).NumberOfTapsRequired(numberOfTapsRequired);

    }
}
