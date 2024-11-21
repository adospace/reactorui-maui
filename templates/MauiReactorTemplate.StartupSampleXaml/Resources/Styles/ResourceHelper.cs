using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactorTemplate.StartupSampleXaml.Resources.Styles;

static class ResourceHelper
{
    public static T GetResource<T>(string key)
    {
        if (Application.Current!.Resources.MergedDictionaries.ElementAt(0)
            .TryGetValue(key, out object? v1))
        {
            return (T)v1;
        }
        if (Application.Current!.Resources.MergedDictionaries.ElementAt(1)
            .TryGetValue(key, out object? v2))
        {
            return (T)v2;
        }

        return (T)Application.Current!.Resources.MergedDictionaries.ElementAt(2)[key];
    }
}
