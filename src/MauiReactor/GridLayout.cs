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

        protected override void OnAnimate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIGridLayout = (IGridLayout)this;

            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.GridLayout.RowSpacingProperty, thisAsIGridLayout.RowSpacing);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.GridLayout.ColumnSpacingProperty, thisAsIGridLayout.ColumnSpacing);

            base.OnAnimate();
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
        public static T RowSpacing<T>(this T gridLayout, double rowSpacing, RxDoubleAnimation? customAnimation = null) where T : IGridLayout
        {
            gridLayout.RowSpacing = new PropertyValue<double>(rowSpacing);
            gridLayout.AppendAnimatable(Microsoft.Maui.Controls.GridLayout.RowSpacingProperty, customAnimation ?? new RxDoubleAnimation(rowSpacing), v => gridLayout.RowSpacing = new PropertyValue<double>(v.CurrentValue()));
            return gridLayout;
        }

        public static T RowSpacing<T>(this T gridLayout, Func<double> rowSpacingFunc) where T : IGridLayout
        {
            gridLayout.RowSpacing = new PropertyValue<double>(rowSpacingFunc);
            return gridLayout;
        }



        public static T ColumnSpacing<T>(this T gridLayout, double columnSpacing, RxDoubleAnimation? customAnimation = null) where T : IGridLayout
        {
            gridLayout.ColumnSpacing = new PropertyValue<double>(columnSpacing);
            gridLayout.AppendAnimatable(Microsoft.Maui.Controls.GridLayout.ColumnSpacingProperty, customAnimation ?? new RxDoubleAnimation(columnSpacing), v => gridLayout.ColumnSpacing = new PropertyValue<double>(v.CurrentValue()));
            return gridLayout;
        }

        public static T ColumnSpacing<T>(this T gridLayout, Func<double> columnSpacingFunc) where T : IGridLayout
        {
            gridLayout.ColumnSpacing = new PropertyValue<double>(columnSpacingFunc);
            return gridLayout;
        }




    }
}
