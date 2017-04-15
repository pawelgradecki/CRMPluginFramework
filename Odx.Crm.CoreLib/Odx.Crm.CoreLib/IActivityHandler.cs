using System.Activities;

namespace Odx.Crm.Core
{
    public interface IActivityHandler : IHandler
    {
        void Initialize(CodeActivityContext ctx, CodeActivity activity);
    }
}
