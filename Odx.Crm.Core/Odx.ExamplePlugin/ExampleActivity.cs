using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Workflow;
using Odx.Crm.Core;

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

        protected override IActivityHandler GetActivityHandler()
        {
            return new ExampleActivityHandler();
        }
    }
}
