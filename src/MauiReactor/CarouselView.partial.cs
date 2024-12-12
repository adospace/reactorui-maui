using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial interface ICarouselView
{
    IItemsLayout? ItemsLayout { get; set; }
}

public partial class CarouselView<T>
{
    IItemsLayout? ICarouselView.ItemsLayout { get; set; }


    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsICarouselView = (ICarouselView)this;

        var children = base.RenderChildren();

        if (thisAsICarouselView.ItemsLayout != null)
        {
            children = children.Concat([(VisualNode)thisAsICarouselView.ItemsLayout]);
        }

        return children;
    }


    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsICarouselView = (ICarouselView)this;

        if (widget == thisAsICarouselView.ItemsLayout &&
            childNativeControl is Microsoft.Maui.Controls.LinearItemsLayout itemsLayout)
        {
            NativeControl.ItemsLayout = itemsLayout;
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIVisualElement = (IStructuredItemsView)this;

        if (widget == thisAsIVisualElement.ItemsLayout &&
            childNativeControl is Microsoft.Maui.Controls.IItemsLayout)
        {
            NativeControl.ItemsLayout = null;
        }

        base.OnRemoveChild(widget, childNativeControl);
    }
}

public static partial class CarouselViewExtensions
{
    public static T ItemsLayout<T>(this T carouselView, IItemsLayout itemsLayout) where T : ICarouselView
    {
        carouselView.SetProperty(Microsoft.Maui.Controls.CarouselView.ItemsLayoutProperty, itemsLayout);
        carouselView.ItemsLayout = itemsLayout;
        return carouselView;
    }
}
