using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xrm.Sdk;

namespace Odx.Crm.Core.DataAccess
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private static Dictionary<Type, Type> mapping = new Dictionary<Type, Type>();

        private IOrganizationServiceFactory serviceFactory;

        static RepositoryFactory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var allRepositories = assembly.GetExportedTypes().Where(x => typeof(IBaseRepository).IsAssignableFrom(x));
            foreach (var repo in allRepositories)
            {
                if (repo.IsGenericType)
                {
                    continue;
                }

                AddMapping<IBaseRepository>(repo);
            }            
        }

        private static void AddMapping<TInterface>(Type implementation)
            where TInterface : IBaseRepository
        {
            mapping.Add(typeof(TInterface), implementation);
        }

        public RepositoryFactory(IOrganizationServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public T Get<T>()
            where T : IBaseRepository
        {
            return this.Get<T>(Guid.Empty);
        }

        /// <summary>
        /// Gets repository in given user's context
        /// </summary>
        /// <typeparam name="T">Repository type</typeparam>
        /// <param name="callerId">User in which service context should be instantiated</param>
        /// <returns>Repository</returns>
        public T Get<T>(Guid? callerId)
            where T : IBaseRepository
        {
            var serviceInContext = this.serviceFactory.CreateOrganizationService(callerId);
            if (typeof(T).IsGenericType)
            {
                var genericArg = typeof(T).GenericTypeArguments[0];
                var genericType = typeof(BaseRepository<>).MakeGenericType(genericArg);
                return (T)Activator.CreateInstance(genericType, serviceInContext);
            }
            else
            {
                var repository = mapping[typeof(T)];
                return (T)Activator.CreateInstance(repository, serviceInContext);
            }
        }

    }
}
