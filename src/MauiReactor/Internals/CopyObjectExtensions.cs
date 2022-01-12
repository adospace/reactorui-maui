using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MauiReactor.Internals
{
    internal static class CopyObjectExtensions
    {
        public static void CopyPropertiesTo(this object source, object dest, PropertyInfo[] destProps)
        {
            if (source.GetType().FullName != dest.GetType().FullName)
            {
                //can't copy state over a type with a different name: surely it's a different type
                return;
            }

            var sourceProps = source.GetType()
                .GetProperties()
                .Where(x => x.CanRead)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                var targetProperty = destProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProperty != null)
                {
                    var sourceValue = sourceProp.GetValue(source, null);
                    if (sourceValue != null && sourceValue.GetType().IsEnum)
                    {
                        sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
                    }

                    try
                    {
                        targetProperty.SetValue(dest, sourceValue, null);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Unable to copy property '{targetProperty.Name}' of state ({source.GetType()}) to new state after hot reload (Exception: '{DumpExceptionMessage(ex)}')");
                    }
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
