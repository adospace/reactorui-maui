using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    internal static class Validation
    {
        public static T ThrowIfNull<T>(this T? v)
        {
            return v ?? throw new ArgumentNullException(nameof(v));
        }
    }
}
