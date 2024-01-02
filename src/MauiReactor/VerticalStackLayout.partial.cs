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
    public static VStack VStack(params VisualNode?[] children)
    {
        var vstack = GetNodeFromPool<VStack>();
        vstack.AddChildren(children);
        return vstack;
    }
    public static VStack VStack(double spacing, params VisualNode?[] children) =>
        VStack(children)
            .Spacing(spacing);

}
