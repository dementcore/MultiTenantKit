using DementCore.MultiTenantKit.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services.Default
{
    public class DefaultTenantMapper : ITenantMapper
    {
        private IConfiguration Configuration { get; }

        public DefaultTenantMapper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Task<string> MapTenantFromSlug(string slug)
        {
            IConfigurationSection cs = Configuration.GetSection("Tenants:TenantsSlugs");
            List<TenantSlug> tenantSlugs = new List<TenantSlug>();

            cs.Bind(tenantSlugs);

            TenantSlug tenantSlug = tenantSlugs.Find(ts => ts.Slug == slug);

            return Task.FromResult(tenantSlug.TenantId);
        }
    }
}
