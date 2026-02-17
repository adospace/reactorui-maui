//namespace MauiReactor.Internals
//{
//    internal static class CopyObjectExtensions
//    {
//        public static void CopyProperties(object source, object dest)
//        {
//            if (!MauiReactorFeatures.HotReloadIsEnabled)
//            {
//                return;
//            }
                
//            var sourceProps = source
//                .GetType()
//                .GetProperties()
//                .Where(x => x.CanRead)
//                .ToList();

//            var destProps = dest
//                .GetType()
//                .GetProperties()
//                .Where(_ => _.CanWrite)
//                .ToDictionary(_ => _.Name, _ => _);

//            foreach (var sourceProp in sourceProps)
//            {
//                if (!destProps.TryGetValue(sourceProp.Name, out var destProp))
//                    continue;

//                var sourceValue = sourceProp.GetValue(source, null);

//                try
//                {
//                    if (sourceValue != null &&
//                        sourceProp.PropertyType != destProp.PropertyType)
//                    {
//                        var sourceValueType = sourceValue.GetType();
//                        if (sourceValueType.IsEnum)
//                        {
//                            var underlyingTypeAsNullable = Nullable.GetUnderlyingType(sourceProp.PropertyType);
//                            if (underlyingTypeAsNullable != null)
//                                sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(underlyingTypeAsNullable));
//                            else
//                                sourceValue = Convert.ChangeType(sourceValue, Enum.GetUnderlyingType(sourceProp.PropertyType));
//                        }
//                        else
//                        {
//                            System.Diagnostics.Debug.WriteLine($"[MauiReactor] Using Json serialization for property '{destProp.Name}' of state ({source?.GetType()}) to copy state to new component after hot-reload");
//                            var serializedSourceValue = System.Text.Json.JsonSerializer.Serialize(sourceValue);
//                            sourceValue = System.Text.Json.JsonSerializer.Deserialize(serializedSourceValue, destProp.PropertyType);
//                        }
//                    }

//                    destProp.SetValue(dest, sourceValue, null);
//                }
//                catch (Exception ex)
//                {
//                    System.Diagnostics.Debug.WriteLine($"[MauiReactor] Unable to copy property '{destProp.Name}' of state ({source?.GetType()}) to new state after hot-reload:{Environment.NewLine}{ex})");
//                }
//            }
//        }

//        private static string DumpExceptionMessage(Exception ex)
//        {
//            if (ex.InnerException != null)
//            {
//                return $"{ex.GetType().Name}({DumpExceptionMessage(ex.InnerException)})";
//            }

//            return ex.Message;
//        }

//    }
//}
