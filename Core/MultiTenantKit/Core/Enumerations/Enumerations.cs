using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core.Enumerations
{
    public enum ResolutionType
    {
        TenantId,
        TenantName
    }

    public enum ResolutionResult
    {
        Success,
        NotApply,
        NotFound
    }

    public enum MappingResult
    {
        Success,
        NotFound
    }

    public enum InfoResult
    {
        Success,
        NotFound
    }
}
