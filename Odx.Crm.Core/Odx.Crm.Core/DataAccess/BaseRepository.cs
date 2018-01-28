using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace Odx.Xrm.Core.DataAccess
{
    internal class BaseRepository : IBaseRepository
    {
        protected IOrganizationService service;

        public BaseRepository(IOrganizationService service)
        {
            this.service = service;
        }

        public TResponse Execute<VRequest, TResponse>(VRequest request)
            where TResponse : OrganizationResponse
            where VRequest : OrganizationRequest
        {
            return (TResponse)this.service.Execute(request);
        }
    }

    internal class BaseRepository<T> : BaseRepository, IBaseRepository<T>
        where T : Entity, new()
    {
        public BaseRepository(IOrganizationService service) : base(service) { }

        public Guid Create(T entity)
        {
            return this.service.Create(entity);
        }

        public void Update(T entity)
        {
            this.service.Update(entity);
        }

        public void Delete(T entity)
        {
            this.service.Delete(entity.LogicalName, entity.Id);
        }

        public T Retrieve(Guid id, params string[] columns)
        {
            var temp = new T();
            return Retrieve(id, temp.LogicalName, columns);
        }

        public T Retrieve(Guid id, string entityLogicalName, params string[] columns)
        {
            return this.service.Retrieve(
                entityLogicalName,
                id,
                columns != null ? new ColumnSet(columns) : new ColumnSet(true))
                .ToEntity<T>();
        }

        public T Retrieve(Guid id, Expression<Func<T, T>> constructor)
        {
            var temp = new T();
            return this.CustomRetrieve(
                ctx =>
                {
                    return ctx.CreateQuery<T>()
                    .Where(x => x.Id == id)
                    .Select(constructor)
                    .Single();
                });
        }

        public List<T> RetrieveAll(params string[] columns)
        {
            var paginator = new EntityCollectionPaginator<T>(this.service, columns);
            var entities = new List<T>();
            do
            {
                entities.AddRange(paginator.GetNextPage());
            } while (paginator.HasMore);

            return entities;
        }

        public U CustomRetrieve<U>(Func<OrganizationServiceContext, U> customRetriever)
        {
            using (var ctx = new OrganizationServiceContext(this.service) { MergeOption = MergeOption.NoTracking })
            {
                return customRetriever.Invoke(ctx);
            }
        }

        public void SetActivationState(T entity, int stateCode, int statusCode)
        {
            SetStateRequest setStateRequest = new SetStateRequest();
            setStateRequest.EntityMoniker = entity.ToEntityReference();
            setStateRequest.State = new OptionSetValue(stateCode);
            setStateRequest.Status = new OptionSetValue(statusCode);
            this.service.Execute(setStateRequest);
        }

        [Obsolete]
        public void Assign(T entity, EntityReference newOwner)
        {
            this.service.Execute(new AssignRequest()
            {
                Target = entity.ToEntityReference(),
                Assignee = newOwner
            });
        }

        public void Associate(T entity, string relationshipName, EntityReferenceCollection relatedEntities)
        {
            this.Associate(entity, new Relationship(relationshipName), relatedEntities);
        }

        public void Associate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.service.Associate(entity.LogicalName, entity.Id, relationship, relatedEntities);
        }

        public void Disassociate(T entity, string relationshipName, EntityReferenceCollection relatedEntities)
        {
            this.Disassociate(entity, new Relationship(relationshipName), relatedEntities);
        }

        public void Disassociate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.service.Disassociate(entity.LogicalName, entity.Id, relationship, relatedEntities);
        }

        public void GrantAccess(T entity, EntityReference principal, AccessRights accessRights)
        {
            this.service.Execute(new GrantAccessRequest()
            {
                Target = entity.ToEntityReference(),
                PrincipalAccess = new PrincipalAccess()
                {
                    Principal = principal,
                    AccessMask = accessRights,
                }
            });
        }

        public void RevokeAccess(T entity, EntityReference principal)
        {
            this.service.Execute(new RevokeAccessRequest()
            {
                Target = entity.ToEntityReference(),
                Revokee = principal,
            });
        }

        public Guid AddToQueue(T entity, Guid destinationQueueId, Guid? sourceQueueId = null)
        {
            var addToQueueRequest = new AddToQueueRequest();
            if (sourceQueueId.HasValue)
            {
                addToQueueRequest.SourceQueueId = sourceQueueId.Value;
            }

            addToQueueRequest.Target = entity.ToEntityReference();
            addToQueueRequest.DestinationQueueId = destinationQueueId;
            AddToQueueResponse response = (AddToQueueResponse)this.service.Execute(addToQueueRequest);
            return response.QueueItemId;
        }
    }
}