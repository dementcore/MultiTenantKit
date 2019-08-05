using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
{
    public enum ResolvedType
    {
        NotApply,
        NotFound,
        TenantSlug,
        TenantId,
        Error
    }
}
