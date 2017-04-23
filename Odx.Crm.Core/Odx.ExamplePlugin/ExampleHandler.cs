using Microsoft.Xrm.Sdk;
using Odx.Crm.Core;
using Odx.Crm.Core.Model;
using Odx.Crm.Core.DataAccess;
using System;

namespace Odx.Crm.Core.BusinessLogic.Handlers.Accounts
{
    public class ExampleHandler : HandlerBase, IHandler
    {
        public void Execute(ILocalPluginExecutionContext context, IRepositoryFactory repositoryFactory)
        {
            var strongContext = context.ToEntity<Account>();
            var generalRepository = repositoryFactory.Get<IBaseRepository<Entity>>();
        }
    }
}