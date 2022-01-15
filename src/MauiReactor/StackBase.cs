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
    public partial interface IStackBase
    {
        double Spacing { get; set; }


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

        double IStackBase.Spacing { get; set; } = (double)Microsoft.Maui.Controls.StackBase.SpacingProperty.DefaultValue;


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIStackBase = (IStackBase)this;
            if (NativeControl.Spacing != thisAsIStackBase.Spacing) NativeControl.Spacing = thisAsIStackBase.Spacing;


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
            stackbase.Spacing = spacing;
            return stackbase;
        }


    }
}
