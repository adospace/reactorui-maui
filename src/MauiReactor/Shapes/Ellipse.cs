// <auto-generated />
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
using MauiReactor.Shapes;
using MauiReactor.Internals;

#nullable enable

namespace MauiReactor.Shapes
{
    public partial interface IEllipse : Shapes.IShape
    {


    }

    public sealed partial class Ellipse : Shapes.Shape<Microsoft.Maui.Controls.Shapes.Ellipse>, IEllipse
    {
        public Ellipse()
        {

        }

        public Ellipse(Action<Microsoft.Maui.Controls.Shapes.Ellipse?> componentRefAction)
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


    public static partial class EllipseExtensions
    {

    }
}