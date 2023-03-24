using MauiReactor.Internals;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.SkiaCanvas.Internals;

public class SkGL : SKGLView, INodeContainer, INodeParent, IAutomationItemContainer
{
    private readonly List<SkNode> _children = new();

    protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
    {
        base.OnPaintSurface(e);

    }


    IEnumerable<T> IAutomationItemContainer.Descendants<T>()
    {
        var queue = new Queue<INodeContainer>(16);
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            IReadOnlyList<SkNode> children = queue.Dequeue().Children;
            for (var i = 0; i < children.Count; i++)
            {
                SkNode child = children[i];
                if (child is not T childT)
                    continue;

                yield return childT;
                queue.Enqueue(child);
            }
        }
    }

    public IReadOnlyList<SkNode> Children => _children;

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


    public void RequestInvalidate() => InvalidateSurface();

}
