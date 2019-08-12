using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantKit.Core.Services
{
    public class TenantInfoService<TTenant> : ITenantInfoService<TTenant> where TTenant : ITenant
    {
        ITenantStore<TTenant> TenantStore { get; }

        public TenantInfoService(ITenantStore<TTenant> tenantStore)
        {
            TenantStore = tenantStore;
        }

        public Task<TTenant> GetTenantInfoAsync(string tenantId)
        {
            return Task.FromResult(TenantStore.GetTenantByTenantId(tenantId));
        }
    }
}
