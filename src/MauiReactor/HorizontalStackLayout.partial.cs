namespace MauiReactor;

public partial class HorizontalStackLayout
{
    public HorizontalStackLayout(double spacing) => this.Spacing(spacing);
}

public class HStack : HorizontalStackLayout
{
    public HStack(double spacing) => this.Spacing(spacing);

    public HStack() { }
}

public partial class Component
{

    public static HStack HStack(params VisualNode?[] children)
    {
        var hstack = GetNodeFromPool<HStack>();
        hstack.AddChildren(children);
        return hstack;
    }
    public static HStack HStack(double spacing, params VisualNode?[] children) 
        => HStack(children)
            .Spacing(spacing);
}
