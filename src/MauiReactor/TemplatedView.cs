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
    public partial interface ITemplatedView : Compatibility.ILayout
    {
        PropertyValue<Microsoft.Maui.Controls.ControlTemplate>? ControlTemplate { get; set; }


    }
    public partial class TemplatedView<T> : Compatibility.Layout<T>, ITemplatedView where T : Microsoft.Maui.Controls.TemplatedView, new()
    {
        public TemplatedView()
        {

        }

        public TemplatedView(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Controls.ControlTemplate>? ITemplatedView.ControlTemplate { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsITemplatedView = (ITemplatedView)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TemplatedView.ControlTemplateProperty, thisAsITemplatedView.ControlTemplate);


            base.OnUpdate();

            OnEndUpdate();
        }


        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class TemplatedView : TemplatedView<Microsoft.Maui.Controls.TemplatedView>
    {
        public TemplatedView()
        {

        }

        public TemplatedView(Action<Microsoft.Maui.Controls.TemplatedView?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class TemplatedViewExtensions
    {
        public static T ControlTemplate<T>(this T templatedView, Microsoft.Maui.Controls.ControlTemplate controlTemplate) where T : ITemplatedView
        {
            templatedView.ControlTemplate = new PropertyValue<Microsoft.Maui.Controls.ControlTemplate>(controlTemplate);
            return templatedView;
        }

        public static T ControlTemplate<T>(this T templatedView, Func<Microsoft.Maui.Controls.ControlTemplate> controlTemplateFunc) where T : ITemplatedView
        {
            templatedView.ControlTemplate = new PropertyValue<Microsoft.Maui.Controls.ControlTemplate>(controlTemplateFunc);
            return templatedView;
        }




    }
}
