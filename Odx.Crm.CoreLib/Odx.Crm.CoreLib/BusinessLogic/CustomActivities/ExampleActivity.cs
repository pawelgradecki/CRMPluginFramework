using System;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Crm.Core;
using Odx.Crm.CoreLib.BusinessLogic.Handlers;

namespace Odx.Crm.CoreLib.BusinessLogic.CustomActivities
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

        protected override IActivityHandler GetActivityHandler()
        {
            return new ExampleActivityHandler();
        }
    }
}
