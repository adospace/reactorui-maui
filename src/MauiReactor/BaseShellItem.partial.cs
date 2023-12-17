using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IBaseShellItem
    {
        PropertyValue<bool>? FlyoutItemIsVisible { get; set; }

        string? Route { get; set; }
    }

    public partial class BaseShellItem<T>
    {
        PropertyValue<bool>? IBaseShellItem.FlyoutItemIsVisible { get; set; }

        string? IBaseShellItem.Route { get; set; }

        partial void OnReset()
        {
            var thisAsIBaseShellItem = (IBaseShellItem)this;
            thisAsIBaseShellItem.FlyoutItemIsVisible = null;
            thisAsIBaseShellItem.Route = null;
        }

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIBaseShellItem = (IBaseShellItem)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shell.FlyoutItemIsVisibleProperty, thisAsIBaseShellItem.FlyoutItemIsVisible);

            if (thisAsIBaseShellItem.Route != null)
            {
                Microsoft.Maui.Controls.Routing.SetRoute(NativeControl, thisAsIBaseShellItem.Route);
            }
        }
    }

    public static partial class BaseShellItemExtensions
    {
        public static T FlyoutItemIsVisible<T>(this T baseShellItem, bool visible) where T : IBaseShellItem
        {
            baseShellItem.FlyoutItemIsVisible = new PropertyValue<bool>(visible);
            return baseShellItem;
        }

        public static T FlyoutItemIsVisible<T>(this T baseShellItem, Func<bool> visibleFunc) where T : IBaseShellItem
        {
            baseShellItem.FlyoutItemIsVisible = new PropertyValue<bool>(visibleFunc);
            return baseShellItem;
        }

        public static T Route<T>(this T baseShellItem, string route) where T : IBaseShellItem
        {
            baseShellItem.Route = route;
            return baseShellItem;
        }

    }
}
