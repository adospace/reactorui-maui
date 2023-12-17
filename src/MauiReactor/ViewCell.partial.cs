using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class ViewCell<T>
{
    protected override IEnumerable<VisualNode> RenderChildren()
    {
        if (_internalChildren.Count == 0)
            yield break;

        yield return _internalChildren.First();
    }

    protected sealed override void OnAddChild(VisualNode widget, BindableObject childNativeControl)
    {
        Validate.EnsureNotNull(NativeControl);

        if (childNativeControl is View view)
            NativeControl.View = view;
        else
        {
            throw new InvalidOperationException($"Type '{childNativeControl.GetType()}' not supported under '{GetType()}'");
        }
    }
}
