using System.Diagnostics.CodeAnalysis;

namespace MauiReactor.Internals
{
    public static class Validate
    {
        public static T EnsureNotNull<T>([NotNull] T? value)
            => value ?? throw new InvalidOperationException();

    }
}
