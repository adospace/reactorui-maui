using MauiReactor.Internals;

namespace MauiReactor;

public partial interface IGrid
{
    ColumnDefinitionCollection ColumnDefinitions { get; set; }
    RowDefinitionCollection RowDefinitions { get; set; }
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

    ColumnDefinitionCollection IGrid.ColumnDefinitions { get; set; } = [];
    RowDefinitionCollection IGrid.RowDefinitions { get; set; } = [];

    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);
        var thisAsIGrid = (IGrid)this;
        if (!NativeControl.ColumnDefinitions.IsEqualTo(thisAsIGrid.ColumnDefinitions)) NativeControl.ColumnDefinitions = thisAsIGrid.ColumnDefinitions;
        if (!NativeControl.RowDefinitions.IsEqualTo(thisAsIGrid.RowDefinitions)) NativeControl.RowDefinitions = thisAsIGrid.RowDefinitions;
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

    public static T Rows<T>(this T grid, string rows) where T : IGrid
    {
        foreach (var rowDefinition in rows.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
            .Select(_ => new RowDefinition() { Height = _ }))
        {
            grid.RowDefinitions.Add(rowDefinition);
        }

        return grid;
    }

    public static T Columns<T>(this T grid, string columns) where T : IGrid
    {
        foreach (var columnDefinition in columns.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => (GridLength)Validate.EnsureNotNull(_gridLengthTypeConverter.ConvertFromInvariantString(_)))
            .Select(_ => new ColumnDefinition() { Width = _ }))
        {
            grid.ColumnDefinitions.Add(columnDefinition);
        }

        return grid;
    }

    public static T Rows<T>(this T grid, RowDefinitionCollection rowDefinitions) where T : IGrid
    {
        grid.RowDefinitions = rowDefinitions;
        return grid;
    }

    public static T Columns<T>(this T grid, ColumnDefinitionCollection columnDefinitions) where T : IGrid
    {
        grid.ColumnDefinitions = columnDefinitions;
        return grid;
    }

    public static T Rows<T>(this T grid, IEnumerable<RowDefinition> rows) where T : IGrid
    {
        foreach (var row in rows)
            grid.RowDefinitions.Add(row);
        return grid;
    }

    public static T Columns<T>(this T grid, IEnumerable<ColumnDefinition> columns) where T : IGrid
    {
        foreach (var column in columns)
            grid.ColumnDefinitions.Add(column);
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
    public static Grid Grid(string rows, string columns, params VisualNode?[]? children)
        => Grid(children).Rows(rows).Columns(columns);

    public static Grid Grid(RowDefinitionCollection rows, ColumnDefinitionCollection columns, params VisualNode?[]? children)
        => Grid(children).Rows(rows).Columns(columns);

    public static Grid Grid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns, params VisualNode?[]? children)
        => Grid(children).Rows(rows).Columns(columns);

}
