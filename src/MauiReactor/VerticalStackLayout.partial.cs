namespace MauiReactor
{
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
        public VStack VStack() => 
            GetNodeFromPool<VStack>();

        public VStack VStack(double spacing) =>
            GetNodeFromPool<VStack>()
                .Spacing(spacing);

        public VStack VStack(IEnumerable<VisualNode> children)
        {
            var vstack = GetNodeFromPool<VStack>();
            vstack.AddChildren(children);
            return vstack;
        }
    }
}
