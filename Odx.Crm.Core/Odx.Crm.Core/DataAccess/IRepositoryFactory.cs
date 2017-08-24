using System;

namespace Odx.Xrm.Core.DataAccess
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// /Get Repository of given type
        /// </summary>
        /// <typeparam name="T">Type of repository</typeparam>
        /// <returns>Repository</returns>
        T Get<T>() where T : IBaseRepository;

        /// <summary>
        /// Gets Repository of given type in given user context
        /// </summary>
        /// <typeparam name="T">Repository type</typeparam>
        /// <param name="userId">User context of repository</param>
        /// <returns>Repository</returns>
        T Get<T>(Guid? userId) where T : IBaseRepository;
    }
}