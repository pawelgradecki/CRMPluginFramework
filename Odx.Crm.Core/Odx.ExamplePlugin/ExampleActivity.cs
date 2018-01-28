using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Xrm.Core;
using Odx.Xrm.Core.DataAccess;

namespace Odx.ExamplePlugin
{
    public class ExampleActivity : BaseCustomActivity
    {
        [Input("Input DateTime")]
        [RequiredArgument]
        public InArgument<DateTime> InDateTime { get; set; }

        [Input("Value")]
        [RequiredArgument]
        public InArgument<int> InValue { get; set; }

        [Input("Unit")]
        [RequiredArgument]
        public InArgument<string> InUnit { get; set; }

        [Output("Calculate Date Time Result")]
        public OutArgument<DateTime> OutResult { get; set; }

        public override void Execute(ILocalWorkflowExecutionContext context, IRepositoryFactory repositoryFactory, ITracingService tracingService)
        {
            throw new NotImplementedException();
        }
    }
}
