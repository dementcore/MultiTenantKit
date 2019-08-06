using DementCore.MultiTenantKit.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public TTenant Tenant { get; }

        public ResolutionType ResolutionType { get; }

        public ResolutionResult ResolutionResult { get; }

        public MappingResult MappingResult { get; }

        public TenantContext(TTenant tenant, ResolutionResult resolutionResult, MappingResult mappingResult, ResolutionType resolvedType)
        {
            Tenant = tenant;
            ResolutionResult = resolutionResult;
            MappingResult = mappingResult;
            ResolutionType = resolvedType;
        }
    }
}
