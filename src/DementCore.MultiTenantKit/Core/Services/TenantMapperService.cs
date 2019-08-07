using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantMapperService<TTenantMappings> : ITenantMapperService<TTenantMappings> 
        where TTenantMappings : ITenantMapping
    {
        private ITenantMappingStore<TTenantMappings> NamesStore { get; }

        public TenantMapperService(ITenantMappingStore<TTenantMappings> namesStore)
        {
            NamesStore = namesStore;
        }

        public Task<TenantMapResult<TTenantMappings>> MapTenantAsync(string tenantName)
        {
            TenantMapResult<TTenantMappings> mapResult;

            var tMap = NamesStore.GetTenantMappingByName(tenantName);

            if (tMap == null)
            {
                mapResult = TenantMapResult<TTenantMappings>.NotFound;
            }
            else
            {
                mapResult = new TenantMapResult<TTenantMappings>(tMap);
            }

            return Task.FromResult(mapResult);
        }
    }
}
