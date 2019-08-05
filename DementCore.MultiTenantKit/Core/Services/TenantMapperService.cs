using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantMapperService<TTenantNames> : ITenantMapperService where TTenantNames : ITenantMapping
    {
        private ITenantMappingStore<TTenantNames> NamesStore { get; }

        public TenantMapperService(ITenantMappingStore<TTenantNames> namesStore)
        {
            NamesStore = namesStore;
        }

        public Task<string> MapTenantAsync(string tenantName)
        {
            var tName = NamesStore.GetTenantMappingByName(tenantName);

            if (tName != null)
            {
                return Task.FromResult(tName?.TenantId);
            }

            return Task.FromResult("");
        }
    }
}
