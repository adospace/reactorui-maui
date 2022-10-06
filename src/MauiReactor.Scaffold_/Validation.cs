using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Scaffold
{
    internal static class Validate
    {
        public static T EnsureNotNull<T>([NotNull] this T? value)
            => value ?? throw new InvalidOperationException();
    }
}
