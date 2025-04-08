namespace MauiReactor.Internals
{
    internal static class CompareUtils
    {
        private const double DOUBLE_EPSILON = 0.0000001;

        public static bool AreEquals(object? left, object? right)
        {
            if (left == null && right == null)
            {
                return true;
            }
            if (left == null)
            {
                return false;
            }
            if (right == null)
            {
                return false;
            }

            bool objectEquals = left.Equals(right);
            if (objectEquals)
            {
                return true;
            }

            if (left is double x &&
                right is double y)
            {
                return AreDoublesClose(x, y);
            }

            if (left is SolidColorBrush leftSolidColorBrush &&
                right is SolidColorBrush rightSolidColorBrush)
            {
                //NOTE: Color is null if IsEmpty==true
                return leftSolidColorBrush.Color?.ToUint() == rightSolidColorBrush.Color?.ToUint();
            }

            if (left is LinearGradientBrush leftLinearGradientBrush &&
                right is LinearGradientBrush rightLinearGradientBrush)
            {
                return AreLinearGradientsClose(leftLinearGradientBrush, rightLinearGradientBrush);
            }

            if (left is Microsoft.Maui.Controls.FileImageSource leftFileImageSource &&
                right is Microsoft.Maui.Controls.FileImageSource rightFileImageSource)
            { 
                return leftFileImageSource.File == rightFileImageSource.File; //intentionally not comparing CachingEnabled
            }

            if (left is Microsoft.Maui.Controls.UriImageSource leftUriImageSource &&
                right is Microsoft.Maui.Controls.UriImageSource rightUriImageSource)
            {
                //compare using uri equals
                return leftUriImageSource.Uri == rightUriImageSource.Uri; //intentionally not comparing CachingEnabled
            }

            if (left is Microsoft.Maui.Controls.FontImageSource leftFontImageSource &&
                right is Microsoft.Maui.Controls.FontImageSource rightFontImageSource)
            {
                return (leftFontImageSource.IsEmpty && rightFontImageSource.IsEmpty) ||
                    (leftFontImageSource.Color == rightFontImageSource.Color && 
                    leftFontImageSource.FontFamily == rightFontImageSource.FontFamily &&
                    leftFontImageSource.Glyph == rightFontImageSource.Glyph &&
                    leftFontImageSource.FontAutoScalingEnabled == rightFontImageSource.FontAutoScalingEnabled &&
                    leftFontImageSource.Size == rightFontImageSource.Size); //intentionally not comparing FontAutoScalingEnabled
            }

            if (left is UrlWebViewSource leftUrlWebViewSource &&
                right is UrlWebViewSource rightUrlWebViewSource)
            {
                return leftUrlWebViewSource.Url == rightUrlWebViewSource.Url;
            }

            return false;
        }

        public static bool AreDoublesClose(double left, double right)
        {
            return (left > right ? left - right : right - left) < DOUBLE_EPSILON;
        }

        public static bool ArePointsClose(Point left, Point right)
        {
            return AreDoublesClose(left.X, right.X) && AreDoublesClose(left.Y, right.Y);
        }

        public static bool AreLinearGradientsClose(LinearGradientBrush leftLinearGradientBrush, LinearGradientBrush rightLinearGradientBrush)
        {
            if (leftLinearGradientBrush.IsEmpty && rightLinearGradientBrush.IsEmpty)
            {
                return true;
            }

            if (!ArePointsClose(leftLinearGradientBrush.StartPoint, rightLinearGradientBrush.StartPoint) ||
                !ArePointsClose(leftLinearGradientBrush.EndPoint, rightLinearGradientBrush.EndPoint))
            {
                return false;
            }

            if (leftLinearGradientBrush.GradientStops.Count != 
                rightLinearGradientBrush.GradientStops.Count)
            {
                return false;
            }

            for (int i = 0; i < leftLinearGradientBrush.GradientStops.Count; i++)
            {
                if (!AreDoublesClose(leftLinearGradientBrush.GradientStops[i].Offset, rightLinearGradientBrush.GradientStops[i].Offset))
                {
                    return false;
                }

                if (leftLinearGradientBrush.GradientStops[i].Color?.ToUint() != rightLinearGradientBrush.GradientStops[i].Color?.ToUint())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
