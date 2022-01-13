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
    public partial interface ITemplatedPage
    {
        Microsoft.Maui.Controls.ControlTemplate ControlTemplate { get; set; }


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

        Microsoft.Maui.Controls.ControlTemplate ITemplatedPage.ControlTemplate { get; set; } = (Microsoft.Maui.Controls.ControlTemplate)Microsoft.Maui.Controls.TemplatedPage.ControlTemplateProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsITemplatedPage = (ITemplatedPage)this;
            if (NativeControl.ControlTemplate != thisAsITemplatedPage.ControlTemplate) NativeControl.ControlTemplate = thisAsITemplatedPage.ControlTemplate;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public class TemplatedPage : TemplatedPage<Microsoft.Maui.Controls.TemplatedPage>
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
            templatedpage.ControlTemplate = controlTemplate;
            return templatedpage;
        }


    }
}
