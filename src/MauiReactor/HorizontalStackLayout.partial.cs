namespace MauiReactor
{
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
        public HStack HStack() =>
            GetNodeFromPool<HStack>();

        public HStack HStack(double spacing) =>
            GetNodeFromPool<HStack>()
                .Spacing(spacing);

        public HStack HStack(IEnumerable<VisualNode> children)
        {
            var hstack = GetNodeFromPool<HStack>();
            hstack.AddChildren(children);
            return hstack;
        }
    }

}
