using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public class LinearGradient
{
    private readonly LinearGradientBrush _brush;

    public LinearGradient(double degrees, params uint[] colors)
    {
        var stops = new GradientStopCollection();
        
        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(Color.FromUint(colors[i]), i * 1.0f / colors.Length));
        }

        // Calculate the center point of the rectangle
        Point centerPoint = new(0.5, 0.5);

        // Calculate the distance from the center to the corners of the rectangle
        double distanceToCorners = Math.Sqrt(2);

        // Calculate the start and end points of the gradient based on the angle and the rectangle's dimensions
        double angleRad = degrees * Math.PI / 180;
        double dx = Math.Cos(angleRad) * distanceToCorners;
        double dy = Math.Sin(angleRad) * distanceToCorners;

        Point startPoint = new(centerPoint.X - dx, centerPoint.Y - dy);
        Point endPoint = new(centerPoint.X + dx, centerPoint.Y + dy);

        _brush = new LinearGradientBrush(stops, startPoint, endPoint);
    }

    public LinearGradient(double angleDegrees, params Color[] colors)
    {
        var stops = new GradientStopCollection();

        for (int i = 0; i < colors.Length; i++)
        {
            stops.Add(new GradientStop(colors[i], i * 1.0f / colors.Length));
        }

        float width = 1.0f;
        float height = 1.0f;
        var rectangle = new RectF(0, 0, width, height);
        PointF startPoint;
        PointF endPoint;

        // Calculate start and end points based on angle
        if (angleDegrees == 0)
        {
            startPoint = new PointF(rectangle.Left, rectangle.Top);
            endPoint = new PointF(rectangle.Left, rectangle.Bottom);
        }
        else if (angleDegrees == 90)
        {
            startPoint = new PointF(rectangle.Left, rectangle.Top);
            endPoint = new PointF(rectangle.Right, rectangle.Top);
        }
        else if (angleDegrees == 180)
        {
            startPoint = new PointF(rectangle.Right, rectangle.Top);
            endPoint = new PointF(rectangle.Left, rectangle.Top);
        }
        else if (angleDegrees == 270)
        {
            startPoint = new PointF(rectangle.Left, rectangle.Bottom);
            endPoint = new PointF(rectangle.Left, rectangle.Top);
        }
        else
        {
            double angleRadians = angleDegrees * Math.PI / 180;
            double dx = Math.Cos(angleRadians);
            double dy = Math.Sin(angleRadians);

            PointF centerPoint = new PointF(rectangle.Left + rectangle.Width / 2f, rectangle.Top + rectangle.Height / 2f);
            double distance = Math.Max(rectangle.Width, rectangle.Height) * Math.Sqrt(2);

            startPoint = new PointF((float)(centerPoint.X - dx * distance / 2), (float)(centerPoint.Y - dy * distance / 2));
            endPoint = new PointF((float)(centerPoint.X + dx * distance / 2), (float)(centerPoint.Y + dy * distance / 2));
        }



        //// Calculate the center point of the rectangle
        //Point centerPoint = new(0.5, 0.5);

        //// Calculate the distance from the center to the corners of the rectangle
        //double distanceToCorners = Math.Sqrt(2) / 2;

        //// Calculate the start and end points of the gradient based on the angle and the rectangle's dimensions
        //double angleRad = degrees * Math.PI / 180;
        //double dx = Math.Cos(angleRad) * distanceToCorners;
        //double dy = Math.Sin(angleRad) * distanceToCorners;

        //Point startPoint = new(centerPoint.X - dx, centerPoint.Y - dy);
        //Point endPoint = new(centerPoint.X + dx, centerPoint.Y + dy);

        _brush = new LinearGradientBrush(stops, startPoint, endPoint);
    }

    //public static LinearGradientBrush CreateLinearGradientBrushFromCSS(string cssGradient, Rect rectangle)
    //{
    //    LinearGradientBrush brush;

    //    try
    //    {
    //        var parser = new System.Text.RegularExpressions.Regex(@"linear-gradient\((.+?)\)");
    //        var match = parser.Match(cssGradient);

    //        if (!match.Success)
    //            throw new ArgumentException("Invalid CSS linear gradient string.");

    //        var gradientParams = match.Groups[1].Value.Trim();

    //        var parts = gradientParams.Split(',');
    //        if (parts.Length < 2)
    //            throw new ArgumentException("Invalid CSS linear gradient string.");

    //        var anglePart = parts[0].Trim();
    //        var colorStopsPart = parts[1].Trim();

    //        double angleDegrees;
    //        if (!double.TryParse(anglePart, out angleDegrees))
    //            throw new ArgumentException("Invalid CSS linear gradient string. Invalid angle value.");

    //        angleDegrees %= 360;
    //        angleDegrees = 360 - angleDegrees; // Invert angle to match GDI+ orientation

    //        var colorStops = colorStopsPart.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    //        var colors = new Color[colorStops.Length];
    //        var positions = new float[colorStops.Length];

    //        for (int i = 0; i < colorStops.Length; i++)
    //        {
    //            var colorStop = colorStops[i].Trim().Split(' ');
    //            if (colorStop.Length < 2)
    //                throw new ArgumentException("Invalid CSS linear gradient string.");

    //            float position;
    //            if (!float.TryParse(colorStop[1], out position))
    //                throw new ArgumentException("Invalid CSS linear gradient string. Invalid color stop position.");

    //            positions[i] = position / 100f;

    //            var colorCode = colorStop[0];
    //            if (colorCode.StartsWith("#") && colorCode.Length == 7)
    //            {
    //                //var red = Convert.ToInt32(colorCode.Substring(1, 2), 16);
    //                //var green = Convert.ToInt32(colorCode.Substring(3, 2), 16);
    //                //var blue = Convert.ToInt32(colorCode.Substring(5, 2), 16);

    //                colors[i] = Color.FromArgb(colorCode);
    //            }
    //            else
    //            {
    //                var color = Colors.FromName(colorCode);
    //                if (!color.IsKnownColor)
    //                    throw new ArgumentException("Invalid CSS linear gradient string. Invalid color code.");

    //                colors[i] = color;
    //            }
    //        }

    //        brush = CreateLinearGradientBrushFromAngle(rectangle, angleDegrees, colors, positions);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new ArgumentException("Error parsing CSS linear gradient string.", ex);
    //    }

    //    return brush;
    //}

    //private static LinearGradientBrush CreateLinearGradientBrushFromAngle(RectF rectangle, double angleDegrees, Color[] colors, float[] positions)
    //{
    //    PointF startPoint;
    //    PointF endPoint;

    //    // Calculate start and end points based on angle
    //    if (angleDegrees == 0)
    //    {
    //        startPoint = new PointF(rectangle.Left, rectangle.Top);
    //        endPoint = new PointF(rectangle.Left, rectangle.Bottom);
    //    }
    //    else if (angleDegrees == 90)
    //    {
    //        startPoint = new PointF(rectangle.Left, rectangle.Top);
    //        endPoint = new PointF(rectangle.Right, rectangle.Top);
    //    }
    //    else if (angleDegrees == 180)
    //    {
    //        startPoint = new PointF(rectangle.Right, rectangle.Top);
    //        endPoint = new PointF(rectangle.Left, rectangle.Top);
    //    }
    //    else if (angleDegrees == 270)
    //    {
    //        startPoint = new PointF(rectangle.Left, rectangle.Bottom);
    //        endPoint = new PointF(rectangle.Left, rectangle.Top);
    //    }
    //    else
    //    {
    //        double angleRadians = angleDegrees * Math.PI / 180;
    //        double dx = Math.Cos(angleRadians);
    //        double dy = Math.Sin(angleRadians);

    //        PointF centerPoint = new PointF(rectangle.Left + rectangle.Width / 2f, rectangle.Top + rectangle.Height / 2f);
    //        double distance = Math.Max(rectangle.Width, rectangle.Height) * Math.Sqrt(2);

    //        startPoint = new PointF((float)(centerPoint.X - dx * distance / 2), (float)(centerPoint.Y - dy * distance / 2));
    //        endPoint = new PointF((float)(centerPoint.X + dx * distance / 2), (float)(centerPoint.Y + dy * distance / 2));
    //    }

    //    // Create LinearGradientBrush with calculated start and end points
    //    LinearGradientBrush brush = new LinearGradientBrush(startPoint, endPoint, gradientStops);

    //    // Set interpolation colors using the provided array of colors and positions
    //    ColorBlend colorBlend = new ColorBlend();
    //    colorBlend.Colors = colors;
    //    colorBlend.Positions = positions;
    //    brush.InterpolationColors = colorBlend;

    //    return brush;
    //}

    public static implicit operator LinearGradientBrush(LinearGradient d) => d._brush;
}
