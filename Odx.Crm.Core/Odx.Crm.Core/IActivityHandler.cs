using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.Core
{
    public interface IActivityHandler : ITraceableObject
    {
        void Initialize(CodeActivityContext ctx, CodeActivity activity);

        void Execute(IWorkflowContext context, IRepositoryFactory repositoryFactory);
    }
}
