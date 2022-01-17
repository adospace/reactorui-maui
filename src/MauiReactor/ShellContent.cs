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
    public partial interface IShellContent : IBaseShellItem
    {
        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? ContentTemplate { get; set; }


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

        PropertyValue<Microsoft.Maui.Controls.DataTemplate>? IShellContent.ContentTemplate { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShellContent = (IShellContent)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.ShellContent.ContentTemplateProperty, thisAsIShellContent.ContentTemplate);


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
        public static T ContentTemplate<T>(this T shellcontent, Microsoft.Maui.Controls.DataTemplate contentTemplate) where T : IShellContent
        {
            shellcontent.ContentTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(contentTemplate);
            return shellcontent;
        }
        public static T ContentTemplate<T>(this T shellcontent, Func<Microsoft.Maui.Controls.DataTemplate> contentTemplateFunc) where T : IShellContent
        {
            shellcontent.ContentTemplate = new PropertyValue<Microsoft.Maui.Controls.DataTemplate>(contentTemplateFunc);
            return shellcontent;
        }




    }
}
