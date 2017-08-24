using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Odx.Xrm.Core.DataAccess
{
    public class RequestAddedEventArgs : EventArgs
    {
        public OrganizationRequest Request { get; private set; }

        public RequestAddedEventArgs(OrganizationRequest request)
        {
            this.Request = request;
        }
    }
}
