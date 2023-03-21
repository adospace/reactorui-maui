using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;

namespace MauiReactor;

public interface ITemplateHost
{
    BindableObject? NativeElement { get; }

    event EventHandler? LayoutCycleExecuted;
}

public class TemplateHost : VisualNode, ITemplateHost
{
    private readonly VisualNode _root;
    private EventHandler? _layoutCycleExecuted;
    
    public TemplateHost(VisualNode root)
    {
        _root = root;

        Layout();
    }

    public static ITemplateHost Create(VisualNode root)
        => new TemplateHost(root);

    public VisualNode Root
    {
        get => _root;
    }

    public BindableObject? NativeElement { get; private set; }

    BindableObject? ITemplateHost.NativeElement => NativeElement;

    event EventHandler? ITemplateHost.LayoutCycleExecuted
    {
        add
        {
            _layoutCycleExecuted += value;
        }
        remove
        {
            _layoutCycleExecuted -= value;
        }
    }

    protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
    {
        NativeElement = nativeControl;
    }

    protected sealed override void OnRemoveChild(VisualNode widget, BindableObject nativeControl)
    {
    }

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        yield return Root;
    }

    protected internal override void OnLayoutCycleRequested()
    {
        Layout();
        _layoutCycleExecuted?.Invoke(this, EventArgs.Empty);
        base.OnLayoutCycleRequested();
    }
}

public static class TemplateHostExtensions
{
    public static T? FindOptional<T>(this ITemplateHost templateHost, string automationId) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
            return elementController.Find<T>(automationId);

        if (templateHost is IAutomationItemContainer automationItemContainer)
            return automationItemContainer.Find<T>(automationId);

        return default;
    }

    public static T Find<T>(this ITemplateHost templateHost, string automationId) where T : class
        => templateHost.FindOptional<T>(automationId) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found");

    public static T? FindOptional<T>(this ITemplateHost templateHost, Func<T, bool> predicate) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
            return elementController.Find<T>(predicate);

        if (templateHost.NativeElement is IAutomationItemContainer automationItemContainer)
            return automationItemContainer.Find<T>(predicate);

        return default;
    }

    public static T Find<T>(this ITemplateHost templateHost, Func<T, bool> predicate) where T : class
        => templateHost.FindOptional<T>(predicate) ?? throw new InvalidOperationException($"Unable to find the element");

    public static IEnumerable<T> FindAll<T>(this ITemplateHost templateHost, Func<T, bool>? predicate = null) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
            return elementController.FindAll<T>(predicate);

        if (templateHost.NativeElement is IAutomationItemContainer automationItemContainer)
            return automationItemContainer.FindAll<T>(predicate);

        return Array.Empty<T>();
    }

    public static async Task<T?> FindOptional<T>(this ITemplateHost templateHost, string automationId, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
    {
        var itemFound = templateHost.FindOptional<T>(automationId);
        if (itemFound != null)
        {
            return itemFound;
        }

        using var waitSem = new SemaphoreSlim(1);

        void handler(object? s, EventArgs e) => waitSem.Release();

        try
        {
            templateHost.LayoutCycleExecuted += handler;
            
            var waitingTimeout = timeout.TotalMilliseconds;
            
            while (itemFound == null && waitingTimeout > 0)
            {
                DateTime now = DateTime.Now;
                await waitSem.WaitAsync(TimeSpan.FromMilliseconds(waitingTimeout), cancellationToken);

                itemFound = templateHost.FindOptional<T>(automationId);
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
            templateHost.LayoutCycleExecuted -= handler;
        }
    }

    public static Task<T?> Find<T>(this ITemplateHost templateHost, string automationId, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => templateHost.FindOptional<T>(automationId, timeout, cancellationToken) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found");

    public static async Task<T?> FindOptional<T>(this ITemplateHost templateHost, Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
    {
        var itemFound = templateHost.FindOptional(predicate);
        if (itemFound != null)
        {
            return itemFound;
        }

        using var waitSem = new SemaphoreSlim(1);

        void handler(object? s, EventArgs e) => waitSem.Release();

        try
        {
            templateHost.LayoutCycleExecuted += handler;

            var waitingTimeout = timeout.TotalMilliseconds;

            while (itemFound == null && waitingTimeout > 0)
            {
                DateTime now = DateTime.Now;
                await waitSem.WaitAsync(TimeSpan.FromMilliseconds(waitingTimeout), cancellationToken);

                itemFound = templateHost.FindOptional<T>(predicate);
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
            templateHost.LayoutCycleExecuted -= handler;
        }
    }

    public static Task<T?> Find<T>(this ITemplateHost templateHost, Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => templateHost.FindOptional<T>(predicate, timeout, cancellationToken) ?? throw new InvalidOperationException($"Unable to find the element");
}
