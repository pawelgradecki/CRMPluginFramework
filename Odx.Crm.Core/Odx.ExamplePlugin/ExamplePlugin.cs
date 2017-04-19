using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Odx.Crm.Core;
using Odx.Crm.Core.BusinessLogic.Handlers.Accounts;

namespace Odx.ExamplePlugin
{
    public class ExamplePlugin : BasePlugin<ExampleHandler>, IPlugin
    {
        public ExamplePlugin(string unsecureConfiguration, string secureConfiguration)
            : base(unsecureConfiguration, secureConfiguration) { }

        protected override void RegisterAvailableMessages()
        {
            this.RegisterMessagePost<CreateRequest>()
                .RegisterMessagePre<UpdateRequest>();
        }
    }
}
