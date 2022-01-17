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
    public partial interface IStackBase : ILayout
    {
        PropertyValue<double>? Spacing { get; set; }


    }
    public abstract partial class StackBase<T> : Layout<T>, IStackBase where T : Microsoft.Maui.Controls.StackBase, new()
    {
        protected StackBase()
        {

        }

        protected StackBase(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<double>? IStackBase.Spacing { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIStackBase = (IStackBase)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.StackBase.SpacingProperty, thisAsIStackBase.Spacing);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }


    public static partial class StackBaseExtensions
    {
        public static T Spacing<T>(this T stackbase, double spacing) where T : IStackBase
        {
            stackbase.Spacing = new PropertyValue<double>(spacing);
            return stackbase;
        }
        public static T Spacing<T>(this T stackbase, Func<double> spacingFunc) where T : IStackBase
        {
            stackbase.Spacing = new PropertyValue<double>(spacingFunc);
            return stackbase;
        }




    }
}
