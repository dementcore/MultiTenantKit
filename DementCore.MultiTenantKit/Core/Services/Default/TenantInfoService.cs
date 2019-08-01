using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services.Default
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
