using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Odx.Xrm.Core.Utilities
{
    static class ProxyTypesCache
    {
        private static Dictionary<string, Type> cachedTypes = new Dictionary<string, Type>();
        static ProxyTypesCache()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var allTypes = assembly.GetExportedTypes();
            foreach (var type in allTypes)
            {
                if (typeof(Entity).IsAssignableFrom(type))
                {
                    var logicalAttributeName = type.GetCustomAttribute<EntityLogicalNameAttribute>();
                    if (logicalAttributeName != null)
                    {
                        cachedTypes.Add(logicalAttributeName.LogicalName, type);
                    }
                }
            }
        }

        public static Type GetProxyType(string logicalName)
        {
            return cachedTypes[logicalName];
        }
    }
}
