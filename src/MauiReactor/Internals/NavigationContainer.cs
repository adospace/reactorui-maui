using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

internal static class NavigationProvider
{
    public static INavigation? Navigation { get; set; }
}

public sealed class NavigationContainer : IDisposable
{
    private readonly Microsoft.Maui.Controls.Internals.NavigationProxy _navigationProxy = new();

    public NavigationContainer()
    {
        NavigationProvider.Navigation = _navigationProxy;
    }

    public ITemplateHost AttachTopPage()
    {
        if (_navigationProxy.NavigationStack.Count == 0)
        {
            throw new InvalidOperationException("Navigation stack is empty");
        }

        var page = _navigationProxy.NavigationStack[_navigationProxy.NavigationStack.Count - 1];

        if (page.GetValue(PageHost.MauiReactorPageHostBagKey.BindableProperty) is not ITemplateHost templateHost)
        {
            throw new InvalidOperationException("Unable to attach the host");
        }

        return templateHost;
    }


    public ITemplateHost AttachTopModal()
    {
        if (_navigationProxy.ModalStack.Count == 0)
        {
            throw new InvalidOperationException("Modal stack is empty");
        }

        var page = _navigationProxy.ModalStack[_navigationProxy.ModalStack.Count - 1];

        if (page.GetValue(PageHost.MauiReactorPageHostBagKey.BindableProperty) is not ITemplateHost templateHost)
        {
            throw new InvalidOperationException("Unable to attach the host");
        }

        return templateHost;
    }


    public void Dispose()
    {
        NavigationProvider.Navigation = null;
    }
}
