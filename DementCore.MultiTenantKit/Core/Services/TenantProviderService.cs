using DementCore.MultiTenantKit.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DementCore.MultiTenantKit.Core.Services
{
    public class TenantProviderService<TTenant> : ITenantProvider<TTenant> where TTenant : ITenant
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public TenantProviderService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public TTenant GetTenant()
        {
            TTenant tenant = HttpContextAccessor.HttpContext.GetTenantContext<TTenant>().Tenant;

            return tenant;
        }
    }
}
