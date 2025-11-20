using MauiReactor.Internals;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Canvas;

[Scaffold(typeof(Internals.CanvasNode))]
public partial class CanvasNode { }

public partial interface ICanvasNode : IVisualNode
{
}

public partial class CanvasNode<T> : IEnumerable 
{
    protected readonly List<VisualNode> _internalChildren = new();

    protected override IEnumerable<VisualNode> RenderChildren()
    {
        return _internalChildren;
    }

    public IEnumerator<VisualNode> GetEnumerator()
    {
        return _internalChildren.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _internalChildren.GetEnumerator();
    }

    public void Add(params IEnumerable<VisualNode?>? childNodes)
    {
        if (childNodes is null)
        {
            return;
        }

        foreach (var node in childNodes)
        {
            if (node != null)
            {
                _internalChildren.Add(node);
            }
        }
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Internals.CanvasNode node)
        {
            NativeControl.InsertChild(widget.ChildIndex, node);
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Internals.CanvasNode node)
        {
            NativeControl.RemoveChild(node);
        }

        base.OnRemoveChild(widget, childControl);
    }
}
