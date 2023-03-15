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

    public ITemplateHost AttachHost()
    {
        if (_navigationProxy.NavigationStack.Count == 0)
        {
            throw new InvalidOperationException("Navigation stack is empty");
        }

        var page = _navigationProxy.NavigationStack[_navigationProxy.NavigationStack.Count - 1];

        return (ITemplateHost)page.GetValue(PageHost.MauiReactorPageHostBagKey.BindableProperty);
    }


    public void Dispose()
    {
        NavigationProvider.Navigation = null;
    }
}
