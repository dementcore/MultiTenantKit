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

        public Task<TenantInfoResult<TTenant>> GetTenantInfoAsync(string tenantId)
        {
            TenantInfoResult<TTenant> infoResult;

            TTenant tenant = TenantStore.GetTenantByTenantId(tenantId);

            if (tenant == null)
            {
                infoResult = TenantInfoResult<TTenant>.NotFound;
            }
            else
            {
                infoResult = new TenantInfoResult<TTenant>(tenant);
            }

            return Task.FromResult(infoResult);

        }
    }
}
