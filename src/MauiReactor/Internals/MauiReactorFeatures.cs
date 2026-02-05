using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor.Internals;

internal static class MauiReactorFeatures
{
    //[FeatureSwitchDefinition("MauiReactor.HotReload")]
    //public static bool HotReloadIsEnabled
    //    => AppContext.TryGetSwitch("MauiReactor.HotReload", out var isEnabled) && isEnabled;

    [FeatureSwitchDefinition("MauiReactor.FrameRate")]
    public static bool FrameRateIsEnabled
        => AppContext.TryGetSwitch("MauiReactor.FrameRate", out var isEnabled) && isEnabled;

}
