using System;
using Microsoft.Xrm.Sdk;
using Odx.Crm.Core.DataAccess;

namespace Odx.Crm.Core
{
    public abstract class HandlerBase : TraceableObject
    {
        protected string UnsecureConfig { get; private set; }
        protected string SecureConfig { get; private set; }

        internal void InitializeConfiguration(string unsecureConfig, string secureConfig)
        {
            this.UnsecureConfig = unsecureConfig;
            this.SecureConfig = secureConfig;
        }
    }
}