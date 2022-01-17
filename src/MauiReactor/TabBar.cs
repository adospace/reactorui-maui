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
    public partial interface ITabBar : IShellItem
    {


    }
    public partial class TabBar<T> : ShellItem<T>, ITabBar where T : Microsoft.Maui.Controls.TabBar, new()
    {
        public TabBar()
        {

        }

        public TabBar(Action<T?> componentRefAction)
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

    public partial class TabBar : TabBar<Microsoft.Maui.Controls.TabBar>
    {
        public TabBar()
        {

        }

        public TabBar(Action<Microsoft.Maui.Controls.TabBar?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class TabBarExtensions
    {

    }
}
