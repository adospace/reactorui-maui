using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiReactor;

public static class ResourceManager
{
    static readonly Dictionary<string, object> _cachedResources = [];

    public static T? Find<T>(string name) where T : class
    {
        if (_cachedResources.TryGetValue(name, out var value))
        {
            return (T)value;
        }

        if (Application.Current != null &&
            Application.Current.Resources != null)
        {
            var foundResource = Application.Current.Resources.Find<T>(name);
            _cachedResources.Add(name, foundResource);
            return foundResource;
        }

        return null;
    }

    public static T Find<T>(this ResourceDictionary resourceDictionary, string name) where T : class
    {
        if (resourceDictionary.TryGetValue(name, out var value))
        {
            return (T)value;
        }

        foreach (var mergedDictionary in resourceDictionary.MergedDictionaries)
        {
            var foundResource = mergedDictionary.Find<T>(name);
            if (foundResource != null)
            {
                return foundResource;
            }
        }

        return null!;
    }

    public static Style? FindStyle(string name)
        => Find<Style>(name);

}