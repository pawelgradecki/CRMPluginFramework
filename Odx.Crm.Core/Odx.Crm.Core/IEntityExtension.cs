using System;

namespace Odx.Xrm.Core
{
    //marker interface
    public interface IEntityExtension
    {
        Guid Id { get; set; }
        string LogicalName { get; set; }
    }
}
