using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core.Stores
{
    public interface ITenantSlugsStore<TTenantSlugs> where TTenantSlugs : ITenantSlugs
    {
        List<TTenantSlugs> GetTenantSlugs();

        TTenantSlugs GetTenantSlugsBySlug(string slug);
    }
}
