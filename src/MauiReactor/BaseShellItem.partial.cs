using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IBaseShellItem
{
    string? Route { get; set; }
}

public partial class BaseShellItem<T>
{
    string? IBaseShellItem.Route { get; set; }

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIBaseShellItem = (IBaseShellItem)this;

        if (thisAsIBaseShellItem.Route != null)
        {
            Microsoft.Maui.Controls.Routing.SetRoute(NativeControl, thisAsIBaseShellItem.Route);
        }
        base.OnUpdate();
    }
}

public static partial class BaseShellItemExtensions
{
    public static T Route<T>(this T baseShellItem, string route) where T : IBaseShellItem
    {
        baseShellItem.Route = route;
        return baseShellItem;
    }

}
