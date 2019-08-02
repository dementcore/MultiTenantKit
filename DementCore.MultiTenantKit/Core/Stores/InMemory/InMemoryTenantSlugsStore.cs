using DementCore.MultiTenantKit.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Stores.Default
{
    class InMemoryTenantSlugsStore<TTenantSlug> : ITenantSlugsStore<TTenantSlug> where TTenantSlug : ITenantSlugs
    {
        private List<TTenantSlug> TenantSlugs { get; }

        public InMemoryTenantSlugsStore(List<TTenantSlug> tenantSlugs)
        {
            TenantSlugs = tenantSlugs;
        }

        public List<TTenantSlug> GetTenantSlugs()
        {
            return TenantSlugs;
        }

        public TTenantSlug GetTenantSlugsBySlug(string slug)
        {
           
            List<TTenantSlug> tenantSlugs = GetTenantSlugs();

            TTenantSlug tenantSlug = (from tenant in tenantSlugs where tenant.Slugs.Contains(slug) select tenant).FirstOrDefault();

            //TTenantSlug tenantSlug = tenantSlugs.Find(ts => ts.Slugs.Find(s => s == slug) == slug);

            return tenantSlug;
        }
    }
}
