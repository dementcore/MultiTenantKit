using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public TTenant Tenant { get; }

        public string CurrentTenantSlug { get; }

        public ResolvedType ResolvedType { get; }

        public TenantContext(TTenant tenant, string currentTenantSlug, ResolvedType resolvedType)
        {
            Tenant = tenant;
            CurrentTenantSlug = currentTenantSlug;
            ResolvedType = resolvedType;
        }
    }
}
