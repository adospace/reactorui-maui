using System.Diagnostics.CodeAnalysis;

namespace MauiReactor.Internals
{
    public static class Validate
    {
        [return: NotNull]
        public static T EnsureNotNull<T>([NotNull] this T? value)
        {
            if (value == null)
            {
                throw new InvalidOperationException();
            }

            return value;
        }

        public static void EnsureNull<T>(this T? value)
        {
            if (value != null)
            {
                throw new InvalidOperationException();
            }
        }

    }
}
