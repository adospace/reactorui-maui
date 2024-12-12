using MauiReactor.Internals;
using Microsoft.Maui.Controls;

namespace MauiReactor;

public partial interface IGrid
{
    string? ColumnDefinitions { get; set; }
    string? RowDefinitions { get; set; }
}

public partial class Grid<T>
{
    public Grid(string rows, string columns)
    {
        var thisAsIGrid = (IGrid)this;
        thisAsIGrid.Rows(rows).Columns(columns);
    }

    public Grid(RowDefinitionCollection rows, ColumnDefinitionCollection columns)
    {
        var thisAsIGrid = (IGrid)this;
        thisAsIGrid.Rows(rows).Columns(columns);
    }

    public Grid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns)
    {
        var thisAsIGrid = (IGrid)this;
        thisAsIGrid.Rows(rows).Columns(columns);
    }

    string? IGrid.ColumnDefinitions { get; set; }
    string? IGrid.RowDefinitions { get; set; }

    private static readonly BindableProperty _mauiReactorGridRows = BindableProperty.CreateAttached(
        nameof(_mauiReactorGridRows),
        typeof(string), 
        typeof(Grid<T>), 
        null);

    private static readonly BindableProperty _mauiReactorGridColumns = BindableProperty.CreateAttached(
        nameof(_mauiReactorGridColumns),
        typeof(string), 
        typeof(Grid<T>), 
        null);

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIGrid = (IGrid)this;
        //if (!NativeControl.ColumnDefinitions.IsEqualTo(thisAsIGrid.ColumnDefinitions)) NativeControl.ColumnDefinitions = thisAsIGrid.ColumnDefinitions;
        //if (!NativeControl.RowDefinitions.IsEqualTo(thisAsIGrid.RowDefinitions)) NativeControl.RowDefinitions = thisAsIGrid.RowDefinitions;

        //foreach (var rowDefinition in rows.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
        //    .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
        //    .Select(_ => new RowDefinition() { Height = _ }))
        //{
        //    grid.RowDefinitions.Add(rowDefinition);
        //}

        var rowsOnNativeControl = (string?)NativeControl.GetValue(_mauiReactorGridRows);
        if (rowsOnNativeControl != thisAsIGrid.RowDefinitions)
        {
            GridExtensions.SetRowDefinitions(NativeControl, thisAsIGrid.RowDefinitions);
            NativeControl.SetValue(_mauiReactorGridColumns, thisAsIGrid.RowDefinitions);
        }

        var columnsOnNativeControl = (string?)NativeControl.GetValue(_mauiReactorGridColumns);
        if (columnsOnNativeControl != thisAsIGrid.ColumnDefinitions)
        {
            GridExtensions.SetColumnDefinitions(NativeControl, thisAsIGrid.ColumnDefinitions);
            NativeControl.SetValue(_mauiReactorGridColumns, thisAsIGrid.ColumnDefinitions);
        }

        base.OnUpdate();
    }
}

public partial class Grid
{
    public Grid(string rows, string columns)
        : base(rows, columns) { }

    public Grid(RowDefinitionCollection rows, ColumnDefinitionCollection columns)
        : base(rows, columns) { }

    public Grid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns)
        : base(rows, columns) { }
}

internal static class RowColumnDefinitionExtensions
{
    public static bool IsEqualTo(this RowDefinitionCollection rows, RowDefinitionCollection otherRows)
    {
        if (rows == null && otherRows == null)
            return true;
        if (rows == null)
            return false;
        if (otherRows == null)
            return false;
        if (rows.Count != otherRows.Count)
            return false;

        for (int i = 0; i < rows.Count; i++)
        {
            if (!rows[i].Height.Equals(otherRows[i].Height))
                return false;
        }

        return true;
    }

    public static bool IsEqualTo(this ColumnDefinitionCollection columns, ColumnDefinitionCollection otherColumns)
    {
        if (columns == null && otherColumns == null)
            return true;
        if (columns == null)
            return false;
        if (otherColumns == null)
            return false;
        if (columns.Count != otherColumns.Count)
            return false;

        for (int i = 0; i < columns.Count; i++)
        {
            if (!columns[i].Width.Equals(otherColumns[i].Width))
                return false;
        }

        return true;
    }
}

public static partial class GridExtensions
{
    private static readonly GridLengthTypeConverter _gridLengthTypeConverter = new GridLengthTypeConverter();

    internal static void SetRowDefinitions(this Microsoft.Maui.Controls.Grid grid, string? rowDefinitions)
    {
        grid.RowDefinitions.Clear();
        if (rowDefinitions == null)
            return;
        foreach (var rowDefinition in rowDefinitions.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
            .Select(_ => new RowDefinition() { Height = _ }))
        {
            grid.RowDefinitions.Add(rowDefinition);
        }
    }

    internal static void SetColumnDefinitions(this Microsoft.Maui.Controls.Grid grid, string? columnDefinitions)
    {
        grid.ColumnDefinitions.Clear();
        if (columnDefinitions == null)
            return;
        foreach (var columnDefinition in columnDefinitions.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
            .Select(_ => new ColumnDefinition() { Width = _ }))
        {
            grid.ColumnDefinitions.Add(columnDefinition);
        }
    }

    public static T Rows<T>(this T grid, string rows) where T : IGrid
    {
        //foreach (var rowDefinition in rows.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
        //    .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
        //    .Select(_ => new RowDefinition() { Height = _ }))
        //{
        //    grid.RowDefinitions.Add(rowDefinition);
        //}
        grid.RowDefinitions = rows;

        return grid;
    }

    public static T Columns<T>(this T grid, string columns) where T : IGrid
    {
        //foreach (var columnDefinition in columns.Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
        //    .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
        //    .Select(_ => new ColumnDefinition() { Width = _ }))
        //{
        //    grid.ColumnDefinitions.Add(columnDefinition);
        //}
        grid.ColumnDefinitions = columns;

        return grid;
    }

    public static T Rows<T>(this T grid, RowDefinitionCollection rowDefinitions) where T : IGrid
    {
        grid.RowDefinitions = string.Join(',', rowDefinitions.Select(_ => _gridLengthTypeConverter.ConvertToInvariantString(_.Height)));
        return grid;
    }

    public static T Columns<T>(this T grid, ColumnDefinitionCollection columnDefinitions) where T : IGrid
    {
        grid.ColumnDefinitions = string.Join(',', columnDefinitions.Select(_ => _gridLengthTypeConverter.ConvertToInvariantString(_.Width)));
        return grid;
    }

    public static T Rows<T>(this T grid, IEnumerable<RowDefinition> rows) where T : IGrid
    {
        //foreach (var row in rows)
        //    grid.RowDefinitions.Add(row);
        grid.RowDefinitions = string.Join(',', rows.Select(_ => _gridLengthTypeConverter.ConvertToInvariantString(_.Height)));
        return grid;
    }

    public static T Columns<T>(this T grid, IEnumerable<ColumnDefinition> columns) where T : IGrid
    {
        //foreach (var column in columns)
        //    grid.ColumnDefinitions.Add(column);
        grid.ColumnDefinitions = string.Join(',', columns.Select(_ => _gridLengthTypeConverter.ConvertToInvariantString(_.Width)));
        return grid;
    }


    public static T GridRow<T>(this T visualNodeWithAttachedProperties, int rowIndex) where T : IVisualNodeWithAttachedProperties
    {
        visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.Grid.RowProperty, rowIndex);

        return visualNodeWithAttachedProperties;
    }

    public static T GridRowSpan<T>(this T visualNodeWithAttachedProperties, int rowSpan) where T : IVisualNodeWithAttachedProperties
    {
        visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.Grid.RowSpanProperty, rowSpan);

        return visualNodeWithAttachedProperties;
    }

    public static T GridColumn<T>(this T visualNodeWithAttachedProperties, int columnIndex) where T : IVisualNodeWithAttachedProperties
    {
        visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.Grid.ColumnProperty, columnIndex);

        return visualNodeWithAttachedProperties;
    }

    public static T GridColumnSpan<T>(this T visualNodeWithAttachedProperties, int columnSpan) where T : IVisualNodeWithAttachedProperties
    {
        visualNodeWithAttachedProperties.SetProperty(Microsoft.Maui.Controls.Grid.ColumnSpanProperty, columnSpan);

        return visualNodeWithAttachedProperties;
    }

}


public partial class Component
{
    public static Grid Grid(string rows, string columns, params IEnumerable<VisualNode?>? children)
        => Grid(children).Rows(rows).Columns(columns);

    public static Grid Grid(RowDefinitionCollection rows, ColumnDefinitionCollection columns, params IEnumerable<VisualNode?>? children)
        => Grid(children).Rows(rows).Columns(columns);

    public static Grid Grid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns, params IEnumerable<VisualNode?>? children)
        => Grid(children).Rows(rows).Columns(columns);

}
