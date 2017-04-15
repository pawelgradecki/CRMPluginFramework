using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.Core
{
    public class ActivityHandlerBase<T> : TraceableObject
        where T : CodeActivity
    {
        private CodeActivityContext activityContext;
        public T ActivityData { get; private set; }
        protected IWorkflowContext WorkflowContext { get; private set; }
        protected IRepositoryFactory Repositories { get; private set; }
        private void InitializeInternal(CodeActivityContext context, T activity)
        {
            this.activityContext = context;
            this.ActivityData = activity;
            this.tracingService = this.activityContext.GetExtension<ITracingService>();
            this.WorkflowContext = this.activityContext.GetExtension<IWorkflowContext>();
            var factory = this.activityContext.GetExtension<IOrganizationServiceFactory>();
            this.Repositories = new RepositoryFactory(factory);
        }

        public void Initialize(CodeActivityContext context, CodeActivity activity)
        {
            this.InitializeInternal(context, (T)activity);
        }

        protected V GetContextProperty<V>(InArgument<V> inValue)
        {
            return inValue.Get<V>(this.activityContext);
        }

        protected void SetContextProperty<V>(OutArgument<V> outValue, V value)
        {
            outValue.Set(this.activityContext, value);
        }
    }
}
