using System;
using Microsoft.Xrm.Sdk;

namespace Odx.Xrm.Core
{
    public abstract class TraceableObject : ITraceableObject
    {
        protected ITracingService tracingService;

        public void Trace(string format, params object[] args)
        {
            this.tracingService.Trace(format, args);
        }

        public void Trace(Exception ex)
        {
            if (ex == null) return;
            this.Trace($"Exception: {ex?.Message}");
            this.Trace($"StackTrace: {ex?.StackTrace}");
            this.Trace("InnerException: \n");
            this.Trace(ex?.InnerException);
        }

        internal void InitializeTracing(ITracingService service)
        {
            this.tracingService = service;
        }
    }
}
