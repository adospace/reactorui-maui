using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MauiReactor.SkiaCanvas.Internals;

public class SkNode : BindableObject, INodeContainer, INodeParent, IAutomationItem
{
    private readonly List<SkNode> _children = new();

    private bool _invalidateRequested = false;

    public IReadOnlyList<SkNode> Children => _children;
    
    public INodeParent? Parent { get; internal set; }

    public static readonly BindableProperty AutomationIdProperty = BindableProperty.Create(nameof(AutomationId), typeof(string), typeof(SkNode), null);
    public string AutomationId => (string)GetValue(AutomationIdProperty);

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        RequestInvalidate();
        base.OnPropertyChanged(propertyName);
    }

    public void InsertChild(int index, SkNode child)
    {
        _children.Insert(index, child);
        child.Parent = this;

        OnChildAdded(child);
    }

    protected virtual void OnChildAdded(SkNode child)
    {
    }

    public void RemoveChild(SkNode child)
    {
        _children.Remove(child);

        if (child.Parent == this)
        {
            child.Parent = null;
        }

        OnChildRemoved(child);
    }

    protected virtual void OnChildRemoved(SkNode child)
    {

    }

    public void Draw(DrawingContext context)
    {

        //context.Surface.Canvas.
        //Bounds = context.DirtyRect;

        //if (IsVisible)
        //{
        //    DrawOverride(context);
        //}
    }

    protected virtual void DrawOverride(DrawingContext context)
    {
        OnDraw(context);
    }

    protected virtual void OnDraw(DrawingContext context)
    {
        _invalidateRequested = false;
    }

    public void RequestInvalidate()
    {
        if (!_invalidateRequested)
        {
            _invalidateRequested = true;
            Parent?.RequestInvalidate();
        }
    }

}