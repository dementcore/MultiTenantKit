using MultiTenantKit.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public string TenantUrlSlug { get; protected internal set; }

        public TTenant Tenant { get; protected internal set; }
    }
}
