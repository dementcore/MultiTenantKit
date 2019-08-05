using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Stores
{
    public interface ITenantMappingStore<TTenantMapping> where TTenantMapping : ITenantMapping
    {
        List<TTenantMapping> GetTenantMappings();

        TTenantMapping GetTenantMappingByName(string tenantName);
    }
}
