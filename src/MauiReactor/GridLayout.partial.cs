using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IGridLayout
    {
        ColumnDefinitionCollection ColumnDefinitions { get; set; }
        RowDefinitionCollection RowDefinitions { get; set; }
    }

    public partial class GridLayout<T>
    {
        public GridLayout(string rows, string columns)
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

        public GridLayout(RowDefinitionCollection rows, ColumnDefinitionCollection columns)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            thisAsIGridLayout.RowDefinitions = rows;
            thisAsIGridLayout.ColumnDefinitions = columns;
        }

        public GridLayout(IEnumerable<RowDefinition> rows, IEnumerable<ColumnDefinition> columns)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            foreach (var row in rows)
                thisAsIGridLayout.RowDefinitions.Add(row);
            foreach (var column in columns)
                thisAsIGridLayout.ColumnDefinitions.Add(column);
        }

        ColumnDefinitionCollection IGridLayout.ColumnDefinitions { get; set; } = new ColumnDefinitionCollection();
        RowDefinitionCollection IGridLayout.RowDefinitions { get; set; } = new RowDefinitionCollection();

        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIGridLayout = (IGridLayout)this;
            if (!NativeControl.ColumnDefinitions.IsEqualTo(thisAsIGridLayout.ColumnDefinitions)) NativeControl.ColumnDefinitions = thisAsIGridLayout.ColumnDefinitions;
            if (!NativeControl.RowDefinitions.IsEqualTo(thisAsIGridLayout.RowDefinitions)) NativeControl.RowDefinitions = thisAsIGridLayout.RowDefinitions;
        }
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
}
