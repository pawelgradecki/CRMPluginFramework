using System;
using System.Activities;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk.Workflow;

namespace Odx.Xrm.Core
{
    internal class LocalWorkflowExecutionContext : ILocalWorkflowExecutionContext
    {
        private CodeActivityContext codeActivityContext;
        private IWorkflowContext workflowContext;

        public LocalWorkflowExecutionContext(CodeActivityContext codeActivityContext)
        {
            this.codeActivityContext = codeActivityContext;
            this.workflowContext = codeActivityContext.GetExtension<IWorkflowContext>();
        }

        public IWorkflowContext Context => this.workflowContext;

        public T GetContextProperty<T>(InArgument<T> inArgument)
        {
            return inArgument.Get<T>(this.codeActivityContext);
        }

        public void SetContextProperty<T>(OutArgument<T> outArgument, T value)
        {
            outArgument.Set(this.codeActivityContext, value);
        }
    }
}
