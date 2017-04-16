using System;
using Microsoft.Xrm.Sdk.Query;

namespace Odx.Crm.Core.DataAccess
{
    public class PaginatorSettings : IPaginatorSettings
    {
        public int PageSize { get; set; }
        public QueryExpression Query { get; set; }
    }
}