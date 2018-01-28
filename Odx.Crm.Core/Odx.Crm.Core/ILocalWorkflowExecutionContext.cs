using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace Odx.Xrm.Core
{
    public interface ILocalWorkflowExecutionContext
    {
        IWorkflowContext Context { get; }

        T GetContextProperty<T>(InArgument<T> inArgument);

        void SetContextProperty<T>(OutArgument<T> outArgument, T value);
    }
}