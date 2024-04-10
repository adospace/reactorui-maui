using MauiReactor.Internals;
using System.Collections;

namespace MauiReactor;

public partial interface IItemsView
{
    IEnumerable? ItemsSource { get; set; }

    Func<object, VisualNode>? ItemTemplate { get; set; }

    Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? ItemTemplateWithNativeView { get; set; }

    VisualStateGroupList? ItemVisualStateGroups { get; set; }

    object? EmptyView { get; set; }
}


public partial class ItemsView<T> : ICustomDataTemplateOwner, IAutomationItemContainer
{
    private List<WeakReference<VisualNode>>? _loadedForciblyChildren;

    private CustomDataTemplate? _customDataTemplate;

    IEnumerable? IItemsView.ItemsSource { get; set; }

    Func<object, VisualNode>? IItemsView.ItemTemplate { get; set; }

    Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>? IItemsView.ItemTemplateWithNativeView { get; set; }

    VisualStateGroupList? IItemsView.ItemVisualStateGroups { get; set; }

    object? IItemsView.EmptyView { get; set; }

    partial void OnReset()
    {
        _loadedForciblyChildren = null;
        _customDataTemplate = null;

        var thisAsIItemsView = (IItemsView)this;
        thisAsIItemsView.ItemsSource = null;
        thisAsIItemsView.ItemTemplate = null;
        thisAsIItemsView.ItemTemplateWithNativeView = null;
        thisAsIItemsView.ItemVisualStateGroups = null;
        thisAsIItemsView.EmptyView = null;
    }

    VisualNode? ICustomDataTemplateOwner.GetVisualNodeForItem(object item)
    {
        var thisAsIItemsView = (IItemsView)this;
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
        var thisAsIItemsView = (IItemsView)this;

        _loadedForciblyChildren = null;

        if (thisAsIItemsView.ItemsSource != null &&
            NativeControl.ItemsSource == thisAsIItemsView.ItemsSource)
        {
            Validate.EnsureNotNull(_customDataTemplate);

            _customDataTemplate.Owner = this;

            _customDataTemplate.Update();
        }
        else if (thisAsIItemsView.ItemsSource != null)
        {
            _customDataTemplate = new CustomDataTemplate(this, itemTemplatePresenter =>
                thisAsIItemsView.ItemVisualStateGroups?.SetToVisualElement(itemTemplatePresenter));
            NativeControl.ItemsSource = thisAsIItemsView.ItemsSource;
            NativeControl.ItemTemplate = _customDataTemplate.DataTemplate;
        }
        else
        {
            NativeControl.ClearValue(ItemsView.ItemsSourceProperty);
            NativeControl.ClearValue(ItemsView.ItemTemplateProperty);
        }
    }

    protected override void OnMigrated(VisualNode newNode)
    {
        var newItemsView = ((ItemsView<T>)newNode);
        newItemsView._customDataTemplate = _customDataTemplate;
        if (newItemsView._customDataTemplate != null)
        {
            newItemsView._customDataTemplate.Owner = newItemsView;
        }


        base.OnMigrated(newNode);
    }

    protected override void OnUnmount()
    {
        Validate.EnsureNotNull(NativeControl);
        NativeControl.ClearValue(ItemsView.ItemsSourceProperty);
        NativeControl.ClearValue(ItemsView.ItemTemplateProperty);
        base.OnUnmount();
    }

    IEnumerable<TChild> IAutomationItemContainer.Descendants<TChild>()
    {
        if (_loadedForciblyChildren == null)
        {
            ForceItemsLoad();
        }

        Validate.EnsureNotNull(_loadedForciblyChildren);

        foreach (var loadedForciblyChild in _loadedForciblyChildren)
        {
            if (loadedForciblyChild.TryGetTarget(out var child))
            {
                if (child is TChild childT)
                {
                    yield return childT;
                }

                if (child is IVisualNodeWithNativeControl childVisualNodeWithNativeControl)
                {
                    var childNativeControl = childVisualNodeWithNativeControl.GetNativeControl<BindableObject>();

                    if (childNativeControl is TChild childNativeControlAsT)
                    {
                        yield return childNativeControlAsT;
                    }
                }

                foreach (var childChildT in ((IAutomationItemContainer)child).Descendants<TChild>())
                {
                    yield return childChildT;
                }
            }
        }
    }

    private void ForceItemsLoad()
    {
        Validate.EnsureNotNull(NativeControl);

        var itemsSource = NativeControl.ItemsSource.Cast<object>().ToArray();

        _loadedForciblyChildren = [];

        foreach (var item in itemsSource)
        {
            var itemContent = (BindableObject)NativeControl.ItemTemplate.CreateContent();

            itemContent.BindingContext = item;
        }
    }

    partial void OnEndUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIItemsView = (IItemsView)this;

        if (thisAsIItemsView.EmptyView is string)
        {
            NativeControl.EmptyView = thisAsIItemsView.EmptyView;
        }
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        var thisAsIItemsView = (IItemsView)this;

        var children = base.RenderChildren();

        if (thisAsIItemsView.EmptyView is VisualNode emptyViewNode)
        {
            children = children.Concat(new[] { emptyViewNode });
        }

        return children;
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIItemsView = (IItemsView)this;

        if (widget == thisAsIItemsView.EmptyView &&
            childNativeControl is Microsoft.Maui.Controls.View emptyView)
        {
            NativeControl.EmptyView = emptyView;
        }

        base.OnAddChild(widget, childNativeControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        var thisAsIItemsView = (IItemsView)this;

        if (widget == thisAsIItemsView.EmptyView &&
            childNativeControl is Microsoft.Maui.Controls.View)
        {
            NativeControl.EmptyView = null;
        }

        base.OnRemoveChild(widget, childNativeControl);
    }

}

public static partial class ItemsViewExtensions
{
    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource) where T : IItemsView
    {
        itemsView.ItemsSource = itemsSource;
        return itemsView;
    }

    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource, Func<TItem, VisualNode> template) where T : IItemsView
    {
        itemsView.ItemsSource = itemsSource;
        itemsView.ItemTemplate = new Func<object, VisualNode>(item => template((TItem)item));
        return itemsView;
    }

    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable<TItem> itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IItemsView
    {
        itemsView.ItemsSource = itemsSource;
        itemsView.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
        return itemsView;
    }

    public static T ItemsSource<T, TItem>(this T itemsView, IEnumerable itemsSource, Func<TItem, Microsoft.Maui.Controls.ItemsView, VisualNode> template) where T : IItemsView
    {
        itemsView.ItemsSource = itemsSource;
        itemsView.ItemTemplateWithNativeView = new Func<object, Microsoft.Maui.Controls.ItemsView, VisualNode>((item, nativeView) => template((TItem)item, nativeView));
        return itemsView;
    }

    public static T ItemVisualState<T>(this T itemsView, string groupName, string stateName, BindableProperty? property = null, object? value = null, string? targetName = null) where T : IItemsView
    {
        itemsView.ItemVisualStateGroups ??= [];

        itemsView.ItemVisualStateGroups.Set(groupName, stateName, property, value, targetName);

        return itemsView;
    }

    public static T ItemVisualState<T>(this T itemsView, VisualStateGroupList visualState) where T : IItemsView
    {
        itemsView.ItemVisualStateGroups = visualState;

        return itemsView;
    }


    public static T EmptyView<T>(this T itemsView, VisualNode emptyView) where T : IItemsView
    {
        itemsView.EmptyView = emptyView;

        return itemsView;
    }

    public static T EmptyView<T>(this T itemsView, string emptyView) where T : IItemsView
    {
        itemsView.EmptyView = emptyView;

        return itemsView;
    }
}
