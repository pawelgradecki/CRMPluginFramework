using Microsoft.Xrm.Sdk;
using Odx.Crm.Core;
using Odx.Crm.Core.Model;
using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.CoreLib.BusinessLogic.Handlers.Accounts
{
    public class ExampleHandler : HandlerBase<Account>, IHandler
    {
        public bool CanExecute
        {
            get
            {

                if (this.ExecutionData.Context.Depth > 1)
                {
                    //Update from post update, skip calculation
                    return false;
                }

                var target = this.ExecutionData.Target;
                var postImage = this.ExecutionData.PostImage;
                return false;
            }
        }

        public void Execute()
        {
            var target = this.ExecutionData.Target;
            var generalRepository = this.Repositories.Get<IBaseRepository<Entity>>();
            generalRepository.Update(target);
        }
    }
}
