using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Odx.Xrm.Core.DataAccess
{
    public interface IBaseRepository
    {
        TResponse Execute<VRequest, TResponse>(VRequest request)
            where VRequest : OrganizationRequest
            where TResponse : OrganizationResponse;
    }

    public interface IBaseRepository<T> : IBaseRepository
        where T : Entity, new()
    {
        Guid Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Retrieve(Guid id, params string[] columns);

        T Retrieve(Guid id, string entityLogicalName, params string[] columns);

        T Retrieve(Guid id, Expression<Func<T, T>> constructor);

        List<T> RetrieveAll(params string[] columns);

        U CustomRetrieve<U>(Func<OrganizationServiceContext, U> customRetriever);

        void SetActivationState(T entity, int stateCode, int statusCode);

        [Obsolete]
        void Assign(T entity, EntityReference newOwner);

        void Associate(T entity, string relationshipName, EntityReferenceCollection relatedEntities);

        void Associate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities);

        void Disassociate(T entity, string relationshipName, EntityReferenceCollection relatedEntities);

        void Disassociate(T entity, Relationship relationship, EntityReferenceCollection relatedEntities);

        void GrantAccess(T entity, EntityReference principal, AccessRights accessRights);

        void RevokeAccess(T entity, EntityReference principal);

        Guid AddToQueue(T entity, Guid destinationQueueId, Guid? sourceQueueId = null);
    }
}