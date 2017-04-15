using System;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.Core
{
    public abstract class HandlerBase : TraceableObject
    {
        protected LocalPluginExecutionContext ExecutionData { get; private set; }

        protected IRepositoryFactory Repositories { get; private set; }

        protected string UnsecureConfig { get; private set; }
        protected string SecureConfig { get; private set; }

        public virtual void Initialize(IServiceProvider serviceProvider, string unsecureConfig, string secureConfig)
        {
            this.tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            this.Repositories = new RepositoryFactory(factory);
            this.ExecutionData = new LocalPluginExecutionContext(context);
            this.UnsecureConfig = unsecureConfig;
            this.SecureConfig = secureConfig;
        }
    }

    public class HandlerBase<T> : HandlerBase
        where T : Entity, new()
    {
        protected new LocalPluginExecutionContext<T> ExecutionData { get; private set; }

        public override void Initialize(IServiceProvider serviceProvider, string unsecureConfig, string secureConfig)
        {
            base.Initialize(serviceProvider, unsecureConfig, secureConfig);
            this.ExecutionData = new LocalPluginExecutionContext<T>(base.ExecutionData.Context);
        }
    }
}