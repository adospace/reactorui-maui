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
    public partial interface ITab
    {


    }
    public partial class Tab<T> : ShellSection<T>, ITab where T : Microsoft.Maui.Controls.Tab, new()
    {
        public Tab()
        {

        }

        public Tab(Action<T?> componentRefAction)
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

    public partial class Tab : Tab<Microsoft.Maui.Controls.Tab>
    {
        public Tab()
        {

        }

        public Tab(Action<Microsoft.Maui.Controls.Tab?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class TabExtensions
    {

    }
}
