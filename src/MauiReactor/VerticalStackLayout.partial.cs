namespace MauiReactor;

public partial class VerticalStackLayout
{
    public VerticalStackLayout(double spacing) => this.Spacing(spacing);
}

public class VStack : VerticalStackLayout
{
    public VStack(double spacing) => this.Spacing(spacing);

    public VStack()
    { }
}

public partial class Component
{
    public static VStack VStack(params IEnumerable<VisualNode?> children)
    {
        var vstack = new VStack();
        vstack.AddChildren(children);
        return vstack;
    }
    public static VStack VStack(double spacing, params IEnumerable<VisualNode?> children) =>
        VStack(children)
            .Spacing(spacing);

}
