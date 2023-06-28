using System.Diagnostics.CodeAnalysis;

namespace MauiReactor.Internals
{
    public static class Validate
    {
        [return: NotNull]
        public static T EnsureNotNull<T>([NotNull] this T? value)
            => value ?? throw new InvalidOperationException();

    }
}
