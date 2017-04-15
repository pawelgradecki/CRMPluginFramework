using System.Activities;

namespace Odx.Crm.Core
{
    public abstract class BaseCustomActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            var handler = this.GetActivityHandler();
            handler.Initialize(context, this);
            handler.Execute();
        }

        protected abstract IActivityHandler GetActivityHandler();
    }
}
