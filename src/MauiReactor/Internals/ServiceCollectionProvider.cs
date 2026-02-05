using MauiReactor.HotReload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MauiReactor.Internals;

internal class ServiceCollectionProvider
{
    static readonly AsyncLocal<IServiceProvider?> _testingServiceProvider = new();

    static ServiceCollectionProvider? _instance;

    IServiceProvider? _serviceProvider;

    public static ServiceCollectionProvider Instance
    {
        get => _instance ??= new();
        set => _instance = value;
    }

    protected virtual IServiceProvider? GetServiceProvider()
    {
        return _serviceProvider;
    }

    protected virtual void SetServiceProvider(IServiceProvider? serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static IServiceProvider? ServiceProvider
    {
        get => _testingServiceProvider.Value ?? Instance.GetServiceProvider();
        set
        {
            Instance.SetServiceProvider(value);
            //if (value != null)
            //{
            //    if (MauiReactorFeatures.HotReloadIsEnabled)
            //    {
            //        _serviceProvider = new ServiceProviderWithHotReloadedServices(value);
            //    }
            //    else
            //    {
            //        _serviceProvider = value;
            //    }
            //}
            //else
            //{
            //    _serviceProvider = null!;
            //}
        }
    }

    internal static void SetTestingServiceProvider(IServiceProvider? serviceProvider)
    {
        _testingServiceProvider.Value = serviceProvider;
    }
}


/// <summary>
/// Used to provide an isolated service provider for testing purposes. This Context should not
/// be used outside of testing.
/// </summary>

public sealed class ServiceContext : IDisposable
{
    public ServiceContext(IServiceProvider serviceProvider)
    {
        ServiceCollectionProvider.SetTestingServiceProvider(serviceProvider);
    }

    public ServiceContext(Action<ServiceCollection> serviceCollectionSetupAction)
         : this(SetupServiceProvider(serviceCollectionSetupAction))
    {
    }

    private static IServiceProvider SetupServiceProvider(Action<ServiceCollection>? serviceCollectionSetupAction = null)
    {
        var services = new ServiceCollection();
        serviceCollectionSetupAction?.Invoke(services);
        return services.BuildServiceProvider();
    }

    public void Dispose()
    {
        ServiceCollectionProvider.SetTestingServiceProvider(null);
    }
}