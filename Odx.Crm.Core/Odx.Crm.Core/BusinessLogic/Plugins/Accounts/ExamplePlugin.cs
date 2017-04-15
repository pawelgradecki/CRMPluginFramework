using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Odx.Crm.Core;
using Odx.Crm.CoreLib.BusinessLogic.Handlers.Accounts;

namespace Odx.Crm.CoreLib.Plugins.Accounts
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
