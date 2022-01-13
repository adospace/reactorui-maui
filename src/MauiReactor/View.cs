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
    public partial interface IView
    {
        Microsoft.Maui.Thickness Margin { get; set; }


    }

    public abstract partial class View<T> : VisualElement<T>, IView where T : Microsoft.Maui.Controls.View, new()
    {
        protected View()
        {

        }

        protected View(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Thickness IView.Margin { get; set; } = (Microsoft.Maui.Thickness)Microsoft.Maui.Controls.View.MarginProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIView = (IView)this;
            if (NativeControl.Margin != thisAsIView.Margin) NativeControl.Margin = thisAsIView.Margin;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }


    public static partial class ViewExtensions
    {
        public static T Margin<T>(this T view, Microsoft.Maui.Thickness margin) where T : IView
        {
            view.Margin = margin;
            return view;
        }
        public static T Margin<T>(this T view, double leftRight, double topBottom) where T : IView
        {
            view.Margin = new Thickness(leftRight, topBottom);
            return view;
        }
        public static T Margin<T>(this T view, double uniformSize) where T : IView
        {
            view.Margin = new Thickness(uniformSize);
            return view;
        }


    }
}
