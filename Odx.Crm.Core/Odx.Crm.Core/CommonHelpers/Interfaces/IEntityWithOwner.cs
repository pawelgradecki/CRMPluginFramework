using Microsoft.Xrm.Sdk;

namespace Odx.Xrm.Core.CommonHelpers.Interfaces
{
    public interface IEntityWithOwner : IEntityExtension
    {
        EntityReference OwnerId { get; set; }
    }
}
