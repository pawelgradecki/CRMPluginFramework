using System;
using Odx.Crm.Core;
using Odx.Crm.Core.BusinessLogic.CustomActivities;

namespace Odx.Crm.Core.BusinessLogic.Handlers
{
    public class ExampleActivityHandler : ActivityHandlerBase<ExampleActivity>, IActivityHandler
    {
        public bool CanExecute
        {
            get
            {
                return true;
            }
        }

        public void Execute()
        {
            var inDateTime = this.GetContextProperty<DateTime>(ActivityData.InDateTime);
            var inValue = this.GetContextProperty<int>(ActivityData.InValue);
            var inUnit = this.GetContextProperty<string>(ActivityData.InUnit);
            this.SetContextProperty<DateTime>(ActivityData.OutResult, DateTime.Now);
        }
    }
}