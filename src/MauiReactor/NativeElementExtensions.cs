using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static class NativeElementExtensions
{
    public static T? FindOptional<T>(this IElementController elementController, string automationId) where T : class
    {
        foreach (var element in elementController.Descendants())
        {
            if (element.AutomationId == automationId)
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
                    if (item.AutomationId == automationId)
                    {
                        return (T)item;
                    }
                }
            }
        }

        return default;
    }

    public static T Find<T>(this IElementController elementController, string automationId) where T : class
        => elementController.FindOptional<T>(automationId) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found");

    public static T? FindOptional<T>(this IElementController elementController, Func<T, bool> predicate) where T : class
    {
        foreach (var element in elementController.Descendants())
        {
            if (element is T elementT)
            {
                if (predicate(elementT))
                {
                    return elementT;
                }
            }

            if (element is IAutomationItemContainer automationItemContainer)
            {
                foreach (var item in automationItemContainer.Descendants())
                {
                    if (item is T itemT)
                    {
                        if (predicate(itemT))
                        {
                            return itemT;
                        }
                    }
                }
            }
        }

        return default;
    }

    public static T Find<T>(this IElementController elementController, Func<T, bool> predicate) where T : class
        => elementController.FindOptional<T>(predicate) ?? throw new InvalidOperationException($"Unable to find the element");

    public static IEnumerable<T> FindAll<T>(this IElementController elementController, Func<T, bool> predicate) where T : class
    {
        foreach (var element in elementController.Descendants())
        {
            if (element is T elementT)
            {
                if (predicate(elementT))
                {
                    yield return elementT;
                }
            }

            if (element is IAutomationItemContainer automationItemContainer)
            {
                foreach (var item in automationItemContainer.Descendants())
                {
                    if (item is T itemT)
                    {
                        if (predicate(itemT))
                        {
                            yield return itemT;
                        }
                    }
                }
            }
        }
    }
}
