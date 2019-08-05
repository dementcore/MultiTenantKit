using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public TTenant Tenant { get; }

        public string CurrentTenantSlug { get; }

        public ResolutionType ResolutionType { get; }

        public ResolutionResult ResolutionResult { get; }

        public TenantContext(TTenant tenant, string currentTenantSlug, ResolutionResult resolutionResult, ResolutionType resolvedType = ResolutionType.Nothing)
        {
            Tenant = tenant;
            CurrentTenantSlug = currentTenantSlug;
            ResolutionResult = resolutionResult;
            ResolutionType = resolvedType;
        }
    }
}
