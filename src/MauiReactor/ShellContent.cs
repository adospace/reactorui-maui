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
    public partial interface IShellContent
    {
        object Content { get; set; }
        Microsoft.Maui.Controls.DataTemplate ContentTemplate { get; set; }


    }
    public partial class ShellContent<T> : BaseShellItem<T>, IShellContent where T : Microsoft.Maui.Controls.ShellContent, new()
    {
        public ShellContent()
        {

        }

        public ShellContent(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        object IShellContent.Content { get; set; } = (object)Microsoft.Maui.Controls.ShellContent.ContentProperty.DefaultValue;
        Microsoft.Maui.Controls.DataTemplate IShellContent.ContentTemplate { get; set; } = (Microsoft.Maui.Controls.DataTemplate)Microsoft.Maui.Controls.ShellContent.ContentTemplateProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShellContent = (IShellContent)this;
            if (NativeControl.Content != thisAsIShellContent.Content) NativeControl.Content = thisAsIShellContent.Content;
            if (NativeControl.ContentTemplate != thisAsIShellContent.ContentTemplate) NativeControl.ContentTemplate = thisAsIShellContent.ContentTemplate;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class ShellContent : ShellContent<Microsoft.Maui.Controls.ShellContent>
    {
        public ShellContent()
        {

        }

        public ShellContent(Action<Microsoft.Maui.Controls.ShellContent?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ShellContentExtensions
    {
        public static T Content<T>(this T shellcontent, object content) where T : IShellContent
        {
            shellcontent.Content = content;
            return shellcontent;
        }

        public static T ContentTemplate<T>(this T shellcontent, Microsoft.Maui.Controls.DataTemplate contentTemplate) where T : IShellContent
        {
            shellcontent.ContentTemplate = contentTemplate;
            return shellcontent;
        }


    }
}
