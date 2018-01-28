using Microsoft.Xrm.Sdk;

namespace Odx.Xrm.Core
{
    public interface IEntityWithOwner : IEntityExtension
    {
        EntityReference OwnerId { get; set; }
    }
}
