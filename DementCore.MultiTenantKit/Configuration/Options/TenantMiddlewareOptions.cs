using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.Options
{
    internal class TenantMiddlewareOptions
    {
        public Type TenantType { get; set; }

        public Type TenantMappingType { get; set; }
    }
}
