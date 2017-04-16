using Microsoft.Xrm.Sdk.Query;

namespace Odx.Crm.Core.DataAccess
{
    public interface IPaginatorSettings
    {
        int PageSize { get; set; }
        QueryExpression Query { get; set; }
    }
}