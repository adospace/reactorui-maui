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
    public partial interface IGridLayout : ILayout
    {
        PropertyValue<double>? RowSpacing { get; set; }
        PropertyValue<double>? ColumnSpacing { get; set; }


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

        PropertyValue<double>? IGridLayout.RowSpacing { get; set; }
        PropertyValue<double>? IGridLayout.ColumnSpacing { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIGridLayout = (IGridLayout)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.GridLayout.RowSpacingProperty, thisAsIGridLayout.RowSpacing);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.GridLayout.ColumnSpacingProperty, thisAsIGridLayout.ColumnSpacing);


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
            gridlayout.RowSpacing = new PropertyValue<double>(rowSpacing);
            return gridlayout;
        }
        public static T RowSpacing<T>(this T gridlayout, Func<double> rowSpacingFunc) where T : IGridLayout
        {
            gridlayout.RowSpacing = new PropertyValue<double>(rowSpacingFunc);
            return gridlayout;
        }



        public static T ColumnSpacing<T>(this T gridlayout, double columnSpacing) where T : IGridLayout
        {
            gridlayout.ColumnSpacing = new PropertyValue<double>(columnSpacing);
            return gridlayout;
        }
        public static T ColumnSpacing<T>(this T gridlayout, Func<double> columnSpacingFunc) where T : IGridLayout
        {
            gridlayout.ColumnSpacing = new PropertyValue<double>(columnSpacingFunc);
            return gridlayout;
        }




    }
}
