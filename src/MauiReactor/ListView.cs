// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable
namespace MauiReactor;
public partial interface IListView : IGenericItemsView
{
    EventCommand<ItemVisibilityEventArgs>? ItemAppearingEvent { get; set; }

    EventCommand<ItemVisibilityEventArgs>? ItemDisappearingEvent { get; set; }

    EventCommand<SelectedItemChangedEventArgs>? ItemSelectedEvent { get; set; }

    EventCommand<ItemTappedEventArgs>? ItemTappedEvent { get; set; }

    EventCommand<ScrolledEventArgs>? ScrolledEvent { get; set; }

    EventCommand<EventArgs>? RefreshingEvent { get; set; }
}

public abstract partial class ListView<T> : ItemsView<T, Microsoft.Maui.Controls.Cell>, IListView where T : Microsoft.Maui.Controls.ListView, new()
{
    public ListView(Action<T?>? componentRefAction = null) : base(componentRefAction)
    {
        ListViewStyles.Default?.Invoke(this);
    }

    EventCommand<ItemVisibilityEventArgs>? IListView.ItemAppearingEvent { get; set; }

    EventCommand<ItemVisibilityEventArgs>? IListView.ItemDisappearingEvent { get; set; }

    EventCommand<SelectedItemChangedEventArgs>? IListView.ItemSelectedEvent { get; set; }

    EventCommand<ItemTappedEventArgs>? IListView.ItemTappedEvent { get; set; }

    EventCommand<ScrolledEventArgs>? IListView.ScrolledEvent { get; set; }

    EventCommand<EventArgs>? IListView.RefreshingEvent { get; set; }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && ListViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void OnAttachingNativeEvents();
    partial void OnDetachingNativeEvents();
    private EventCommand<ItemVisibilityEventArgs>? _executingItemAppearingEvent;
    private EventCommand<ItemVisibilityEventArgs>? _executingItemDisappearingEvent;
    private EventCommand<SelectedItemChangedEventArgs>? _executingItemSelectedEvent;
    private EventCommand<ItemTappedEventArgs>? _executingItemTappedEvent;
    private EventCommand<ScrolledEventArgs>? _executingScrolledEvent;
    private EventCommand<EventArgs>? _executingRefreshingEvent;
    protected override void OnAttachNativeEvents()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIListView = (IListView)this;
        if (thisAsIListView.ItemAppearingEvent != null)
        {
            NativeControl.ItemAppearing += NativeControl_ItemAppearing;
        }

        if (thisAsIListView.ItemDisappearingEvent != null)
        {
            NativeControl.ItemDisappearing += NativeControl_ItemDisappearing;
        }

        if (thisAsIListView.ItemSelectedEvent != null)
        {
            NativeControl.ItemSelected += NativeControl_ItemSelected;
        }

        if (thisAsIListView.ItemTappedEvent != null)
        {
            NativeControl.ItemTapped += NativeControl_ItemTapped;
        }

        if (thisAsIListView.ScrolledEvent != null)
        {
            NativeControl.Scrolled += NativeControl_Scrolled;
        }

        if (thisAsIListView.RefreshingEvent != null)
        {
            NativeControl.Refreshing += NativeControl_Refreshing;
        }

        OnAttachingNativeEvents();
        base.OnAttachNativeEvents();
    }

    private void NativeControl_ItemAppearing(object? sender, ItemVisibilityEventArgs e)
    {
        var thisAsIListView = (IListView)this;
        if (_executingItemAppearingEvent == null || _executingItemAppearingEvent.IsCompleted)
        {
            _executingItemAppearingEvent = thisAsIListView.ItemAppearingEvent;
            _executingItemAppearingEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_ItemDisappearing(object? sender, ItemVisibilityEventArgs e)
    {
        var thisAsIListView = (IListView)this;
        if (_executingItemDisappearingEvent == null || _executingItemDisappearingEvent.IsCompleted)
        {
            _executingItemDisappearingEvent = thisAsIListView.ItemDisappearingEvent;
            _executingItemDisappearingEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_ItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        var thisAsIListView = (IListView)this;
        if (_executingItemSelectedEvent == null || _executingItemSelectedEvent.IsCompleted)
        {
            _executingItemSelectedEvent = thisAsIListView.ItemSelectedEvent;
            _executingItemSelectedEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_ItemTapped(object? sender, ItemTappedEventArgs e)
    {
        var thisAsIListView = (IListView)this;
        if (_executingItemTappedEvent == null || _executingItemTappedEvent.IsCompleted)
        {
            _executingItemTappedEvent = thisAsIListView.ItemTappedEvent;
            _executingItemTappedEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Scrolled(object? sender, ScrolledEventArgs e)
    {
        var thisAsIListView = (IListView)this;
        if (_executingScrolledEvent == null || _executingScrolledEvent.IsCompleted)
        {
            _executingScrolledEvent = thisAsIListView.ScrolledEvent;
            _executingScrolledEvent?.Execute(sender, e);
        }
    }

    private void NativeControl_Refreshing(object? sender, EventArgs e)
    {
        var thisAsIListView = (IListView)this;
        if (_executingRefreshingEvent == null || _executingRefreshingEvent.IsCompleted)
        {
            _executingRefreshingEvent = thisAsIListView.RefreshingEvent;
            _executingRefreshingEvent?.Execute(sender, e);
        }
    }

    protected override void OnDetachNativeEvents()
    {
        if (NativeControl != null)
        {
            NativeControl.ItemAppearing -= NativeControl_ItemAppearing;
            NativeControl.ItemDisappearing -= NativeControl_ItemDisappearing;
            NativeControl.ItemSelected -= NativeControl_ItemSelected;
            NativeControl.ItemTapped -= NativeControl_ItemTapped;
            NativeControl.Scrolled -= NativeControl_Scrolled;
            NativeControl.Refreshing -= NativeControl_Refreshing;
        }

        OnDetachingNativeEvents();
        base.OnDetachNativeEvents();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        if (newNode is ListView<T> @listview)
        {
            if (_executingItemAppearingEvent != null && !_executingItemAppearingEvent.IsCompleted)
            {
                @listview._executingItemAppearingEvent = _executingItemAppearingEvent;
            }

            if (_executingItemDisappearingEvent != null && !_executingItemDisappearingEvent.IsCompleted)
            {
                @listview._executingItemDisappearingEvent = _executingItemDisappearingEvent;
            }

            if (_executingItemSelectedEvent != null && !_executingItemSelectedEvent.IsCompleted)
            {
                @listview._executingItemSelectedEvent = _executingItemSelectedEvent;
            }

            if (_executingItemTappedEvent != null && !_executingItemTappedEvent.IsCompleted)
            {
                @listview._executingItemTappedEvent = _executingItemTappedEvent;
            }

            if (_executingScrolledEvent != null && !_executingScrolledEvent.IsCompleted)
            {
                @listview._executingScrolledEvent = _executingScrolledEvent;
            }

            if (_executingRefreshingEvent != null && !_executingRefreshingEvent.IsCompleted)
            {
                @listview._executingRefreshingEvent = _executingRefreshingEvent;
            }
        }

        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class ListView : ListView<Microsoft.Maui.Controls.ListView>
{
    public ListView(Action<Microsoft.Maui.Controls.ListView?>? componentRefAction = null) : base(componentRefAction)
    {
    }

    public ListView(params IEnumerable<VisualNode?>? children)
    {
        if (children != null)
        {
            this.AddChildren(children);
        }
    }
}

public static partial class ListViewExtensions
{
    public static T IsPullToRefreshEnabled<T>(this T listView, bool isPullToRefreshEnabled)
        where T : IListView
    {
        //listView.IsPullToRefreshEnabled = isPullToRefreshEnabled;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.IsPullToRefreshEnabledProperty, isPullToRefreshEnabled);
        return listView;
    }

    public static T IsPullToRefreshEnabled<T>(this T listView, Func<bool> isPullToRefreshEnabledFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.IsPullToRefreshEnabledProperty, new PropertyValue<bool>(isPullToRefreshEnabledFunc, componentWithState));
        return listView;
    }

    public static T IsRefreshing<T>(this T listView, bool isRefreshing)
        where T : IListView
    {
        //listView.IsRefreshing = isRefreshing;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.IsRefreshingProperty, isRefreshing);
        return listView;
    }

    public static T IsRefreshing<T>(this T listView, Func<bool> isRefreshingFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.IsRefreshingProperty, new PropertyValue<bool>(isRefreshingFunc, componentWithState));
        return listView;
    }

    public static T SelectedItem<T>(this T listView, object? selectedItem)
        where T : IListView
    {
        //listView.SelectedItem = selectedItem;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SelectedItemProperty, selectedItem);
        return listView;
    }

    public static T SelectedItem<T>(this T listView, Func<object?> selectedItemFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SelectedItemProperty, new PropertyValue<object?>(selectedItemFunc, componentWithState));
        return listView;
    }

    public static T SelectionMode<T>(this T listView, Microsoft.Maui.Controls.ListViewSelectionMode selectionMode)
        where T : IListView
    {
        //listView.SelectionMode = selectionMode;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SelectionModeProperty, selectionMode);
        return listView;
    }

    public static T SelectionMode<T>(this T listView, Func<Microsoft.Maui.Controls.ListViewSelectionMode> selectionModeFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SelectionModeProperty, new PropertyValue<Microsoft.Maui.Controls.ListViewSelectionMode>(selectionModeFunc, componentWithState));
        return listView;
    }

    public static T HasUnevenRows<T>(this T listView, bool hasUnevenRows)
        where T : IListView
    {
        //listView.HasUnevenRows = hasUnevenRows;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.HasUnevenRowsProperty, hasUnevenRows);
        return listView;
    }

    public static T HasUnevenRows<T>(this T listView, Func<bool> hasUnevenRowsFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.HasUnevenRowsProperty, new PropertyValue<bool>(hasUnevenRowsFunc, componentWithState));
        return listView;
    }

    public static T RowHeight<T>(this T listView, int rowHeight)
        where T : IListView
    {
        //listView.RowHeight = rowHeight;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.RowHeightProperty, rowHeight);
        return listView;
    }

    public static T RowHeight<T>(this T listView, Func<int> rowHeightFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.RowHeightProperty, new PropertyValue<int>(rowHeightFunc, componentWithState));
        return listView;
    }

    public static T IsGroupingEnabled<T>(this T listView, bool isGroupingEnabled)
        where T : IListView
    {
        //listView.IsGroupingEnabled = isGroupingEnabled;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.IsGroupingEnabledProperty, isGroupingEnabled);
        return listView;
    }

    public static T IsGroupingEnabled<T>(this T listView, Func<bool> isGroupingEnabledFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.IsGroupingEnabledProperty, new PropertyValue<bool>(isGroupingEnabledFunc, componentWithState));
        return listView;
    }

    public static T SeparatorVisibility<T>(this T listView, Microsoft.Maui.Controls.SeparatorVisibility separatorVisibility)
        where T : IListView
    {
        //listView.SeparatorVisibility = separatorVisibility;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SeparatorVisibilityProperty, separatorVisibility);
        return listView;
    }

    public static T SeparatorVisibility<T>(this T listView, Func<Microsoft.Maui.Controls.SeparatorVisibility> separatorVisibilityFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SeparatorVisibilityProperty, new PropertyValue<Microsoft.Maui.Controls.SeparatorVisibility>(separatorVisibilityFunc, componentWithState));
        return listView;
    }

    public static T SeparatorColor<T>(this T listView, Microsoft.Maui.Graphics.Color separatorColor)
        where T : IListView
    {
        //listView.SeparatorColor = separatorColor;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SeparatorColorProperty, separatorColor);
        return listView;
    }

    public static T SeparatorColor<T>(this T listView, Func<Microsoft.Maui.Graphics.Color> separatorColorFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.SeparatorColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(separatorColorFunc, componentWithState));
        return listView;
    }

    public static T RefreshControlColor<T>(this T listView, Microsoft.Maui.Graphics.Color refreshControlColor)
        where T : IListView
    {
        //listView.RefreshControlColor = refreshControlColor;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.RefreshControlColorProperty, refreshControlColor);
        return listView;
    }

    public static T RefreshControlColor<T>(this T listView, Func<Microsoft.Maui.Graphics.Color> refreshControlColorFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.RefreshControlColorProperty, new PropertyValue<Microsoft.Maui.Graphics.Color>(refreshControlColorFunc, componentWithState));
        return listView;
    }

    public static T HorizontalScrollBarVisibility<T>(this T listView, Microsoft.Maui.ScrollBarVisibility horizontalScrollBarVisibility)
        where T : IListView
    {
        //listView.HorizontalScrollBarVisibility = horizontalScrollBarVisibility;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.HorizontalScrollBarVisibilityProperty, horizontalScrollBarVisibility);
        return listView;
    }

    public static T HorizontalScrollBarVisibility<T>(this T listView, Func<Microsoft.Maui.ScrollBarVisibility> horizontalScrollBarVisibilityFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.HorizontalScrollBarVisibilityProperty, new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(horizontalScrollBarVisibilityFunc, componentWithState));
        return listView;
    }

    public static T VerticalScrollBarVisibility<T>(this T listView, Microsoft.Maui.ScrollBarVisibility verticalScrollBarVisibility)
        where T : IListView
    {
        //listView.VerticalScrollBarVisibility = verticalScrollBarVisibility;
        listView.SetProperty(Microsoft.Maui.Controls.ListView.VerticalScrollBarVisibilityProperty, verticalScrollBarVisibility);
        return listView;
    }

    public static T VerticalScrollBarVisibility<T>(this T listView, Func<Microsoft.Maui.ScrollBarVisibility> verticalScrollBarVisibilityFunc, IComponentWithState? componentWithState = null)
        where T : IListView
    {
        listView.SetProperty(Microsoft.Maui.Controls.ListView.VerticalScrollBarVisibilityProperty, new PropertyValue<Microsoft.Maui.ScrollBarVisibility>(verticalScrollBarVisibilityFunc, componentWithState));
        return listView;
    }

    public static T OnItemAppearing<T>(this T listView, Action? itemAppearingAction)
        where T : IListView
    {
        listView.ItemAppearingEvent = new SyncEventCommand<ItemVisibilityEventArgs>(execute: itemAppearingAction);
        return listView;
    }

    public static T OnItemAppearing<T>(this T listView, Action<ItemVisibilityEventArgs>? itemAppearingAction)
        where T : IListView
    {
        listView.ItemAppearingEvent = new SyncEventCommand<ItemVisibilityEventArgs>(executeWithArgs: itemAppearingAction);
        return listView;
    }

    public static T OnItemAppearing<T>(this T listView, Action<object?, ItemVisibilityEventArgs>? itemAppearingAction)
        where T : IListView
    {
        listView.ItemAppearingEvent = new SyncEventCommand<ItemVisibilityEventArgs>(executeWithFullArgs: itemAppearingAction);
        return listView;
    }

    public static T OnItemAppearing<T>(this T listView, Func<Task>? itemAppearingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemAppearingEvent = new AsyncEventCommand<ItemVisibilityEventArgs>(execute: itemAppearingAction, runInBackground);
        return listView;
    }

    public static T OnItemAppearing<T>(this T listView, Func<ItemVisibilityEventArgs, Task>? itemAppearingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemAppearingEvent = new AsyncEventCommand<ItemVisibilityEventArgs>(executeWithArgs: itemAppearingAction, runInBackground);
        return listView;
    }

    public static T OnItemAppearing<T>(this T listView, Func<object?, ItemVisibilityEventArgs, Task>? itemAppearingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemAppearingEvent = new AsyncEventCommand<ItemVisibilityEventArgs>(executeWithFullArgs: itemAppearingAction, runInBackground);
        return listView;
    }

    public static T OnItemDisappearing<T>(this T listView, Action? itemDisappearingAction)
        where T : IListView
    {
        listView.ItemDisappearingEvent = new SyncEventCommand<ItemVisibilityEventArgs>(execute: itemDisappearingAction);
        return listView;
    }

    public static T OnItemDisappearing<T>(this T listView, Action<ItemVisibilityEventArgs>? itemDisappearingAction)
        where T : IListView
    {
        listView.ItemDisappearingEvent = new SyncEventCommand<ItemVisibilityEventArgs>(executeWithArgs: itemDisappearingAction);
        return listView;
    }

    public static T OnItemDisappearing<T>(this T listView, Action<object?, ItemVisibilityEventArgs>? itemDisappearingAction)
        where T : IListView
    {
        listView.ItemDisappearingEvent = new SyncEventCommand<ItemVisibilityEventArgs>(executeWithFullArgs: itemDisappearingAction);
        return listView;
    }

    public static T OnItemDisappearing<T>(this T listView, Func<Task>? itemDisappearingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemDisappearingEvent = new AsyncEventCommand<ItemVisibilityEventArgs>(execute: itemDisappearingAction, runInBackground);
        return listView;
    }

    public static T OnItemDisappearing<T>(this T listView, Func<ItemVisibilityEventArgs, Task>? itemDisappearingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemDisappearingEvent = new AsyncEventCommand<ItemVisibilityEventArgs>(executeWithArgs: itemDisappearingAction, runInBackground);
        return listView;
    }

    public static T OnItemDisappearing<T>(this T listView, Func<object?, ItemVisibilityEventArgs, Task>? itemDisappearingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemDisappearingEvent = new AsyncEventCommand<ItemVisibilityEventArgs>(executeWithFullArgs: itemDisappearingAction, runInBackground);
        return listView;
    }

    public static T OnItemSelected<T>(this T listView, Action? itemSelectedAction)
        where T : IListView
    {
        listView.ItemSelectedEvent = new SyncEventCommand<SelectedItemChangedEventArgs>(execute: itemSelectedAction);
        return listView;
    }

    public static T OnItemSelected<T>(this T listView, Action<SelectedItemChangedEventArgs>? itemSelectedAction)
        where T : IListView
    {
        listView.ItemSelectedEvent = new SyncEventCommand<SelectedItemChangedEventArgs>(executeWithArgs: itemSelectedAction);
        return listView;
    }

    public static T OnItemSelected<T>(this T listView, Action<object?, SelectedItemChangedEventArgs>? itemSelectedAction)
        where T : IListView
    {
        listView.ItemSelectedEvent = new SyncEventCommand<SelectedItemChangedEventArgs>(executeWithFullArgs: itemSelectedAction);
        return listView;
    }

    public static T OnItemSelected<T>(this T listView, Func<Task>? itemSelectedAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemSelectedEvent = new AsyncEventCommand<SelectedItemChangedEventArgs>(execute: itemSelectedAction, runInBackground);
        return listView;
    }

    public static T OnItemSelected<T>(this T listView, Func<SelectedItemChangedEventArgs, Task>? itemSelectedAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemSelectedEvent = new AsyncEventCommand<SelectedItemChangedEventArgs>(executeWithArgs: itemSelectedAction, runInBackground);
        return listView;
    }

    public static T OnItemSelected<T>(this T listView, Func<object?, SelectedItemChangedEventArgs, Task>? itemSelectedAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemSelectedEvent = new AsyncEventCommand<SelectedItemChangedEventArgs>(executeWithFullArgs: itemSelectedAction, runInBackground);
        return listView;
    }

    public static T OnItemTapped<T>(this T listView, Action? itemTappedAction)
        where T : IListView
    {
        listView.ItemTappedEvent = new SyncEventCommand<ItemTappedEventArgs>(execute: itemTappedAction);
        return listView;
    }

    public static T OnItemTapped<T>(this T listView, Action<ItemTappedEventArgs>? itemTappedAction)
        where T : IListView
    {
        listView.ItemTappedEvent = new SyncEventCommand<ItemTappedEventArgs>(executeWithArgs: itemTappedAction);
        return listView;
    }

    public static T OnItemTapped<T>(this T listView, Action<object?, ItemTappedEventArgs>? itemTappedAction)
        where T : IListView
    {
        listView.ItemTappedEvent = new SyncEventCommand<ItemTappedEventArgs>(executeWithFullArgs: itemTappedAction);
        return listView;
    }

    public static T OnItemTapped<T>(this T listView, Func<Task>? itemTappedAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemTappedEvent = new AsyncEventCommand<ItemTappedEventArgs>(execute: itemTappedAction, runInBackground);
        return listView;
    }

    public static T OnItemTapped<T>(this T listView, Func<ItemTappedEventArgs, Task>? itemTappedAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemTappedEvent = new AsyncEventCommand<ItemTappedEventArgs>(executeWithArgs: itemTappedAction, runInBackground);
        return listView;
    }

    public static T OnItemTapped<T>(this T listView, Func<object?, ItemTappedEventArgs, Task>? itemTappedAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ItemTappedEvent = new AsyncEventCommand<ItemTappedEventArgs>(executeWithFullArgs: itemTappedAction, runInBackground);
        return listView;
    }

    public static T OnScrolled<T>(this T listView, Action? scrolledAction)
        where T : IListView
    {
        listView.ScrolledEvent = new SyncEventCommand<ScrolledEventArgs>(execute: scrolledAction);
        return listView;
    }

    public static T OnScrolled<T>(this T listView, Action<ScrolledEventArgs>? scrolledAction)
        where T : IListView
    {
        listView.ScrolledEvent = new SyncEventCommand<ScrolledEventArgs>(executeWithArgs: scrolledAction);
        return listView;
    }

    public static T OnScrolled<T>(this T listView, Action<object?, ScrolledEventArgs>? scrolledAction)
        where T : IListView
    {
        listView.ScrolledEvent = new SyncEventCommand<ScrolledEventArgs>(executeWithFullArgs: scrolledAction);
        return listView;
    }

    public static T OnScrolled<T>(this T listView, Func<Task>? scrolledAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ScrolledEvent = new AsyncEventCommand<ScrolledEventArgs>(execute: scrolledAction, runInBackground);
        return listView;
    }

    public static T OnScrolled<T>(this T listView, Func<ScrolledEventArgs, Task>? scrolledAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ScrolledEvent = new AsyncEventCommand<ScrolledEventArgs>(executeWithArgs: scrolledAction, runInBackground);
        return listView;
    }

    public static T OnScrolled<T>(this T listView, Func<object?, ScrolledEventArgs, Task>? scrolledAction, bool runInBackground = false)
        where T : IListView
    {
        listView.ScrolledEvent = new AsyncEventCommand<ScrolledEventArgs>(executeWithFullArgs: scrolledAction, runInBackground);
        return listView;
    }

    public static T OnRefreshing<T>(this T listView, Action? refreshingAction)
        where T : IListView
    {
        listView.RefreshingEvent = new SyncEventCommand<EventArgs>(execute: refreshingAction);
        return listView;
    }

    public static T OnRefreshing<T>(this T listView, Action<EventArgs>? refreshingAction)
        where T : IListView
    {
        listView.RefreshingEvent = new SyncEventCommand<EventArgs>(executeWithArgs: refreshingAction);
        return listView;
    }

    public static T OnRefreshing<T>(this T listView, Action<object?, EventArgs>? refreshingAction)
        where T : IListView
    {
        listView.RefreshingEvent = new SyncEventCommand<EventArgs>(executeWithFullArgs: refreshingAction);
        return listView;
    }

    public static T OnRefreshing<T>(this T listView, Func<Task>? refreshingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.RefreshingEvent = new AsyncEventCommand<EventArgs>(execute: refreshingAction, runInBackground);
        return listView;
    }

    public static T OnRefreshing<T>(this T listView, Func<EventArgs, Task>? refreshingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.RefreshingEvent = new AsyncEventCommand<EventArgs>(executeWithArgs: refreshingAction, runInBackground);
        return listView;
    }

    public static T OnRefreshing<T>(this T listView, Func<object?, EventArgs, Task>? refreshingAction, bool runInBackground = false)
        where T : IListView
    {
        listView.RefreshingEvent = new AsyncEventCommand<EventArgs>(executeWithFullArgs: refreshingAction, runInBackground);
        return listView;
    }
}

public static partial class ListViewStyles
{
    public static Action<IListView>? Default { get; set; }
    public static Dictionary<string, Action<IListView>> Themes { get; } = [];
}