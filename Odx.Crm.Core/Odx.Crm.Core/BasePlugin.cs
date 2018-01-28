using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Odx.Xrm.Core.DataAccess;

namespace Odx.Xrm.Core
{
    public abstract class BasePlugin : IPlugin
    {
        private string unsecureConfig;
        private string secureConfig;
        private HashSet<string> availableMessages;

        protected string UnsecureConfig
        {
            get
            {
                return this.unsecureConfig;
            }
        }

        protected string SecureConfig
        {
            get
            {
                return this.secureConfig;
            }
        }

        /// <summary>
        /// Register all messages that this plugin is registered on using fluent RegisterMessage method Example:
        /// RegisterMessage<CreateRequest>().RegisterMessage<UpdateRequest>()
        /// </summary>
        protected abstract void RegisterAvailableMessages();

        private BasePlugin RegisterMessage<TMessage>(PipelineStage stage)
            where TMessage : OrganizationRequest, new()
        {
            var temp = new TMessage();
            return this.RegisterMessage(stage, temp.RequestName);
        }

        private BasePlugin RegisterMessage(PipelineStage stage, string messageName)
        {
            this.availableMessages.Add(stage + messageName);
            return this;
        }

        public BasePlugin RegisterMessagePost(string messageName)
        {
            return this.RegisterMessage(PipelineStage.PostOperation, messageName);
        }

        public BasePlugin RegisterMessagePost<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            return this.RegisterMessage<TMessage>(PipelineStage.PostOperation);
        }

        public BasePlugin RegisterMessagePre(string messageName)
        {
            return this.RegisterMessage(PipelineStage.PreOperation, messageName);
        }

        public BasePlugin RegisterMessagePre<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            return this.RegisterMessage<TMessage>(PipelineStage.PreOperation);
        }

        public BasePlugin RegisterMessagePreValidation<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            return this.RegisterMessage<TMessage>(PipelineStage.PreValidation);
        }

        public BasePlugin RegisterMessagePreValidation(string messageName)
        {
            return this.RegisterMessage(PipelineStage.PreValidation, messageName);
        }


        public BasePlugin(string unsecureConfig, string secureConfig)
        {
            this.availableMessages = new HashSet<string>();
            this.unsecureConfig = unsecureConfig;
            this.secureConfig = secureConfig;
        }

        public void Execute(IServiceProvider serviceProvider)
        {
            this.RegisterAvailableMessages();
            var context = this.GetPluginExecutionContext(serviceProvider);
            if (!availableMessages.Contains((PipelineStage)context.Stage + context.MessageName))
            {
                throw new InvalidPluginExecutionException($"Plugin registered on bad message. Contact your system administrator");
            }

            var localContext = this.GetLocalPluginContext(serviceProvider);
            var repositoryFactory = this.GetRepositoryFactory(serviceProvider);
            var tracingService = this.GetTracingService(serviceProvider);

            try
            {
                if(this.CanExecute(localContext, repositoryFactory, tracingService))
                {
                    this.Execute(localContext, repositoryFactory, tracingService);
                }               
            }
            catch (Exception ex)
            {
                tracingService.Trace(ex);
                tracingService.Trace($"Context: {localContext.Context.InputParameters.ToJSON()}");
                throw;
            }
        }

        protected IPluginExecutionContext GetPluginExecutionContext(IServiceProvider serviceProvider)
        {
            return (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
        }

        public abstract bool CanExecute(ILocalPluginExecutionContext localContext, IRepositoryFactory repoFactory, ITracingService service);

        public abstract void Execute(ILocalPluginExecutionContext localContext, IRepositoryFactory repoFactory, ITracingService service);

        private IRepositoryFactory GetRepositoryFactory(IServiceProvider serviceProvider)
        {
            var factory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            return new RepositoryFactory(factory);
        }

        private ILocalPluginExecutionContext GetLocalPluginContext(IServiceProvider serviceProvider)
        {
            var context = this.GetPluginExecutionContext(serviceProvider);
            return new LocalPluginExecutionContext(context);
        }

        private ITracingService GetTracingService(IServiceProvider serviceProvider)
        {
            return (ITracingService)serviceProvider.GetService(typeof(ITracingService));
        }
    }
}