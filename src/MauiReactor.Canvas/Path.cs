using MauiReactor.Internals;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.Path))]
public partial class Path { }


public partial class PathExtensions
{
    //public static T Data<T>(this T path, string? data) where T : IPath
    //{
    //    path.Data = new PropertyValue<Microsoft.Maui.Graphics.PathF?>(ConvertFromPathString(data));
    //    return path;
    //}

    //private static PathF? ConvertFromPathString(string? pathString)
    //{
    //    if (string.IsNullOrWhiteSpace(pathString))
    //    {
    //        return null;
    //    }

    //    var pathData = (PathGeometry)Validate.EnsureNotNull(new PathGeometryConverter().ConvertFromInvariantString(pathString));

    //    var resPath = new PathF();

    //    foreach (var pathFigure in pathData.Figures)
    //    {
    //        var currentPoint = pathFigure.StartPoint;
    //        resPath.MoveTo(pathFigure.StartPoint);
    //        foreach (var pathSegment in pathFigure.Segments)
    //        {
    //            if (pathSegment is LineSegment lineSegment)
    //            {
    //                resPath.LineTo(lineSegment.Point);
    //                currentPoint = lineSegment.Point;
    //            }
    //            else if (pathSegment is BezierSegment bezierSegment)
    //            {
    //                resPath.CurveTo(bezierSegment.Point1, bezierSegment.Point2, bezierSegment.Point3);
    //                currentPoint = bezierSegment.Point3;
    //            }
    //            else if (pathSegment is QuadraticBezierSegment quadraticBezierSegment)
    //            {
    //                resPath.QuadTo(quadraticBezierSegment.Point1, quadraticBezierSegment.Point2);
    //                currentPoint = quadraticBezierSegment.Point2;
    //            }
    //            else if (pathSegment is ArcSegment arcSegment)
    //            {
    //                throw new NotSupportedException();
    //            }
    //        }

    //        if (pathFigure.IsClosed)
    //        {
    //            resPath.Close();
    //        }
    //    }

    //}
}
