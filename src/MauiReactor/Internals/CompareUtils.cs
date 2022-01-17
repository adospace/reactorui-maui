using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return left.Equals(right);        
        }
    }
}
