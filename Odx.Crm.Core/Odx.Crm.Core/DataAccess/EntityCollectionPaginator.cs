using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Odx.Xrm.Core.DataAccess
{
    public class EntityCollectionPaginator<T>
        where T : Entity, new()
    {
        private IOrganizationService service;
        private QueryExpression initialQuery;
        private bool moreResults;
        private PagingInfo pagingInfo;

        public EntityCollectionPaginator(IOrganizationService service, IPaginatorSettings settings) : this(service)
        {
            this.service = service;
            this.initialQuery = settings.Query;
            this.pagingInfo.Count = settings.PageSize;
        }

        public EntityCollectionPaginator(IOrganizationService service, params string[] columns)
        {
            this.service = service;
            this.initialQuery = new QueryExpression(new T().LogicalName);
            this.initialQuery.ColumnSet = columns.Length > 0 ? new ColumnSet(columns) : new ColumnSet(true);
            this.moreResults = true;
            this.pagingInfo = new PagingInfo();
            this.pagingInfo.Count = 100;
            this.pagingInfo.PageNumber = 1;
        }

        public int CurrentPage
        {
            get => this.pagingInfo.PageNumber - 1;
            }

        public bool HasMore
        {
            get => this.moreResults;
            }

        public List<T> GetNextPage()
        {
            var query = this.initialQuery;
            query.PageInfo = this.pagingInfo;
            var results = new List<T>();
            if (this.moreResults)
            {
                var collection = this.service.RetrieveMultiple(query);
                results = collection.Entities.Select(x => x.ToEntity<T>()).ToList<T>();

                this.moreResults = collection.MoreRecords;
                this.pagingInfo.PagingCookie = collection.PagingCookie;
                this.pagingInfo.PageNumber++;
            }

            return results;
        }

        public void Reset()
        {
            this.pagingInfo.PageNumber = 1;
            this.pagingInfo.PagingCookie = null;
        }
    }
}
