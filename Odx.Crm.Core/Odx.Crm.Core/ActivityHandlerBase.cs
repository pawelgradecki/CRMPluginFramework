using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Xrm.Core.DataAccess;

namespace Odx.Xrm.Core
{
    public class ActivityHandlerBase<T> : TraceableObject
        where T : CodeActivity
    {
        private CodeActivityContext activityContext;
        public T ActivityData { get; private set; }
        protected IWorkflowContext WorkflowContext { get; private set; }
        private void InitializeInternal(CodeActivityContext context, T activity)
        {
            this.activityContext = context;
            this.ActivityData = activity;
            this.tracingService = context.GetExtension<ITracingService>();
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
