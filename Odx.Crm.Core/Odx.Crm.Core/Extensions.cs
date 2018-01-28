using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;

namespace Odx.Xrm.Core
{
    public static class Extensions
    {
        public static TAttribute GetCustomAttribute<TAttribute>(this Type type)
        where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;

            return att;
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this PropertyInfo propertyInfo)
        where TAttribute : Attribute
        {
            var att = propertyInfo.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;

            return att;
        }

        public static string ToJSON<T>(this T obj, JsonSerializerSettings settings = null) where T : class
        {
            if (settings == null)
            {
                return JsonConvert.SerializeObject(obj);
            }

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static T FromJSON<T>(this T obj, string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static bool IsNullOrEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        public static void Trace(this ITracingService tracingService, Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            tracingService.Trace($"Exception: {ex?.Message}");
            tracingService.Trace($"StackTrace: {ex?.StackTrace}");
            tracingService.Trace("InnerException: \n");
            tracingService.Trace(ex?.InnerException);
        }
    }
}
