using MauiReactor.HotReload;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace MauiReactor.Internals;

internal static class ServiceCollectionProvider
{
    static IServiceProvider? _serviceProvider = null!;

    public static IServiceProvider? ServiceProvider
    { 
        get => _serviceProvider;
        set
        {
            if (value != null)
            {
                if (MauiReactorFeatures.HotReloadIsEnabled)
                {
                    _serviceProvider = new ServiceProviderWithHotReloadedServices(value);
                }
                else
                {
                    _serviceProvider = value;
                }
            }
            else
            {
                _serviceProvider = null!;
            }
        }
    }
}

internal class ServiceProviderWithHotReloadedServices : IServiceProvider
{
    private readonly IServiceProvider _serviceProvider;
    private Assembly? _lastParseAssembly;
    private IServiceProvider? _lastServiceProvider;

    public ServiceProviderWithHotReloadedServices(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object? GetService(Type serviceType)
    {
        var service = _serviceProvider.GetService(serviceType);
        if (service == null)
        {
            if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                //1. find the first static method in the assembly that has the attribute ComponentServicesAttribute
                //2. if it exists it must accept a IServiceCollection as a parameter
                //3. create a ServiceCollection and pass it to the method
                //4. build a new service provider
                //5. save the save provider and the assembly as local class field so to not recreate it every time
                //6. return the service from the new service provider

                if (HotReloadTypeLoader.Instance.LastLoadedAssembly != null &&
                    _lastParseAssembly != HotReloadTypeLoader.Instance.LastLoadedAssembly)
                {
                    _lastParseAssembly = HotReloadTypeLoader.Instance.LastLoadedAssembly;

                    var servicesBuilderMethods = _lastParseAssembly
                        .GetTypes()
                        .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                        .Where(m => m.GetCustomAttribute<ComponentServicesAttribute>() != null)
                        .Where(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(IServiceCollection))
                        .ToList();

                    if (servicesBuilderMethods.Count > 0)
                    {
                        var services = new ServiceCollection();

                        foreach (var servicesBuilderMethod in servicesBuilderMethods)
                        {
                            servicesBuilderMethod.Invoke(null, [services]);
                        }

                        //get the service descriptors list from the _serviceProvider using reflection
                        var entryAssembly = Assembly.GetEntryAssembly();
                        var existingServices = services.Select(_ => _.ServiceType.AssemblyQualifiedName).ToHashSet();

                        foreach (var serviceDescriptor in _serviceProvider.GetServiceDescriptors())
                        {
                            if (existingServices.Contains(serviceDescriptor.ServiceType.AssemblyQualifiedName))
                            {
                                continue;
                            }

                            services.Add(serviceDescriptor);
                        }

                        _lastServiceProvider = services.BuildServiceProvider();
                    }
                }

                if (_lastServiceProvider != null)
                {
                    return _lastServiceProvider.GetService(serviceType);
                }
            }
        }

        return service;
    }
}

internal static class ServiceProviderExtensions
{
    public static ServiceDescriptor[] GetServiceDescriptors(this IServiceProvider serviceProvider)
    {
        //serviceProvider.RootProvider.CallSiteFactory.Descriptors
        var rootProvider = serviceProvider
            .GetType()
            .GetProperty("RootProvider", BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(serviceProvider)!;

        var callSiteFactory = rootProvider
            .GetType()
            .GetProperty("CallSiteFactory", BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(rootProvider)!;

        var serviceDescriptors = callSiteFactory
            .GetType()
            .GetProperty("Descriptors", BindingFlags.Instance | BindingFlags.NonPublic)!
            .GetValue(callSiteFactory)!;

        return (ServiceDescriptor[])serviceDescriptors;
    }
}

public sealed class ServiceContext : IDisposable
{
    public ServiceContext(IServiceProvider serviceProvider)
    {
        ServiceCollectionProvider.ServiceProvider = serviceProvider;
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
        ServiceCollectionProvider.ServiceProvider = null!;
    }
}