using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Xrm.Core.DataAccess;

namespace Odx.Xrm.Core
{
    public interface IActivityHandler : ITraceableObject
    {
        void Initialize(CodeActivityContext ctx, CodeActivity activity);

        void Execute(IWorkflowContext context, IRepositoryFactory repositoryFactory);
    }
}
