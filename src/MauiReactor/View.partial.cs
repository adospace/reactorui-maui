using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IView
    {
        Microsoft.Maui.Controls.LayoutOptions VerticalOptions { get; set; }
        Microsoft.Maui.Controls.LayoutOptions HorizontalOptions { get; set; }
    }

    public abstract partial class View<T> : VisualElement<T>, IView where T : Microsoft.Maui.Controls.View, new()
    {
        Microsoft.Maui.Controls.LayoutOptions IView.VerticalOptions { get; set; } = (Microsoft.Maui.Controls.LayoutOptions)Microsoft.Maui.Controls.View.VerticalOptionsProperty.DefaultValue;
        Microsoft.Maui.Controls.LayoutOptions IView.HorizontalOptions { get; set; } = (Microsoft.Maui.Controls.LayoutOptions)Microsoft.Maui.Controls.View.HorizontalOptionsProperty.DefaultValue;


        partial void OnBeginUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIView = (IView)this;
            if (NativeControl.VerticalOptions.Alignment != thisAsIView.VerticalOptions.Alignment ||
                NativeControl.VerticalOptions.Expands != thisAsIView.VerticalOptions.Expands) 
                NativeControl.VerticalOptions = thisAsIView.VerticalOptions;
            if (NativeControl.HorizontalOptions.Alignment != thisAsIView.HorizontalOptions.Alignment ||
                NativeControl.HorizontalOptions.Expands != thisAsIView.HorizontalOptions.Expands) 
                NativeControl.HorizontalOptions = thisAsIView.HorizontalOptions;
        }
    }


    public static partial class ViewExtensions
    {
        public static T VerticalOptions<T>(this T view, Microsoft.Maui.Controls.LayoutOptions verticalOptions) where T : IView
        {
            view.VerticalOptions = verticalOptions;
            return view;
        }

        public static T HorizontalOptions<T>(this T view, Microsoft.Maui.Controls.LayoutOptions horizontalOptions) where T : IView
        {
            view.HorizontalOptions = horizontalOptions;
            return view;
        }


    }
}
