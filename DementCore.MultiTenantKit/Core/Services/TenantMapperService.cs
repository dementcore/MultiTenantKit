using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantMapperService<TTenantSlug> : ITenantMapperService where TTenantSlug : ITenantSlugs
    {
        private ITenantSlugsStore<TTenantSlug> SlugsStore { get; }

        public TenantMapperService(ITenantSlugsStore<TTenantSlug> slugsStore)
        {
            SlugsStore = slugsStore;
        }

        public Task<string> MapTenantAsync(string slug)
        {
            var tSlug = SlugsStore.GetTenantSlugsBySlug(slug);

            if (tSlug != null)
            {
                return Task.FromResult(tSlug?.TenantId);
            }

            return Task.FromResult("");
        }
    }
}
