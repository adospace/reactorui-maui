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
    public partial interface IGestureRecognizer : IElement
    {


    }
    public partial class GestureRecognizer<T> : Element<T>, IGestureRecognizer where T : Microsoft.Maui.Controls.GestureRecognizer, new()
    {
        public GestureRecognizer()
        {

        }

        public GestureRecognizer(Action<T?> componentRefAction)
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

    public partial class GestureRecognizer : GestureRecognizer<Microsoft.Maui.Controls.GestureRecognizer>
    {
        public GestureRecognizer()
        {

        }

        public GestureRecognizer(Action<Microsoft.Maui.Controls.GestureRecognizer?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class GestureRecognizerExtensions
    {

    }
}
