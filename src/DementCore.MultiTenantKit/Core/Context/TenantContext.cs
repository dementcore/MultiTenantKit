using DementCore.MultiTenantKit.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Context
{
    public class TenantContext<TTenant>
    {
        public TTenant Tenant { get; }

        public TenantContext(TTenant tenant)
        {
            Tenant = tenant;
        }
    }
}
