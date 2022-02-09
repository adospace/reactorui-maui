using System.Diagnostics.CodeAnalysis;

namespace MauiReactor.Internals
{
    internal static class Validate
    {
        public static T EnsureNotNull<T>([NotNull] T? value)
            => value ?? throw new InvalidOperationException();

    }
}
