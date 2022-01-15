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
    public partial interface INavigableElement
    {
        Microsoft.Maui.Controls.Style Style { get; set; }


    }
    public abstract partial class NavigableElement<T> : Element<T>, INavigableElement where T : Microsoft.Maui.Controls.NavigableElement, new()
    {
        protected NavigableElement()
        {

        }

        protected NavigableElement(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.Style INavigableElement.Style { get; set; } = (Microsoft.Maui.Controls.Style)Microsoft.Maui.Controls.NavigableElement.StyleProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsINavigableElement = (INavigableElement)this;
            if (NativeControl.Style != thisAsINavigableElement.Style) NativeControl.Style = thisAsINavigableElement.Style;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }


    public static partial class NavigableElementExtensions
    {
        public static T Style<T>(this T navigableelement, Microsoft.Maui.Controls.Style style) where T : INavigableElement
        {
            navigableelement.Style = style;
            return navigableelement;
        }


    }
}
