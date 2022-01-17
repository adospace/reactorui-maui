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
    public partial interface IGrid : IGridLayout
    {


    }
    public partial class Grid<T> : GridLayout<T>, IGrid where T : Microsoft.Maui.Controls.Grid, new()
    {
        public Grid()
        {

        }

        public Grid(Action<T?> componentRefAction)
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

    public partial class Grid : Grid<Microsoft.Maui.Controls.Grid>
    {
        public Grid()
        {

        }

        public Grid(Action<Microsoft.Maui.Controls.Grid?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class GridExtensions
    {

    }
}
