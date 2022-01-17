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
    public partial interface ITemplatedPage : IPage
    {
        PropertyValue<Microsoft.Maui.Controls.ControlTemplate>? ControlTemplate { get; set; }


    }
    public partial class TemplatedPage<T> : Page<T>, ITemplatedPage where T : Microsoft.Maui.Controls.TemplatedPage, new()
    {
        public TemplatedPage()
        {

        }

        public TemplatedPage(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Controls.ControlTemplate>? ITemplatedPage.ControlTemplate { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsITemplatedPage = (ITemplatedPage)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.TemplatedPage.ControlTemplateProperty, thisAsITemplatedPage.ControlTemplate);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class TemplatedPage : TemplatedPage<Microsoft.Maui.Controls.TemplatedPage>
    {
        public TemplatedPage()
        {

        }

        public TemplatedPage(Action<Microsoft.Maui.Controls.TemplatedPage?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class TemplatedPageExtensions
    {
        public static T ControlTemplate<T>(this T templatedpage, Microsoft.Maui.Controls.ControlTemplate controlTemplate) where T : ITemplatedPage
        {
            templatedpage.ControlTemplate = new PropertyValue<Microsoft.Maui.Controls.ControlTemplate>(controlTemplate);
            return templatedpage;
        }
        public static T ControlTemplate<T>(this T templatedpage, Func<Microsoft.Maui.Controls.ControlTemplate> controlTemplateFunc) where T : ITemplatedPage
        {
            templatedpage.ControlTemplate = new PropertyValue<Microsoft.Maui.Controls.ControlTemplate>(controlTemplateFunc);
            return templatedpage;
        }




    }
}
