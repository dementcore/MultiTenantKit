using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Configuration.Options
{
    internal class TenantMiddlewareOptions
    {
        public Type TenantType { get; internal set; }

        public Type TenantMappingType { get; internal set; }
    }
}
