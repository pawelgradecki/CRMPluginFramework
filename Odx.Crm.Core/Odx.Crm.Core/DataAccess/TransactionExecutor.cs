using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

namespace Odx.Xrm.Core.DataAccess
{
    /// <summary>
    /// Helps to execute requests in transactions
    /// </summary>
    public class TransactionExecutor
    {
        private OrganizationRequestCollection requestsToExecute;
        private IOrganizationService service;

        public event EventHandler<RequestAddedEventArgs> RequestAdded;
        public event EventHandler ExecutionStarted;
        public event EventHandler ExecutionFinished;

        public TransactionExecutor(IOrganizationService service)
        {
            this.service = service;
            this.requestsToExecute = new OrganizationRequestCollection();
        }

        public TransactionExecutor AddToTransaction<T>(T request)
            where T : OrganizationRequest
        {
            this.requestsToExecute.Add(request);
            this.OnRequestAdded(request);
            return this;
        }

        public TransactionExecutor AddToTransaction<T>(IEnumerable<T> requestCollection)
            where T : OrganizationRequest
        {
            foreach (var item in requestCollection)
            {
                this.AddToTransaction(item);
            }

            return this;
        }

        public void Execute()
        {
            this.OnExecutionStarted();

            if (this.requestsToExecute.Count > 0)
            {
                var transactionRequest = new ExecuteTransactionRequest();
                transactionRequest.Requests = this.requestsToExecute;
                service.Execute(transactionRequest);
            }

            this.OnExecutionFinished();
        }

        private void OnRequestAdded(OrganizationRequest request)
        {
            this.RequestAdded?.Invoke(this, new RequestAddedEventArgs(request));
        }

        private void OnExecutionStarted()
        {
            this.ExecutionStarted?.Invoke(this, EventArgs.Empty);
        }

        private void OnExecutionFinished()
        {
            this.ExecutionFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}
