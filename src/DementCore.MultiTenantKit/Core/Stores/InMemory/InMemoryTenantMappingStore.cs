using DementCore.MultiTenantKit.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Stores.Default
{
    class InMemoryTenantMappingStore<TTenantMapping> : ITenantMappingStore<TTenantMapping> where TTenantMapping : ITenantMapping
    {
        private List<TTenantMapping> TenantMappings { get; }

        public InMemoryTenantMappingStore(List<TTenantMapping> tenantMappings)
        {
            TenantMappings = tenantMappings;
        }

        public List<TTenantMapping> GetTenantMappings()
        {
            return TenantMappings;
        }

        public TTenantMapping GetTenantMappingByName(string tenantName)
        {
            List<TTenantMapping> tenantMappings = GetTenantMappings();

            TTenantMapping _tenantName = (from tenant in tenantMappings where tenant.Names.Contains(tenantName) select tenant).FirstOrDefault();

            return _tenantName;
        }
    }
}
