using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Enumerations
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
}
