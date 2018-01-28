using System;
using Microsoft.Xrm.Sdk;

namespace Odx.Xrm.Core.Utilities
{
    static class InterfaceMocker
    {
        public static T Mock<T>(Entity entity)
            where T : IEntityExtension
        {
            var type = ProxyTypesCache.GetProxyType(entity.LogicalName);
            var copy = Activator.CreateInstance(type);
            (copy as Entity).Attributes = entity.Attributes;
            (copy as Entity).Id = entity.Id;
            if (typeof(T).IsAssignableFrom(type))
            {
                return (T)copy;
            }
            else
            {
                throw new InvalidPluginExecutionException("Cannot mock interface. Target type does not implement interface " + typeof(T).Name);
            }
        }
    }
}
