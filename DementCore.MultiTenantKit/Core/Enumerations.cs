using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
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
}
