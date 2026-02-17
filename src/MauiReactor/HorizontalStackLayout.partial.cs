namespace MauiReactor;

public partial class HorizontalStackLayout
{
    public HorizontalStackLayout(double spacing) => this.Spacing(spacing);
}

public class HStack : HorizontalStackLayout
{
    public HStack() { }

    public HStack(double spacing)
        => this.Spacing(spacing);

    public HStack(Action<Microsoft.Maui.Controls.HorizontalStackLayout?> componentRefAction, double spacing)
        : base(componentRefAction)
        => this.Spacing(spacing);

    public HStack(Action<Microsoft.Maui.Controls.HorizontalStackLayout?> componentRefAction)
        :base(componentRefAction)
    { }
}

public partial class Component
{
    public static HStack HStack(params IEnumerable<VisualNode?> children)
    {
        var hstack = new HStack();
        hstack.AddChildren(children);
        return hstack;
    }

    public static HStack HStack(double spacing, params IEnumerable<VisualNode?> children)
    {
        var hstack = new HStack();
        hstack.AddChildren(children);
        hstack.Spacing(spacing);
        return hstack;
    }

    public static HStack HStack(Action<Microsoft.Maui.Controls.HorizontalStackLayout?> componentRefAction, params IEnumerable<VisualNode?> children)
    {
        var hstack = new HStack(componentRefAction);
        hstack.AddChildren(children);
        return hstack;
    }

    public static HStack HStack(Action<Microsoft.Maui.Controls.HorizontalStackLayout?> componentRefAction, double spacing, params IEnumerable<VisualNode?> children)
    {
        var hstack = new HStack(componentRefAction);
        hstack.AddChildren(children);
        hstack.Spacing(spacing);
        return hstack;
    }
}
