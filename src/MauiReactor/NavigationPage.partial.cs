using MauiReactor.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public partial class NavigationPage<T>
{
    public NavigationPage(VisualNode content)
    {
        _internalChildren.Add(content);
    }

    protected override void OnMount()
    {
        //base.OnMount();
    }

    protected override void OnAddChild(VisualNode widget, BindableObject childControl)
    {
        //Validate.EnsureNotNull(NativeControl);

        if (childControl is Microsoft.Maui.Controls.Page page)
        {
            _nativeControl = (T)(Activator.CreateInstance(typeof(T), page) ?? throw new InvalidOperationException($"Unable to create type '{typeof(T)}'"));
            base.OnMount();
            OnUpdate();
            //NativeControl.PushAsync(page);
        }

        base.OnAddChild(widget, childControl);
    }

}
