using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

public interface IAutomationItemContainer
{
    IEnumerable<T> Descendants<T>() where T : class;
}

public static class AutomationItemContainerExtensions
{
    public static T? FindOptional<T>(this IAutomationItemContainer automationItemContainer, string automationId) where T : class
    {
        foreach (var item in automationItemContainer.Descendants<T>())
        {
            if (item is IAutomationItem automationItem)
            {
                if (automationItem.AutomationId == automationId)
                {
                    return item;
                }
            }
            else if (item is Element itemElement)
            {
                if (itemElement.AutomationId == automationId)
                {
                    return item;
                }
            }    
        }

        return default;
    }

    public static T Find<T>(this IAutomationItemContainer automationItemContainer, string automationId) where T : class
        => automationItemContainer.FindOptional<T>(automationId) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found or requested type is not correct");

    public static T? FindOptional<T>(this IAutomationItemContainer automationItemContainer, Func<T, bool> predicate) where T : class
    {
        foreach (var item in automationItemContainer.Descendants<T>())
        {
            if (predicate(item))
            {
                return item;
            }
        }

        return default;
    }

    public static T Find<T>(this IAutomationItemContainer automationItemContainer, Func<T, bool> predicate) where T : class
        => automationItemContainer.FindOptional<T>(predicate) ?? throw new InvalidOperationException($"Unable to find the element");

    public static IEnumerable<T> FindAll<T>(this IAutomationItemContainer automationItemContainer, Func<T, bool>? predicate = null) where T : class
    {
        foreach (var item in automationItemContainer.Descendants<T>())
        {
            if (predicate == null || predicate(item))
            {
                yield return item;
            }
        }
    }
}

