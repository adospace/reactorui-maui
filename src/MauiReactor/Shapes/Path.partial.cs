using MauiReactor.Internals;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Shapes
{
    public partial interface IPath
    {
        PropertyValue<Geometry>? Data { get; set; }
    }

    public partial class Path
    {
        public Path(string data)
        {
            this.Data(data);
        }

        PropertyValue<Geometry>? IPath.Data { get; set; }

        partial void OnEndUpdate()
        {
            Validate.EnsureNotNull(NativeControl);
            var thisAsIPath = (IPath)this;
            SetPropertyValue(NativeControl, Microsoft.Maui.Controls.Shapes.Path.DataProperty, thisAsIPath.Data);
        }
    }

    public partial class PathExtensions
    {
        public static T Data<T>(this T path, string data)
            where T : IPath
        {
            path.Data = new PropertyValue<Geometry>((Geometry)Validate.EnsureNotNull(new PathGeometryConverter().ConvertFromInvariantString(data)));
            return path;
        }
    }
}
