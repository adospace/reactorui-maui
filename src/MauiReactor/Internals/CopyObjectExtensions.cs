using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MauiReactor.Internals
{
    internal static class CopyObjectExtensions
    {
        public static void CopyProperties(this object source, object dest)
        {
            var sourceProps = source
                .GetType()
                .GetProperties()
                .Where(x => x.CanRead)
                .ToList();

            var destProps = dest
                .GetType()
                .GetProperties()
                .Where(_ => _.CanWrite)
                .ToDictionary(_ => _.Name, _ => _);

            foreach (var sourceProp in sourceProps)
            {
                if (!destProps.TryGetValue(sourceProp.Name, out var destProp))
                    continue;

                var sourceValue = sourceProp.GetValue(source, null);

                try
                {
                    if (sourceValue != null &&
                        sourceProp.PropertyType != destProp.PropertyType)
                    {
                        var sourceValueType = sourceValue.GetType();
                        if (sourceValueType.IsEnum)
                        {
                            var underlyingTypeAsNullable = Nullable.GetUnderlyingType(sourceProp.PropertyType);
                            if (underlyingTypeAsNullable != null)
                                sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(underlyingTypeAsNullable));
                            else
                                sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
                        }
                    }

                    destProp.SetValue(dest, sourceValue, null);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to copy property '{destProp.Name}' of state ({source?.GetType()}) to new state after hot reload:{Environment.NewLine}{ex})");
                }
            }
        }

        private static string DumpExceptionMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return $"{ex.GetType().Name}({DumpExceptionMessage(ex.InnerException)})";
            }

            return ex.Message;
        }

    }
}
