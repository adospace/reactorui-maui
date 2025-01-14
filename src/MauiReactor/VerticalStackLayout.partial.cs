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

    public VStack(Action<Microsoft.Maui.Controls.VerticalStackLayout?> componentRefAction, double spacing)
        : base(componentRefAction) => this.Spacing(spacing);

    public VStack(Action<Microsoft.Maui.Controls.VerticalStackLayout?> componentRefAction)
        : base(componentRefAction)
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

    public static VStack VStack(Action<Microsoft.Maui.Controls.VerticalStackLayout?> componentRefAction, params IEnumerable<VisualNode?> children)
    {
        var vstack = new VStack(componentRefAction);
        vstack.AddChildren(children);
        return vstack;
    }

    public static VStack VStack(double spacing, params IEnumerable<VisualNode?> children)
    {
        var vstack = new VStack();
        vstack.AddChildren(children);
        vstack.Spacing(spacing);
        return vstack;
    }

    public static VStack VStack(Action<Microsoft.Maui.Controls.VerticalStackLayout?> componentRefAction, double spacing, params IEnumerable<VisualNode?> children)
    {
        var vstack = new VStack(componentRefAction);
        vstack.AddChildren(children);
        vstack.Spacing(spacing);
        return vstack;
    }
}
