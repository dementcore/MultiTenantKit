using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public TTenant Tenant { get; }

        public string CurrentTenantName { get; }

        public ResolutionType ResolutionType { get; }

        public ResolutionResult ResolutionResult { get; }

        public TenantContext(TTenant tenant, string currentTenantName, ResolutionResult resolutionResult, ResolutionType resolvedType = ResolutionType.Nothing)
        {
            Tenant = tenant;
            CurrentTenantName = currentTenantName;
            ResolutionResult = resolutionResult;
            ResolutionType = resolvedType;
        }
    }
}
