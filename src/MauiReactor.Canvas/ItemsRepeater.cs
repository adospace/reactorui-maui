using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;


public partial interface IItemsRepeater<T> : ICanvasVisualElement
{
    PropertyValue<float>? ItemHeight { get; set; }

    PropertyValue<System.Collections.Generic.IReadOnlyList<T>?>? Items { get; set; }

    PropertyValue<System.Func<T, MauiReactor.Canvas.Internals.CanvasNode?>?>? ItemTemplate { get; set; }

    PropertyValue<float>? ItemWidth { get; set; }

    PropertyValue<Microsoft.Maui.Controls.ItemsLayoutOrientation>? Orientation { get; set; }

    PropertyValue<float>? ViewportX { get; set; }

    PropertyValue<float>? ViewportY { get; set; }
}

public sealed partial class ItemsRepeater<T> : CanvasVisualElement<MauiReactor.Canvas.Internals.ItemsRepeater<T>>, IItemsRepeater<T>
{
    public ItemsRepeater()
    {
    }

    public ItemsRepeater(Action<MauiReactor.Canvas.Internals.ItemsRepeater<T>?> componentRefAction) : base(componentRefAction)
    {
    }

    PropertyValue<float>? IItemsRepeater<T>.ItemHeight { get; set; }

    PropertyValue<System.Collections.Generic.IReadOnlyList<T>?>? IItemsRepeater<T>.Items { get; set; }

    PropertyValue<System.Func<T, MauiReactor.Canvas.Internals.CanvasNode?>?>? IItemsRepeater<T>.ItemTemplate { get; set; }

    PropertyValue<float>? IItemsRepeater<T>.ItemWidth { get; set; }

    PropertyValue<Microsoft.Maui.Controls.ItemsLayoutOrientation>? IItemsRepeater<T>.Orientation { get; set; }

    PropertyValue<float>? IItemsRepeater<T>.ViewportX { get; set; }

    PropertyValue<float>? IItemsRepeater<T>.ViewportY { get; set; }

    protected override void OnUpdate()
    {
        OnBeginUpdate();
        Validate.EnsureNotNull(NativeControl);
        var thisAsIItemsRepeater = (IItemsRepeater<T>)this;
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.ItemHeightProperty, thisAsIItemsRepeater.ItemHeight);
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.ItemsProperty, thisAsIItemsRepeater.Items);
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.ItemTemplateProperty, thisAsIItemsRepeater.ItemTemplate);
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.ItemWidthProperty, thisAsIItemsRepeater.ItemWidth);
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.OrientationProperty, thisAsIItemsRepeater.Orientation);
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.ViewportXProperty, thisAsIItemsRepeater.ViewportX);
        SetPropertyValue(NativeControl, MauiReactor.Canvas.Internals.ItemsRepeater<T>.ViewportYProperty, thisAsIItemsRepeater.ViewportY);
        base.OnUpdate();
        OnEndUpdate();
    }

    partial void OnBeginUpdate();
    partial void OnEndUpdate();
}

public static partial class ItemsRepeaterExtensions
{
    public static IItemsRepeater<T> ItemHeight<T>(this IItemsRepeater<T> itemsRepeater, float itemHeight)
    {
        itemsRepeater.ItemHeight = new PropertyValue<float>(itemHeight);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ItemHeight<T>(this IItemsRepeater<T> itemsRepeater, Func<float> itemHeightFunc)
    {
        itemsRepeater.ItemHeight = new PropertyValue<float>(itemHeightFunc);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> Items<T>(this IItemsRepeater<T> itemsRepeater, System.Collections.Generic.IReadOnlyList<T>? items)
    {
        itemsRepeater.Items = new PropertyValue<System.Collections.Generic.IReadOnlyList<T>?>(items);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> Items<T>(this IItemsRepeater<T> itemsRepeater, Func<System.Collections.Generic.IReadOnlyList<T>?> itemsFunc)
    {
        itemsRepeater.Items = new PropertyValue<System.Collections.Generic.IReadOnlyList<T>?>(itemsFunc);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ItemTemplate<T>(this IItemsRepeater<T> itemsRepeater, System.Func<T, VisualNode>? itemTemplate)
    {
        itemsRepeater.ItemTemplate = new PropertyValue<System.Func<T, MauiReactor.Canvas.Internals.CanvasNode?>?>(item =>
        {
            if (itemTemplate == null)
            {
                return null;
            }

            var root = itemTemplate.Invoke(item);
            var itemTemplateHost = new TemplateHost<MauiReactor.Canvas.Internals.CanvasNode>(root);
            return itemTemplateHost.NativeElement;
        });
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ItemWidth<T>(this IItemsRepeater<T> itemsRepeater, float itemWidth)
    {
        itemsRepeater.ItemWidth = new PropertyValue<float>(itemWidth);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ItemWidth<T>(this IItemsRepeater<T> itemsRepeater, Func<float> itemWidthFunc)
    {
        itemsRepeater.ItemWidth = new PropertyValue<float>(itemWidthFunc);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> Orientation<T>(this IItemsRepeater<T> itemsRepeater, Microsoft.Maui.Controls.ItemsLayoutOrientation orientation)
    {
        itemsRepeater.Orientation = new PropertyValue<Microsoft.Maui.Controls.ItemsLayoutOrientation>(orientation);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> Orientation<T>(this IItemsRepeater<T> itemsRepeater, Func<Microsoft.Maui.Controls.ItemsLayoutOrientation> orientationFunc)
    {
        itemsRepeater.Orientation = new PropertyValue<Microsoft.Maui.Controls.ItemsLayoutOrientation>(orientationFunc);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ViewportX<T>(this IItemsRepeater<T> itemsRepeater, float viewportX)
    {
        itemsRepeater.ViewportX = new PropertyValue<float>(viewportX);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ViewportX<T>(this IItemsRepeater<T> itemsRepeater, Func<float> viewportXFunc)
    {
        itemsRepeater.ViewportX = new PropertyValue<float>(viewportXFunc);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ViewportY<T>(this IItemsRepeater<T> itemsRepeater, float viewportY)
    {
        itemsRepeater.ViewportY = new PropertyValue<float>(viewportY);
        return itemsRepeater;
    }

    public static IItemsRepeater<T> ViewportY<T>(this IItemsRepeater<T> itemsRepeater, Func<float> viewportYFunc)
    {
        itemsRepeater.ViewportY = new PropertyValue<float>(viewportYFunc);
        return itemsRepeater;
    }
}


