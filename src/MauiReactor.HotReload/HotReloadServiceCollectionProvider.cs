using MauiReactor.Internals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MauiReactor.HotReload;

internal class HotReloadServiceCollectionProvider : ServiceCollectionProvider
{
    protected override void SetServiceProvider(IServiceProvider? serviceProvider)
    {
        if (serviceProvider != null)
        {
            base.SetServiceProvider(new ServiceProviderWithHotReloadedServices(serviceProvider));
        }
        else
        {
            base.SetServiceProvider(serviceProvider);
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
            //if (MauiReactorFeatures.HotReloadIsEnabled)
            {
                //1. find the first static method in the assembly that has the attribute ComponentServicesAttribute
                //2. if it exists it must accept a IServiceCollection as a parameter
                //3. create a ServiceCollection and pass it to the method
                //4. build a new service provider
                //5. save the save provider and the assembly as local class field so to not recreate it every time
                //6. return the service from the new service provider
                var hotReloadTypeLoader = (HotReloadTypeLoader)HotReloadTypeLoader.Instance;

                if (hotReloadTypeLoader.LastLoadedAssembly != null &&
                    _lastParseAssembly != hotReloadTypeLoader.LastLoadedAssembly)
                {
                    _lastParseAssembly = hotReloadTypeLoader.LastLoadedAssembly;

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
