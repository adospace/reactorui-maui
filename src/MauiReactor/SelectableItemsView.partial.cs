﻿using MauiReactor.Internals;

namespace MauiReactor;


public partial interface ISelectableItemsView 
{
    object? SelectedItems { get; set; }
}

public partial class SelectableItemsView<T> : StructuredItemsView<T>
{
    object? ISelectableItemsView.SelectedItems { get; set; }

    partial void OnReset()
    {
        var thisAsISelectableItemsView = (ISelectableItemsView)this;
        thisAsISelectableItemsView.SelectedItems = null;
    }


    partial void OnEndUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsISelectableItemsView = (ISelectableItemsView)this;
        
        NativeControl.SelectionChanged -= NativeControl_SelectionChanged;
        
        SetPropertyValue(NativeControl, Microsoft.Maui.Controls.SelectableItemsView.SelectedItemsProperty, thisAsISelectableItemsView.SelectedItems);
        
        if (thisAsISelectableItemsView.SelectionChangedAction != null || thisAsISelectableItemsView.SelectionChangedActionWithArgs != null)
        {
            NativeControl.SelectionChanged += NativeControl_SelectionChanged;
        }
    }
}


public static partial class SelectableItemsViewExtensions
{
    public static T SelectedItems<T>(this T selectableItemsView, IList<object>? selectedItems)
        where T : ISelectableItemsView
    {
        selectableItemsView.SelectedItems = selectedItems;
        return selectableItemsView;
    }

    public static T SelectedItems<T>(this T selectableItemsView, Func<IList<object>?> selectedItemsFunc)
        where T : ISelectableItemsView
    {
        selectableItemsView.SelectedItems = new PropertyValue<IList<object>?>(selectedItemsFunc);
        return selectableItemsView;
    }

    public static T OnSelected<T, I>(this T collectionView, Action<I?> action) where T : ISelectableItemsView
    {
        collectionView.SelectionChangedActionWithArgs = (sender, args) => action(args.CurrentSelection.Count == 0 ? default : (I)args.CurrentSelection[0]);
        return collectionView;
    }

    public static T OnSelectedMany<T, I>(this T collectionView, Action<I[]> action) where T : ISelectableItemsView
    {
        collectionView.SelectionChangedActionWithArgs = (sender, args) => action(args.CurrentSelection.Cast<I>().ToArray());
        return collectionView;
    }
}
