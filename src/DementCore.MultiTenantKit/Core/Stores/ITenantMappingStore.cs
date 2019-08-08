using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Stores
{
    public interface ITenantMappingStore<out TTenantMapping> where TTenantMapping : ITenantMapping
    {
     
        TTenantMapping GetTenantMappingByName(string tenantName);
    }
}
