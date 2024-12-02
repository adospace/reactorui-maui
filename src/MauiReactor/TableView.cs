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
public partial interface ITableView : IView
{
}

public partial class TableView<T> : View<T>, ITableView where T : Microsoft.Maui.Controls.TableView, new()
{
    public TableView()
    {
        TableViewStyles.Default?.Invoke(this);
    }

    public TableView(Action<T?> componentRefAction) : base(componentRefAction)
    {
        TableViewStyles.Default?.Invoke(this);
    }

    partial void OnBeginAnimate();
    partial void OnEndAnimate();
    protected override void OnThemeChanged()
    {
        if (ThemeKey != null && TableViewStyles.Themes.TryGetValue(ThemeKey, out var styleAction))
        {
            styleAction(this);
        }

        base.OnThemeChanged();
    }

    partial void Migrated(VisualNode newNode);
    protected override void OnMigrated(VisualNode newNode)
    {
        Migrated(newNode);
        base.OnMigrated(newNode);
    }
}

public partial class TableView : TableView<Microsoft.Maui.Controls.TableView>
{
    public TableView()
    {
    }

    public TableView(Action<Microsoft.Maui.Controls.TableView?> componentRefAction) : base(componentRefAction)
    {
    }
}

public static partial class TableViewExtensions
{
    /*
    
    
    
    
    */
    public static T RowHeight<T>(this T tableView, int rowHeight)
        where T : ITableView
    {
        //tableView.RowHeight = rowHeight;
        tableView.SetProperty(Microsoft.Maui.Controls.TableView.RowHeightProperty, rowHeight);
        return tableView;
    }

    public static T RowHeight<T>(this T tableView, Func<int> rowHeightFunc)
        where T : ITableView
    {
        //tableView.RowHeight = new PropertyValue<int>(rowHeightFunc);
        tableView.SetProperty(Microsoft.Maui.Controls.TableView.RowHeightProperty, new PropertyValue<int>(rowHeightFunc));
        return tableView;
    }

    public static T HasUnevenRows<T>(this T tableView, bool hasUnevenRows)
        where T : ITableView
    {
        //tableView.HasUnevenRows = hasUnevenRows;
        tableView.SetProperty(Microsoft.Maui.Controls.TableView.HasUnevenRowsProperty, hasUnevenRows);
        return tableView;
    }

    public static T HasUnevenRows<T>(this T tableView, Func<bool> hasUnevenRowsFunc)
        where T : ITableView
    {
        //tableView.HasUnevenRows = new PropertyValue<bool>(hasUnevenRowsFunc);
        tableView.SetProperty(Microsoft.Maui.Controls.TableView.HasUnevenRowsProperty, new PropertyValue<bool>(hasUnevenRowsFunc));
        return tableView;
    }
}

public static partial class TableViewStyles
{
    public static Action<ITableView>? Default { get; set; }
    public static Dictionary<string, Action<ITableView>> Themes { get; } = [];
}