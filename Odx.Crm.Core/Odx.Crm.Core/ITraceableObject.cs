using System;

namespace Odx.Xrm.Core
{
    public interface ITraceableObject
    {
        void Trace(Exception ex);
        void Trace(string format, params object[] args);
    }
}