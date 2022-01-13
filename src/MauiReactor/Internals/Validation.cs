using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals
{
    internal static class Validate
    {
        public static T EnsureNotNull<T>([NotNull] T? value)
            => value ?? throw new InvalidOperationException();

    }
}
