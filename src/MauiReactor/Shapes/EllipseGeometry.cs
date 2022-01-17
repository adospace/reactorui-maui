using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using MauiReactor.Animations;
//using MauiReactor.Shapes;
using MauiReactor.Internals;

namespace MauiReactor.Shapes
{
    public partial interface IEllipseGeometry : Shapes.IGeometry
    {
        PropertyValue<Microsoft.Maui.Graphics.Point>? Center { get; set; }
        PropertyValue<double>? RadiusX { get; set; }
        PropertyValue<double>? RadiusY { get; set; }


    }
    public partial class EllipseGeometry<T> : Shapes.Geometry<T>, IEllipseGeometry where T : Microsoft.Maui.Controls.Shapes.EllipseGeometry, new()
    {
        public EllipseGeometry()
        {

        }

        public EllipseGeometry(Action<T?> componentRefAction)
            : base(componentRefAction)
        {

        }

        PropertyValue<Microsoft.Maui.Graphics.Point>? IEllipseGeometry.Center { get; set; }
        PropertyValue<double>? IEllipseGeometry.RadiusX { get; set; }
        PropertyValue<double>? IEllipseGeometry.RadiusY { get; set; }


        protected override void OnUpdate()
        {
            OnBeginUpdate();

            Validate.EnsureNotNull(NativeControl);
            var thisAsIEllipseGeometry = (IEllipseGeometry)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.EllipseGeometry.CenterProperty, thisAsIEllipseGeometry.Center);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusXProperty, thisAsIEllipseGeometry.RadiusX);
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.EllipseGeometry.RadiusYProperty, thisAsIEllipseGeometry.RadiusY);


            base.OnUpdate();

            OnEndUpdate();
        }

        partial void OnBeginUpdate();
        partial void OnEndUpdate();


    }

    public partial class EllipseGeometry : EllipseGeometry<Microsoft.Maui.Controls.Shapes.EllipseGeometry>
    {
        public EllipseGeometry()
        {

        }

        public EllipseGeometry(Action<Microsoft.Maui.Controls.Shapes.EllipseGeometry?> componentRefAction)
            : base(componentRefAction)
        {

        }
    }

    public static partial class EllipseGeometryExtensions
    {
        public static T Center<T>(this T ellipsegeometry, Microsoft.Maui.Graphics.Point center) where T : IEllipseGeometry
        {
            ellipsegeometry.Center = new PropertyValue<Microsoft.Maui.Graphics.Point>(center);
            return ellipsegeometry;
        }
        public static T Center<T>(this T ellipsegeometry, Func<Microsoft.Maui.Graphics.Point> centerFunc) where T : IEllipseGeometry
        {
            ellipsegeometry.Center = new PropertyValue<Microsoft.Maui.Graphics.Point>(centerFunc);
            return ellipsegeometry;
        }
        public static T Center<T>(this T ellipsegeometry, double x, double y) where T : IEllipseGeometry
        {
            ellipsegeometry.Center = new PropertyValue<Microsoft.Maui.Graphics.Point>(new Microsoft.Maui.Graphics.Point(x, y));
            return ellipsegeometry;
        }



        public static T RadiusX<T>(this T ellipsegeometry, double radiusX) where T : IEllipseGeometry
        {
            ellipsegeometry.RadiusX = new PropertyValue<double>(radiusX);
            return ellipsegeometry;
        }
        public static T RadiusX<T>(this T ellipsegeometry, Func<double> radiusXFunc) where T : IEllipseGeometry
        {
            ellipsegeometry.RadiusX = new PropertyValue<double>(radiusXFunc);
            return ellipsegeometry;
        }



        public static T RadiusY<T>(this T ellipsegeometry, double radiusY) where T : IEllipseGeometry
        {
            ellipsegeometry.RadiusY = new PropertyValue<double>(radiusY);
            return ellipsegeometry;
        }
        public static T RadiusY<T>(this T ellipsegeometry, Func<double> radiusYFunc) where T : IEllipseGeometry
        {
            ellipsegeometry.RadiusY = new PropertyValue<double>(radiusYFunc);
            return ellipsegeometry;
        }




    }
}
