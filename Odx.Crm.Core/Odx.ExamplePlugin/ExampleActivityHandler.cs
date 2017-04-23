using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Crm.Core;
using Odx.Crm.Core.DataAccess;

namespace Odx.ExamplePlugin
{
    public class ExampleActivityHandler : ActivityHandlerBase<ExampleActivity>, IActivityHandler
    {
        public void Execute(IWorkflowContext context, IRepositoryFactory repositoryFactory)
        {
            var inDateTime = this.GetContextProperty(ActivityData.InDateTime);
            var inValue = this.GetContextProperty(ActivityData.InValue);
            var inUnit = this.GetContextProperty(ActivityData.InUnit);
            this.SetContextProperty(ActivityData.OutResult, DateTime.Now);
        }
    }
}
