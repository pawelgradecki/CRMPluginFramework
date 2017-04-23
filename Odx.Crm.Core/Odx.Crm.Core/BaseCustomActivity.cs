using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.Core
{
    public abstract class BaseCustomActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            var handler = this.GetActivityHandler();
            handler.Initialize(context, this);
            var repositoryFactory = this.GetRepositoryFactory(context);
            var workflowContext = this.GetWorkflowContext(context);
            handler.Execute(workflowContext, repositoryFactory);
        }

        private IWorkflowContext GetWorkflowContext(CodeActivityContext context)
        {
            return context.GetExtension<IWorkflowContext>();
        }

        private IRepositoryFactory GetRepositoryFactory(CodeActivityContext context)
        {
            var factory = context.GetExtension<IOrganizationServiceFactory>();
            return new RepositoryFactory(factory);
        }

        protected abstract IActivityHandler GetActivityHandler();
    }
}
