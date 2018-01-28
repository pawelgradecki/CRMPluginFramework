using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Odx.Xrm.Core;
using Odx.Xrm.Core.DataAccess;

namespace Odx.ExamplePlugin
{
    public class ExamplePlugin : BasePlugin, IPlugin
    {
        public ExamplePlugin(string unsecureConfiguration, string secureConfiguration)
            : base(unsecureConfiguration, secureConfiguration) { }

        public override bool CanExecute(ILocalPluginExecutionContext localContext, IRepositoryFactory repoFactory, ITracingService service)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(ILocalPluginExecutionContext localContext, IRepositoryFactory repoFactory, ITracingService service)
        {
            throw new System.NotImplementedException();
        }

        protected override void RegisterAvailableMessages()
        {
            this.RegisterMessagePost<CreateRequest>()
                .RegisterMessagePre<UpdateRequest>();
        }
    }
}
