using MultiTenantKit.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiTenantKit.Core.Stores.InMemory
{
    public class InMemoryTenantMappingStore<TTenantMapping> : ITenantMappingStore<TTenantMapping> where TTenantMapping : ITenantMapping
    {
        private List<TTenantMapping> TenantMappings { get; }

        public InMemoryTenantMappingStore(List<TTenantMapping> tenantMappings)
        {
            TenantMappings = tenantMappings;
        }

       
        public TTenantMapping GetTenantMappingByName(string tenantName)
        {
            
            TTenantMapping _tenantName = (from tenant in TenantMappings where tenant.Names.Contains(tenantName) select tenant).FirstOrDefault();

            return _tenantName;
        }
    }
}
