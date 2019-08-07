using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Enumerations
{
    public enum ResolutionType
    {
        Nothing,
        TenantId,
        TenantName
    }

    public enum ResolutionResult
    {
        Success,
        NotApply,
        NotFound,
        Error
    }

    public enum MappingResult
    {
        Success,
        NotApply,
        NotFound,
        Error
    }
}
