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
    public partial interface IShellSection
    {
        Microsoft.Maui.Controls.ShellContent CurrentItem { get; set; }


    }
    public partial class ShellSection<T> : ShellGroupItem<T>, IShellSection where T : Microsoft.Maui.Controls.ShellSection, new()
    {
        public ShellSection()
        {

        }

        public ShellSection(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        Microsoft.Maui.Controls.ShellContent IShellSection.CurrentItem { get; set; } = (Microsoft.Maui.Controls.ShellContent)Microsoft.Maui.Controls.ShellSection.CurrentItemProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIShellSection = (IShellSection)this;
            if (NativeControl.CurrentItem != thisAsIShellSection.CurrentItem) NativeControl.CurrentItem = thisAsIShellSection.CurrentItem;


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class ShellSection : ShellSection<Microsoft.Maui.Controls.ShellSection>
    {
        public ShellSection()
        {

        }

        public ShellSection(Action<Microsoft.Maui.Controls.ShellSection?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class ShellSectionExtensions
    {
        public static T CurrentItem<T>(this T shellsection, Microsoft.Maui.Controls.ShellContent currentItem) where T : IShellSection
        {
            shellsection.CurrentItem = currentItem;
            return shellsection;
        }


    }
}
