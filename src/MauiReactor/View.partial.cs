using MauiReactor.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor
{
    public partial interface IView
    {
        List<IGestureRecognizer>? GestureRecognizers { get; set; }
        Microsoft.Maui.Controls.LayoutOptions VerticalOptions { get; set; }
        Microsoft.Maui.Controls.LayoutOptions HorizontalOptions { get; set; }
    }

    public abstract partial class View<T>
    {
        List<IGestureRecognizer>? IView.GestureRecognizers { get; set; }
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

        protected override void OnChildAdd(VisualNode node)
        {
            if (node is IGestureRecognizer gestureRecognizer)
            {
                var thisAsIView = (IView)this;
                thisAsIView.GestureRecognizers ??= new List<IGestureRecognizer>();
                thisAsIView.GestureRecognizers.Add(gestureRecognizer);
                return;            
            }

            base.OnChildAdd(node);
        }

        protected override IEnumerable<VisualNode> RenderChildren()
        {
            var thisAsIView = (IView)this;
            if (thisAsIView.GestureRecognizers == null)
            {
                return base.RenderChildren();
            }

            return base.RenderChildren().Concat(thisAsIView.GestureRecognizers.Cast<VisualNode>());
        }

        protected override void OnAddChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.GestureRecognizer gestureRecognizer)
            {
                NativeControl.GestureRecognizers.Add(gestureRecognizer);
            }

            base.OnAddChild(widget, childControl);
        }

        protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
        {
            Validate.EnsureNotNull(NativeControl);

            if (childControl is Microsoft.Maui.Controls.GestureRecognizer gestureRecognizer)
            {
                NativeControl.GestureRecognizers.Remove(gestureRecognizer);
            }

            base.OnRemoveChild(widget, childControl);
        }
    }


    public static partial class ViewExtensions
    {
        public static T HorizontalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IView
        {
            view.HorizontalOptions = layoutOptions;
            return view;
        }

        public static T HStart<T>(this T view) where T : IView
        {
            view.HorizontalOptions = LayoutOptions.Start;
            return view;
        }

        public static T HCenter<T>(this T view) where T : IView
        {
            view.HorizontalOptions = LayoutOptions.Center;
            return view;
        }

        public static T HEnd<T>(this T view) where T : IView
        {
            view.HorizontalOptions = LayoutOptions.End;
            return view;
        }

        public static T HFill<T>(this T view) where T : IView
        {
            view.HorizontalOptions = LayoutOptions.Fill;
            return view;
        }

        public static T VerticalOptions<T>(this T view, LayoutOptions layoutOptions) where T : IView
        {
            view.VerticalOptions = layoutOptions;
            return view;
        }

        public static T VStart<T>(this T view) where T : IView
        {
            view.VerticalOptions = LayoutOptions.Start;
            return view;
        }

        public static T VCenter<T>(this T view) where T : IView
        {
            view.VerticalOptions = LayoutOptions.Center;
            return view;
        }

        public static T VEnd<T>(this T view) where T : IView
        {
            view.VerticalOptions = LayoutOptions.End;
            return view;
        }

        public static T VFill<T>(this T view) where T : IView
        {
            view.VerticalOptions = LayoutOptions.Fill;
            return view;
        }

        public static T OnTapped<T>(this T view, Action action) where T : IView
        {
            view.GestureRecognizers ??= new List<IGestureRecognizer>();
            view.GestureRecognizers.Add(new TapGestureRecognizer(action));

            return view;
        }
    }
}
