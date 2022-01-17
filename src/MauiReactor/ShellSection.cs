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
    public partial interface IShellSection : IShellGroupItem
    {


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



        protected override void OnUpdate()
        {
            OnBeginUpdate();

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

    }
}
