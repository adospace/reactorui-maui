using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MauiReactor;

public class LinearGradient
{
    private readonly LinearGradientBrush _brush;

    public LinearGradient(double angleDegrees, params ReadOnlySpan<uint> colors)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, params ReadOnlySpan<string> colors)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, params ReadOnlySpan<Color> colors)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(colors);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, params ReadOnlySpan<GradientStop> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, params ReadOnlySpan<(Color, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, params ReadOnlySpan<(uint, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, params ReadOnlySpan<(string, float)> stops)
    {
        var stopCollection = GradientStopCollectionExtensions.CreateStopCollection(stops);

        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    public LinearGradient(double angleDegrees, GradientStopCollection stopCollection)
    {
        _brush = CreateBrush(angleDegrees, stopCollection);
    }

    private static LinearGradientBrush CreateBrush(double angleDegrees, GradientStopCollection stops)
    {
        //css-like angle degrees
        angleDegrees += 270;

        //normalize the angle
        angleDegrees %= 360;
        if (angleDegrees < 0)
            angleDegrees += 360;

        var angle = Math.Round(angleDegrees, 4);

        if (angle == 0)
        {
            return new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 0));
        }
        else if (angle == 90)
        {
            return new LinearGradientBrush(stops, new Point(0, 0), new Point(0, 1));
        }
        else if (angle == 180)
        {
            return new LinearGradientBrush(stops, new Point(1, 0), new Point(0, 0));
        }
        else if (angle == 270)
        {
            return new LinearGradientBrush(stops, new Point(0, 1), new Point(0, 0));
        }
        else
        {
            static Point Intersect(double angleInDegrees, double x2, double y2)
            {
                // Convert angle to radians and calculate m1
                double angleInRadians = angleInDegrees * Math.PI / 180;
                double m1 = Math.Tan(angleInRadians);

                // Calculate c1 for line 1
                double c1 = 0.5 - m1 * 0.5;

                // Calculate m2 for line 2
                double m2 = -1 / m1;

                // Calculate c2 for line 2 using the given point
                double c2 = y2 - m2 * x2;

                // Find intersection point
                double x = (c2 - c1) / (m1 - m2);
                double y = m1 * x + c1;

                return new Point(x, y);
            }


            if (angle > 0 && angle < 90)
            {
                var pt2 = Intersect(angleDegrees, 1.0, 1.0);
                var pt1 = Intersect(angleDegrees, 0.0, 0.0);

                return new LinearGradientBrush(stops, pt1, pt2);
            }
            else if (angle > 90 && angle < 180)
            {
                var pt2 = Intersect(angleDegrees, 0.0, 1.0);
                var pt1 = Intersect(angleDegrees, 1.0, 0.0);

                return new LinearGradientBrush(stops, pt1, pt2);
            }
            else if (angle > 180 && angle < 270)
            {
                var pt2 = Intersect(angleDegrees, 0.0, 0.0);
                var pt1 = Intersect(angleDegrees, 1.0, 1.0);

                return new LinearGradientBrush(stops, pt1, pt2);
            }
            else if (angle > 270 && angle < 360)
            {
                var pt2 = Intersect(angleDegrees, 1.0, 0.0);
                var pt1 = Intersect(angleDegrees, 0.0, 1.0);

                return new LinearGradientBrush(stops, pt1, pt2);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }

    public static implicit operator LinearGradientBrush(LinearGradient d) => d._brush;
}
