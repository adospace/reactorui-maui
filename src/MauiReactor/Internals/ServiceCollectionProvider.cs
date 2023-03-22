namespace MauiReactor.Internals;

internal static class ServiceCollectionProvider
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;
}

public sealed class ServiceContext : IDisposable
{
    public ServiceContext(IServiceProvider serviceProvider)
    {
        ServiceCollectionProvider.ServiceProvider = serviceProvider;
    }

    public ServiceContext(Action<ServiceCollection> serviceCollectionSetupAction)
         : this( SetupServiceProvider(serviceCollectionSetupAction))
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