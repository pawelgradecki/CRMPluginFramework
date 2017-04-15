using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace Odx.Crm.Core
{
    public abstract class BasePlugin : IPlugin
    {
        private string unsecureConfig;
        private string secureConfig;
        private Dictionary<string, Type> availableMessages;

        private enum PipelineStage
        {
            PreValidation = 10,
            PreOperation = 20,
            PostOperation = 40
        }

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
            this.availableMessages.Add(stage + temp.RequestName, typeof(TMessage));
            return this;
        }

        public BasePlugin RegisterMessagePost<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            return this.RegisterMessage<TMessage>(PipelineStage.PostOperation);
        }

        public BasePlugin RegisterMessagePre<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            return this.RegisterMessage<TMessage>(PipelineStage.PostOperation);
        }

        public BasePlugin RegisterMessagePreValidation<TMessage>()
            where TMessage : OrganizationRequest, new()
        {
            return this.RegisterMessage<TMessage>(PipelineStage.PostOperation);
        }


        public BasePlugin(string unsecureConfig, string secureConfig)
        {
            this.availableMessages = new Dictionary<string, Type>();
            this.unsecureConfig = unsecureConfig;
            this.secureConfig = secureConfig;
        }

        public virtual void Execute(IServiceProvider serviceProvider)
        {
            this.RegisterAvailableMessages();
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (!availableMessages.ContainsKey((PipelineStage)context.Stage + context.MessageName))
            {
                throw new InvalidPluginExecutionException($"Plugin registered on bad message. Contact your system administrator");
            }
        }
    }

    public abstract class BasePlugin<T> : BasePlugin, IPlugin
        where T : HandlerBase, IHandler, new()
    {

        public BasePlugin(string unsecureConfig, string secureConfig) : base(unsecureConfig, secureConfig) { }

        public override void Execute(IServiceProvider serviceProvider)
        {
            base.Execute(serviceProvider);
            var handler = new T();
            handler.Initialize(serviceProvider, this.UnsecureConfig, this.SecureConfig);
            try
            {
                handler.Execute();
            }
            catch (Exception ex)
            {
                handler.Trace(ex);
                throw;
            }
        }
    }
}