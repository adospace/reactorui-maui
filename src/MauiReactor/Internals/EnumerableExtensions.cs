namespace MauiReactor.Internals
{
    internal static class EnumerableExtensions
    {
        public static bool NullableSequenceEqual<T>(this IEnumerable<T>? list, IEnumerable<T>? compareToList)
        {
            if (list == null && compareToList == null)
            {
                return true;
            }

            if (list == null)
            {
                return false;
            }

            if (compareToList == null)
            {
                return false;
            }

            return list.SequenceEqual(compareToList);
        }
    }
}
