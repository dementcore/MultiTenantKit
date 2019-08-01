using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public TTenant Tenant { get; }

        public string CurrentTenantSlug { get; }

        public TenantContext(TTenant tenant, string currentTenantSlug)
        {
            Tenant = tenant;
            CurrentTenantSlug = currentTenantSlug;
        }
    }
}
