namespace MauiReactor.Internals
{
    internal static class CompareUtils
    {
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

            if (left is ImageSource leftImageSource &&
                right is ImageSource rightImageSource)
            { 
                return leftImageSource.ToString() == rightImageSource.ToString();
            }

            if (left is UrlWebViewSource leftUrlWebViewSource &&
                right is UrlWebViewSource rightUrlWebViewSource)
            {
                return leftUrlWebViewSource.Url.ToString() == rightUrlWebViewSource.Url.ToString();
            }

            return left.Equals(right);        
        }
    }
}
