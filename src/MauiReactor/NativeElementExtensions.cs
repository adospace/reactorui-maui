using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static class NativeElementExtensions
{
    public static T? FindByAutomationId<T>(this IElementController elementController, string id) where T : class
    {
        foreach (var element in elementController.Descendants())
        {
            if (element.AutomationId == id)
            {
                if (element is T elementT)
                {
                    return elementT;
                }
                else
                {
                    throw new InvalidOperationException($"Unable to cast {element.GetType()} to type {typeof(T)}");
                }
            }

            if (element is IAutomationItemContainer automationItemContainer)
            {
                foreach (var item in automationItemContainer.Descendants())
                {
                    if (item.AutomationId == id)
                    {
                        return (T)item;
                    }
                }
            }
        }

        return default;
    }
}
