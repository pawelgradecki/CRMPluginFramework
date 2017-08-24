using Odx.Xrm.Core.DataAccess;

namespace Odx.Xrm.Core
{
    public interface IHandler : ITraceableObject
    {
        void Execute(ILocalPluginExecutionContext context, IRepositoryFactory repositoryFactory);
    }
}
