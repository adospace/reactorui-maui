using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IToolbarItem
{
    ToolbarItemOrder? Order { get; set; }

    int? Priority { get; set; }
}

public partial class ToolbarItem<T> : IToolbarItem
{
    ToolbarItemOrder? IToolbarItem.Order { get; set; }
    int? IToolbarItem.Priority { get; set; }

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIToolbarItem = (IToolbarItem)this;
        if (thisAsIToolbarItem.Order != null)
        {
            NativeControl.Order = thisAsIToolbarItem.Order.Value;
        }
        if (thisAsIToolbarItem.Priority != null)
        {
            NativeControl.Priority = thisAsIToolbarItem.Priority.Value;
        }
        base.OnUpdate();
    }
}

public partial class ToolbarItem
{
    public ToolbarItem(string text)
        => this.Text(text);
}

public static partial class ToolbarItemExtensions
{
    public static T Order<T>(this T toolbarItem, ToolbarItemOrder order) where T : IToolbarItem
    {
        toolbarItem.Order = order;
        return toolbarItem;
    }

    public static T Priority<T>(this T toolbarItem, int priority) where T : IToolbarItem
    {
        toolbarItem.Priority = priority;
        return toolbarItem;
    }
}


public partial class Component
{
    public static ToolbarItem ToolbarItem(string text) =>
        new ToolbarItem().Text(text);

}
