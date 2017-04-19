using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.Core
{
    public interface IHandler : ITraceableObject
    {
        void Execute(ILocalPluginExecutionContext context, IRepositoryFactory repositoryFactory);
    }
}
