using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;
using System.Collections;

namespace MauiReactor;

public partial interface IGenericMultiPage
{
    IEnumerable? ItemsSource { get; set; }

    Func<object, VisualNode>? ItemTemplate { get; set; }

    Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? ItemTemplateWithNativeView { get; set; }

    VisualStateGroupList? ItemVisualStateGroups { get; set; }
}


public abstract partial class MultiPage<T, TChild> : Page<T>, ICustomDataTemplateOwner, IGenericMultiPage where T : Microsoft.Maui.Controls.MultiPage<TChild>, new()
    where TChild : Microsoft.Maui.Controls.Page
{
    private List<WeakReference<VisualNode>>? _loadedForciblyChildren;

    private CustomDataTemplate<Microsoft.Maui.Controls.ContentPage>? _customDataTemplate;

    IEnumerable? IGenericMultiPage.ItemsSource { get; set; }

    Func<object, VisualNode>? IGenericMultiPage.ItemTemplate { get; set; }

    Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? IGenericMultiPage.ItemTemplateWithNativeView { get; set; }

    VisualStateGroupList? IGenericMultiPage.ItemVisualStateGroups { get; set; }

    VisualNode? ICustomDataTemplateOwner.GetVisualNodeForItem(object item)
    {
        var thisAsIItemsView = (IGenericMultiPage)this;
        if (thisAsIItemsView.ItemTemplate == null)
        {
            return null;
        }

        var visualNodeForItem = thisAsIItemsView.ItemTemplate.Invoke(item);

        _loadedForciblyChildren?.Add(new WeakReference<VisualNode>(visualNodeForItem));

        return visualNodeForItem;
    }

    partial void OnBeginUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIGenericMultiPage = (IGenericMultiPage)this;

        _loadedForciblyChildren = null;

        if (thisAsIGenericMultiPage.ItemsSource != null &&
            NativeControl.ItemsSource == thisAsIGenericMultiPage.ItemsSource)
        {
            Validate.EnsureNotNull(_customDataTemplate);

            _customDataTemplate.Owner = this;

            _customDataTemplate.Update();
        }
        else if (thisAsIGenericMultiPage.ItemsSource != null)
        {
            _customDataTemplate = new CustomDataTemplate<Microsoft.Maui.Controls.ContentPage>(this, itemTemplatePresenter =>
                thisAsIGenericMultiPage.ItemVisualStateGroups?.SetToVisualElement(itemTemplatePresenter));
            NativeControl.ItemsSource = thisAsIGenericMultiPage.ItemsSource;
            NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
        }
        else
        {
            NativeControl.ClearValue(ItemsView.ItemsSourceProperty);
            NativeControl.ClearValue(ItemsView.ItemTemplateProperty);
        }        
    }

    partial void Migrated(VisualNode newNode)
    {
        var newItemsView = ((MultiPage<T, TChild>)newNode);
        newItemsView._customDataTemplate = _customDataTemplate;
        if (newItemsView._customDataTemplate != null)
        {
            newItemsView._customDataTemplate.Owner = newItemsView;
        }
    }

    protected override void OnMount()
    {
        base.OnMount();

        Validate.EnsureNotNull(NativeControl);
        NativeControl.Children.Clear();
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (NativeControl.ItemsSource == null)
        {
            if (childControl is TChild page)
                NativeControl.Children.Insert(widget.ChildIndex, page);
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (NativeControl.ItemsSource == null)
        {
            if (childControl is TChild page)
                NativeControl.Children.Remove(page);
        }

        base.OnRemoveChild(widget, childControl);
    }
}


public static partial class MultiPageExtensions
{
    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource) where T : IGenericMultiPage
    {
        itemsView.ItemsSource = itemsSource;
        return itemsView;
    }

    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource, Func<TItem, VisualNode> template) where T : IGenericMultiPage
    {
        itemsView.ItemsSource = itemsSource;
        itemsView.ItemTemplate = new Func<object, VisualNode>(item => template((TItem)item));
        return itemsView;
    }

    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IGenericMultiPage
    {
        itemsView.ItemsSource = itemsSource;
        itemsView.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
        return itemsView;
    }

    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IGenericMultiPage
    {
        itemsView.ItemsSource = itemsSource;
        itemsView.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
        return itemsView;
    }

    public static T ItemVisualState<T>(this T itemsView, string groupName, string stateName, BindableProperty? property = null, object? value = null, string? targetName = null) where T : IGenericMultiPage
    {
        itemsView.ItemVisualStateGroups ??= [];

        itemsView.ItemVisualStateGroups.Set(groupName, stateName, property, value, targetName);

        return itemsView;
    }

    public static T ItemVisualState<T>(this T itemsView, VisualStateGroupList visualState) where T : IGenericMultiPage
    {
        itemsView.ItemVisualStateGroups = visualState;

        return itemsView;
    }
}
