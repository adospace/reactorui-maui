using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

internal static class ElementExtensions
{
    public static T? GetParent<T>(this Element element) where T : Element
    {
        var parent = element.Parent;

        if (parent is T)
            return (T?)parent;

        if (parent == null)
            return null;

        return parent.GetParent<T>();
    }
}
