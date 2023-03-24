using MauiReactor.Internals;
using Microsoft.Maui.Controls;
using System;
using System.Collections;

namespace MauiReactor.SkiaCanvas;

public partial interface ISkView : IView
{


}


public partial class SkCanvas<T> : View<T>, ISkView, IEnumerable where T : Internals.SkCanvas, new()
{
    public SkCanvas()
    {

    }

    public SkCanvas(Action<T?> componentRefAction)
        : base(componentRefAction)
    {

    }

    protected override bool SupportChildIndexing => true;

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Internals.SkNode node)
        {
            NativeControl.InsertChild(widget.ChildIndex, node);
        }

        base.OnAddChild(widget, childControl);
    }

    protected override void OnRemoveChild(VisualNode widget, BindableObject childControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childControl is Internals.SkNode node)
        {
            NativeControl.RemoveChild(node);
        }

        base.OnRemoveChild(widget, childControl);
    }


    protected override void OnUpdate()
    {
        Validate.EnsureNotNull(NativeControl);

        NativeControl.RequestInvalidate();

        base.OnUpdate();
    }
}


public partial class SkView : SkCanvas<Internals.SkCanvas>
{
    public SkView()
    {

    }

    public SkView(Action<Internals.SkCanvas?> componentRefAction)
        : base(componentRefAction)
    {

    }
}


public static partial class SkViewExtensions
{

}
