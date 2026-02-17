using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using MauiReactor.Internals;
using Microsoft.Extensions.Logging;
using static MauiReactor.Integration.ComponentHost;

namespace MauiReactor;

public interface ITemplateHost
{
    BindableObject? NativeElement { get; }
}

public class TemplateHost : VisualNode, ITemplateHost, IVisualNode, IHostElement
{
    private readonly VisualNode _root;
    private readonly LinkedList<IVisualNodeWithNativeControl> _listOfVisualsToAnimate = new();
    private bool _layoutCallEnqueued;
    private bool _started;

    public TemplateHost(VisualNode root)
    {
        _root = root;

        Run();
    }

    public static ITemplateHost Create(VisualNode root)
        => new TemplateHost(root);

    public VisualNode Root
    {
        get => _root;
    }

    public BindableObject? NativeElement { get; private set; }

    BindableObject? ITemplateHost.NativeElement => NativeElement;

    internal static event EventHandler? LayoutCycleExecuted;

    internal static void FireLayoutCycleExecuted(object? sender)
        => LayoutCycleExecuted?.Invoke(sender, EventArgs.Empty);

    Microsoft.Maui.Controls.Page? IVisualNode.GetContainerPage()
    {
        return NativeElement as Microsoft.Maui.Controls.Page;
    }

    IHostElement? IVisualNode.GetPageHost()
    {
        return this;
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
        if (_started)
        {
            if (_layoutCallEnqueued)
            {
                System.Diagnostics.Debug.WriteLine("_layoutCallEnqueued");
            }
            else
            {
                _layoutCallEnqueued = true;
                if (Application.Current != null)
                {
                    Application.Current.Dispatcher.Dispatch(OnLayout);
                }
                else
                {
                    OnLayout();
                }                
            }
        }

        base.OnLayoutCycleRequested();
    }

    private void OnLayout()
    {
        _layoutCallEnqueued = false;

        if (!_started)
        {
            return;
        }

        try
        {
            Layout();

            if (_listOfVisualsToAnimate.Count > 0)
            {
                AnimationCallback();
            }

            FireLayoutCycleExecuted(this);
        }
        catch (Exception ex)
        {
            var logger = ServiceCollectionProvider.ServiceProvider?.GetService<ILogger<TemplateHost>>();
            logger?.LogError(ex, "Unable to layout component");

            ReactorApplicationHost.FireUnhandledExceptionEvent(ex);
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    public IHostElement Run()
    {
        _started = true;

        OnLayout();


        return this;
    }

    public void Stop()
    {
        _root?.Unmount();
        _started = false;
    }

    public void RequestAnimationFrame(IVisualNodeWithNativeControl visualNode)
    {
        _listOfVisualsToAnimate.AddFirst(visualNode);
    }

    private void AnimationCallback()
    {
        DateTime now = DateTime.Now;
        if (Application.Current != null && AnimateVisuals())
        {
            //System.Diagnostics.Debug.WriteLine($"{(DateTime.Now - now).TotalMilliseconds}");
            var elapsedMilliseconds = (DateTime.Now - now).TotalMilliseconds;
            if (elapsedMilliseconds > 16)
            {
                System.Diagnostics.Debug.WriteLine("[MauiReactor] FPS WARNING");
                Application.Current.Dispatcher.Dispatch(AnimationCallback);
            }
            else
            {
                Application.Current.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(16 - elapsedMilliseconds), AnimationCallback);
            }
        }
    }

    private bool AnimateVisuals()
    {
        if (_listOfVisualsToAnimate.Count == 0)
            return false;

        bool animated = false;
        LinkedListNode<IVisualNodeWithNativeControl>? nodeToAnimate = _listOfVisualsToAnimate.First;
        while (nodeToAnimate != null)
        {
            var nextNode = nodeToAnimate.Next;

            if (nodeToAnimate.Value.Animate())
            {
                animated = true;
            }
            else
            {
                _listOfVisualsToAnimate.Remove(nodeToAnimate);
            }

            nodeToAnimate = nextNode;
        }

        return animated;
    }

}

public static class TemplateHostExtensions
{
    public static T? FindOptional<T>(this ITemplateHost templateHost, string automationId) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
        {
            var foundElement = elementController.FindOptional<T>(automationId);
            if (foundElement != null)
            {
                return foundElement;
            }
        }

        if (templateHost.NativeElement is IAutomationItemContainer childAsAutomationItemContainer)
        {
            var foundElement = childAsAutomationItemContainer.FindOptional<T>(automationId);
            if (foundElement != null)
            {
                return foundElement;
            }
        }

        if (templateHost is IAutomationItemContainer automationItemContainer)
            return automationItemContainer.FindOptional<T>(automationId);

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
            TemplateHost.LayoutCycleExecuted += handler;
            
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
            TemplateHost.LayoutCycleExecuted -= handler;
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
            TemplateHost.LayoutCycleExecuted += handler;

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
            TemplateHost.LayoutCycleExecuted -= handler;
        }
    }

    public static Task<T?> Find<T>(this ITemplateHost templateHost, Func<T, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default) where T : class
        => templateHost.FindOptional<T>(predicate, timeout, cancellationToken) ?? throw new InvalidOperationException($"Unable to find the element");
}
