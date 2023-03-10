using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static class NativeElementExtensions
{
    public static T? FindByAutomationId<T>(this IElementController elementController, string id) where T : Element
    {
        foreach (var element in elementController.Descendants())
        {
            if (element.AutomationId == id)
            {
                return (T)element;
            }
        }

        return default;
    }
}
