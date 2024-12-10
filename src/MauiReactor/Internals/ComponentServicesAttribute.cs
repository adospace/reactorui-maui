namespace MauiReactor.HotReload;

/// <summary>
/// Attribute to mark a method as a component service method: the method is called to register services when the assembly is hot-reloaded.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ComponentServicesAttribute : Attribute
{
}