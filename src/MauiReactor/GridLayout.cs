using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor
{
    public partial interface IGridLayout
    {
        double RowSpacing { get; set; }
        double ColumnSpacing { get; set; }


    }
    public partial class GridLayout<T> : Layout<T>, IGridLayout where T : Microsoft.Maui.Controls.GridLayout, new()
    {
        public GridLayout()
        {

        }

        public GridLayout(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        double IGridLayout.RowSpacing { get; set; } = (double)Microsoft.Maui.Controls.GridLayout.RowSpacingProperty.DefaultValue;
        double IGridLayout.ColumnSpacing { get; set; } = (double)Microsoft.Maui.Controls.GridLayout.ColumnSpacingProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIGridLayout = (IGridLayout)this;
            if (NativeControl.RowSpacing != thisAsIGridLayout.RowSpacing) NativeControl.RowSpacing = thisAsIGridLayout.RowSpacing;
            if (NativeControl.ColumnSpacing != thisAsIGridLayout.ColumnSpacing) NativeControl.ColumnSpacing = thisAsIGridLayout.ColumnSpacing;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class GridLayout : GridLayout<Microsoft.Maui.Controls.GridLayout>
    {
        public GridLayout()
        {

        }

        public GridLayout(Action<Microsoft.Maui.Controls.GridLayout?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class GridLayoutExtensions
    {
        public static T RowSpacing<T>(this T gridlayout, double rowSpacing) where T : IGridLayout
        {
            gridlayout.RowSpacing = rowSpacing;
            return gridlayout;
        }

        public static T ColumnSpacing<T>(this T gridlayout, double columnSpacing) where T : IGridLayout
        {
            gridlayout.ColumnSpacing = columnSpacing;
            return gridlayout;
        }


    }
}
