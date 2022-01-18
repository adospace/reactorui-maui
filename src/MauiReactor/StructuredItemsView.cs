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
    public partial interface IStructuredItemsView : IItemsView
    {
        PropertyValue<object>? Header { get; set; }
        PropertyValue<object>? Footer { get; set; }
        PropertyValue<Microsoft.Maui.Controls.IItemsLayout>? ItemsLayout { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ItemSizingStrategy>? ItemSizingStrategy { get; set; }


    }
    public partial class StructuredItemsView<T> : ItemsView<T>, IStructuredItemsView where T : Microsoft.Maui.Controls.StructuredItemsView, new()
    {
        public StructuredItemsView()
        {

        }

        public StructuredItemsView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<object>? IStructuredItemsView.Header { get; set; }
        PropertyValue<object>? IStructuredItemsView.Footer { get; set; }
        PropertyValue<Microsoft.Maui.Controls.IItemsLayout>? IStructuredItemsView.ItemsLayout { get; set; }
        PropertyValue<Microsoft.Maui.Controls.ItemSizingStrategy>? IStructuredItemsView.ItemSizingStrategy { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIStructuredItemsView = (IStructuredItemsView)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.StructuredItemsView.HeaderProperty, thisAsIStructuredItemsView.Header);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.StructuredItemsView.FooterProperty, thisAsIStructuredItemsView.Footer);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.StructuredItemsView.ItemsLayoutProperty, thisAsIStructuredItemsView.ItemsLayout);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.StructuredItemsView.ItemSizingStrategyProperty, thisAsIStructuredItemsView.ItemSizingStrategy);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class StructuredItemsView : StructuredItemsView<Microsoft.Maui.Controls.StructuredItemsView>
    {
        public StructuredItemsView()
        {

        }

        public StructuredItemsView(Action<Microsoft.Maui.Controls.StructuredItemsView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class StructuredItemsViewExtensions
    {
        public static T Header<T>(this T structureditemsview, object header) where T : IStructuredItemsView
        {
            structureditemsview.Header = new PropertyValue<object>(header);
            return structureditemsview;
        }
        public static T Header<T>(this T structureditemsview, Func<object> headerFunc) where T : IStructuredItemsView
        {
            structureditemsview.Header = new PropertyValue<object>(headerFunc);
            return structureditemsview;
        }



        public static T Footer<T>(this T structureditemsview, object footer) where T : IStructuredItemsView
        {
            structureditemsview.Footer = new PropertyValue<object>(footer);
            return structureditemsview;
        }
        public static T Footer<T>(this T structureditemsview, Func<object> footerFunc) where T : IStructuredItemsView
        {
            structureditemsview.Footer = new PropertyValue<object>(footerFunc);
            return structureditemsview;
        }



        public static T ItemsLayout<T>(this T structureditemsview, Microsoft.Maui.Controls.IItemsLayout itemsLayout) where T : IStructuredItemsView
        {
            structureditemsview.ItemsLayout = new PropertyValue<Microsoft.Maui.Controls.IItemsLayout>(itemsLayout);
            return structureditemsview;
        }
        public static T ItemsLayout<T>(this T structureditemsview, Func<Microsoft.Maui.Controls.IItemsLayout> itemsLayoutFunc) where T : IStructuredItemsView
        {
            structureditemsview.ItemsLayout = new PropertyValue<Microsoft.Maui.Controls.IItemsLayout>(itemsLayoutFunc);
            return structureditemsview;
        }



        public static T ItemSizingStrategy<T>(this T structureditemsview, Microsoft.Maui.Controls.ItemSizingStrategy itemSizingStrategy) where T : IStructuredItemsView
        {
            structureditemsview.ItemSizingStrategy = new PropertyValue<Microsoft.Maui.Controls.ItemSizingStrategy>(itemSizingStrategy);
            return structureditemsview;
        }
        public static T ItemSizingStrategy<T>(this T structureditemsview, Func<Microsoft.Maui.Controls.ItemSizingStrategy> itemSizingStrategyFunc) where T : IStructuredItemsView
        {
            structureditemsview.ItemSizingStrategy = new PropertyValue<Microsoft.Maui.Controls.ItemSizingStrategy>(itemSizingStrategyFunc);
            return structureditemsview;
        }




    }
}
