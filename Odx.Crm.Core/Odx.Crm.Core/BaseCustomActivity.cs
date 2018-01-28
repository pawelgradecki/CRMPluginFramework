using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Xrm.Core.DataAccess;

namespace Odx.Xrm.Core
{
    public abstract class BaseCustomActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            var repositoryFactory = this.GetRepositoryFactory(context);
            var workflowContext = new LocalWorkflowExecutionContext(context);
            var tracingService = this.GetTracingService(context);
            this.Execute(workflowContext, repositoryFactory, tracingService);
        }

        public abstract void Execute(ILocalWorkflowExecutionContext context, IRepositoryFactory repositoryFactory, ITracingService tracingService);

        private ITracingService GetTracingService(CodeActivityContext context)
        {
            return context.GetExtension<ITracingService>();
        }

        private IRepositoryFactory GetRepositoryFactory(CodeActivityContext context)
        {
            var factory = context.GetExtension<IOrganizationServiceFactory>();
            return new RepositoryFactory(factory);
        }
       
    }
}
