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
    public partial interface IContentPage
    {
        Microsoft.Maui.Controls.View Content { get; set; }


    }

    public partial class ContentPage<T> : TemplatedPage<T>, IContentPage where T : Microsoft.Maui.Controls.ContentPage, new()
    {
        public ContentPage()
        {

        }

        public ContentPage(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.View IContentPage.Content { get; set; } = (Microsoft.Maui.Controls.View)Microsoft.Maui.Controls.ContentPage.ContentProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIContentPage = (IContentPage)this;
            if (NativeControl.Content != thisAsIContentPage.Content) NativeControl.Content = thisAsIContentPage.Content;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public class ContentPage : ContentPage<Microsoft.Maui.Controls.ContentPage>
    {
        public ContentPage()
        {

        }

        public ContentPage(Action<Microsoft.Maui.Controls.ContentPage?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ContentPageExtensions
    {
        public static T Content<T>(this T contentpage, Microsoft.Maui.Controls.View content) where T : IContentPage
        {
            contentpage.Content = content;
            return contentpage;
        }


    }
}
