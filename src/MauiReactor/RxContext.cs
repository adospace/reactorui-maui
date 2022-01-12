using System;
using System.Collections.Generic;
using System.Text;

namespace MauiReactor
{
    public class RxContext : Dictionary<string, object>
    {
        public RxContext()
        { }

        public RxContext(Action<RxContext> builderAction)
        {
            if (builderAction is null)
            {
                throw new ArgumentNullException(nameof(builderAction));
            }

            builderAction(this);
        }
    }

    public static class RxContextExtensions
    {
        public static T Get<T>(this RxContext context, string key, T defaultValue = default)
        {
            if (context.TryGetValue(key, out var value))
                return (T)value;

            return defaultValue;
        }
    }
}
