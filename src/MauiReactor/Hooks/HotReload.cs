using System.Threading;

namespace MauiReactor;

/// <summary>
/// Marks a state object that lives in MauiReactor's own assembly and can therefore be
/// handed to the next component generation whole, instead of going through the reflective
/// property copy that hot reload uses for app-assembly state types.
/// </summary>
internal interface IHotReloadTransferable;

/// <summary>
/// Monotonic counter bumped once per hot-reload pass. Both regimes - a MetadataUpdate
/// under <c>dotnet watch</c> and a full assembly load under <c>dotnet-maui-reactor</c> -
/// funnel through <c>PageHost.OnAssemblyChanged</c>, so one bump site covers both.
/// </summary>
internal static class HotReloadGeneration
{
    private static int _current;

    internal static int Current => Volatile.Read(ref _current);

    internal static void Bump() => Interlocked.Increment(ref _current);
}
