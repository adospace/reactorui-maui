namespace MauiReactor
{
    public partial class VerticalStackLayout
    {
        public VerticalStackLayout(double spacing) => this.Spacing(spacing);
    }

    public class VStack : VerticalStackLayout
    {
        public VStack(double spacing) => this.Spacing(spacing);
    }
}
