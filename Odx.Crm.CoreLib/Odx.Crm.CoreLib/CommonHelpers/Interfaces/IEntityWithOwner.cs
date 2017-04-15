using Microsoft.Xrm.Sdk;

namespace Odx.Crm.Core.CommonHelpers.Interfaces
{
    public interface IEntityWithOwner : IEntityExtension
    {
        EntityReference OwnerId { get; set; }
    }
}
