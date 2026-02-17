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

    public static async Task<T?> FindOptional<T>(this IAutomationItemContainer automationItemContainer, string automationId, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
    {
        var itemFound = automationItemContainer.FindOptional<T>(automationId);
        if (itemFound != null)
        {
            return itemFound;
        }

        using var waitSem = new SemaphoreSlim(1);

        void handler(object? s, EventArgs e) => waitSem.Release();

        try
        {
            TemplateHost.LayoutCycleExecuted += handler;

            var waitingTimeout = timeout.TotalMilliseconds;

            while (itemFound == null && waitingTimeout > 0)
            {
                DateTime now = DateTime.Now;
                await waitSem.WaitAsync(TimeSpan.FromMilliseconds(waitingTimeout), cancellationToken);

                itemFound = automationItemContainer.FindOptional<T>(automationId);
                if (itemFound != null)
                {
                    return itemFound;
                }

                waitingTimeout -= (DateTime.Now - now).TotalMilliseconds;
            }

            return null;
        }
        finally
        {
            TemplateHost.LayoutCycleExecuted -= handler;
        }
    }

    public static Task<T?> Find<T>(this IAutomationItemContainer automationItemContainer, string automationId, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => automationItemContainer.FindOptional<T>(automationId, timeout, cancellationToken) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found");

    public static async Task<T?> FindOptional<T>(this IAutomationItemContainer automationItemContainer, Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
    {
        var itemFound = automationItemContainer.FindOptional(predicate);
        if (itemFound != null)
        {
            return itemFound;
        }

        using var waitSem = new SemaphoreSlim(1);

        void handler(object? s, EventArgs e) => waitSem.Release();

        try
        {
            TemplateHost.LayoutCycleExecuted += handler;

            var waitingTimeout = timeout.TotalMilliseconds;

            while (itemFound == null && waitingTimeout > 0)
            {
                DateTime now = DateTime.Now;
                await waitSem.WaitAsync(TimeSpan.FromMilliseconds(waitingTimeout), cancellationToken);

                itemFound = automationItemContainer.FindOptional<T>(predicate);
                if (itemFound != null)
                {
                    return itemFound;
                }

                waitingTimeout -= (DateTime.Now - now).TotalMilliseconds;
            }

            return null;
        }
        finally
        {
            TemplateHost.LayoutCycleExecuted -= handler;
        }
    }

    public static Task<T?> Find<T>(this IAutomationItemContainer automationItemContainer,Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => automationItemContainer.FindOptional<T>(predicate, timeout, cancellationToken) ?? throw new InvalidOperationException($"Unable to find the element");
}

