using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public class Row : Grid
    {
        protected override void OnChildAdded(VisualNode child)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            thisAsIGridLayout.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
            base.OnChildAdded(child);
        }
    }
    public class Column : Grid
    {
        protected override void OnChildAdded(VisualNode child)
        {
            var thisAsIGridLayout = (IGridLayout)this;
            thisAsIGridLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
            base.OnChildAdded(child);
        }
    }

}
