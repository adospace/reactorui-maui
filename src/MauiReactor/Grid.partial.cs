using MauiReactor.Internals;

namespace MauiReactor
{
    public partial class Grid
    {
        public Grid(string rows, string columns)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            var converter = new GridLengthTypeConverter();
            foreach (var rowDefinition in rows.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)Validate.EnsureNotNull(converter.ConvertFromInvariantString(_)))
                .Select(_ => new RowDefinition() { Height = _ }))
            {
                thisAsIGridLayout.RowDefinitions.Add(rowDefinition);
            }
            foreach (var columnDefinition in columns.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => (GridLength)Validate.EnsureNotNull(converter.ConvertFromInvariantString(_)))
                .Select(_ => new ColumnDefinition() { Width = _ }))
            {
                thisAsIGridLayout.ColumnDefinitions.Add(columnDefinition);
            }
        }

        public Grid(RowDefinitionCollection rows, ColumnDefinitionCollection columns)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            thisAsIGridLayout.RowDefinitions = rows;
            thisAsIGridLayout.ColumnDefinitions = columns;
        }

        public Grid(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            foreach (var row in rows)
                thisAsIGridLayout.RowDefinitions.Add(row);
            foreach (var column in columns)
                thisAsIGridLayout.ColumnDefinitions.Add(column);
        }
    }

    public class Column : Grid
    {
        protected override void OnChildAdded(VisualNode child)
        {
            if (child is MauiReactor.IView)
            {
                var thisAsIGridLayout = (IGridLayout)this;
                thisAsIGridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
                ((IVisualNodeWithAttachedProperties)child).GridRow(thisAsIGridLayout.RowDefinitions.Count - 1);
            }
            base.OnChildAdded(child);
        }
    }
    public class Row : Grid
    {
        protected override void OnChildAdded(VisualNode child)
        {
            if (child is MauiReactor.IView)
            {
                var thisAsIGridLayout = (IGridLayout)this;
                thisAsIGridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
                ((IVisualNodeWithAttachedProperties)child).GridColumn(thisAsIGridLayout.ColumnDefinitions.Count - 1);
            }
            base.OnChildAdded(child);
        }
    }

}
