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
}

public class TemplateHost<T> : VisualNode, ITemplateHost where T : BindableObject
{
    public TemplateHost(VisualNode root)
    {
        _root = root;

        Layout();
    }

    private readonly VisualNode _root;

    public VisualNode Root
    {
        get => _root;
    }

    public T? NativeElement { get; private set; }

    BindableObject? ITemplateHost.NativeElement => NativeElement;

    protected sealed override void OnAddChild(VisualNode widget, BindableObject nativeControl)
    {
        if (nativeControl is T nativeElement)
            NativeElement = nativeElement;
        else
        {
            throw new InvalidOperationException($"Type '{nativeControl.GetType()}' not supported under '{GetType()}'");
        }
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
        base.OnLayoutCycleRequested();
    }
}

public static class TemplateHostExtensions
{
    public static T? FindOptional<T>(this ITemplateHost templateHost, string automationId) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
            return elementController.Find<T>(automationId);

        return default;
    }

    public static T Find<T>(this ITemplateHost templateHost, string automationId) where T : class
        => templateHost.FindOptional<T>(automationId) ?? throw new InvalidOperationException($"Element with automation id {automationId} not found");

    public static T? FindOptional<T>(this ITemplateHost templateHost, Func<T, bool> predicate) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
            return elementController.Find<T>(predicate);

        return default;
    }

    public static T Find<T>(this ITemplateHost templateHost, Func<T, bool> predicate) where T : class
        => templateHost.FindOptional<T>(predicate) ?? throw new InvalidOperationException($"Unable to find the element");

    public static IEnumerable<T> FindAll<T>(this ITemplateHost templateHost, Func<T, bool> predicate) where T : class
    {
        if (templateHost.NativeElement is IElementController elementController)
            return elementController.FindAll<T>(predicate);

        return Array.Empty<T>();
    }
}
