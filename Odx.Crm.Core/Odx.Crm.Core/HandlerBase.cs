using System;
using Microsoft.Xrm.Sdk;
using Odx.Xrm.Core.DataAccess;

namespace Odx.Xrm.Core
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