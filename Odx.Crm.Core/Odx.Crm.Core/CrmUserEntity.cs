using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Odx.Xrm.Core
{
    public class CrmUserEntity : CrmEntity
    {
        [AttributeLogicalName("ownerid")]
        public EntityReference Owner
        {
            get => GetAttribute<EntityReference>();
            set => SetAttribute(value);
        }
    }
}
