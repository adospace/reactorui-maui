using System.Reflection;

namespace MauiReactor.Scaffold
{
    internal class EventInfoEqualityComparer : IEqualityComparer<EventInfo>
    {
        public bool Equals(EventInfo? x, EventInfo? y)
        {
            return x?.Name == y?.Name;
        }

        public int GetHashCode(EventInfo obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}