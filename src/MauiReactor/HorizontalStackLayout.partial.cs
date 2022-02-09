namespace MauiReactor
{
    public partial class HorizontalStackLayout
    {
        public HorizontalStackLayout(double spacing) => this.Spacing(spacing);
    }

    public class HStack : HorizontalStackLayout
    {
        public HStack(double spacing) => this.Spacing(spacing);
    }
}
