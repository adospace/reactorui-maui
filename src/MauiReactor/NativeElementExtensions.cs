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
            }

            if (element is IAutomationItemContainer automationItemContainer)
            {
                foreach (var item in automationItemContainer.Descendants<IAutomationItem>())
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
        => elementController.FindOptional<T>(automationId) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found or requested type is not correct");

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
                foreach (var item in automationItemContainer.Descendants<T>())
                {
                    if (predicate(item))
                    {
                        return item;
                    }
                }
            }
        }

        return default;
    }

    public static T Find<T>(this IElementController elementController, Func<T, bool> predicate) where T : class
        => elementController.FindOptional<T>(predicate) ?? throw new InvalidOperationException($"Unable to find the element");

    public static IEnumerable<T> FindAll<T>(this IElementController elementController, Func<T, bool>? predicate = null) where T : class
    {
        foreach (var element in elementController.Descendants())
        {
            if (element is T elementT)
            {
                if (predicate == null || predicate(elementT))
                {
                    yield return elementT;
                }
            }

            if (element is IAutomationItemContainer automationItemContainer)
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
    }

    public static async Task<T?> FindOptional<T>(this IElementController elementController, string automationId, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
    {
        var itemFound = elementController.FindOptional<T>(automationId);
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

                itemFound = elementController.FindOptional<T>(automationId);
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

    public static Task<T?> Find<T>(this IElementController elementController, string automationId, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => elementController.FindOptional<T>(automationId, timeout, cancellationToken) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found");

    public static async Task<T?> FindOptional<T>(this IElementController elementController, Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
    {
        var itemFound = elementController.FindOptional(predicate);
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

                itemFound = elementController.FindOptional<T>(predicate);
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

    public static Task<T?> Find<T>(this IElementController elementController, Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => elementController.FindOptional<T>( predicate, timeout, cancellationToken) ?? throw new InvalidOperationException($"Unable to find the element");
}
