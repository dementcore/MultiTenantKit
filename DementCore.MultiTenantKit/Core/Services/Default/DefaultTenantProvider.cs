using DementCore.MultiTenantKit.Core.Models;
using Microsoft.AspNetCore.Http;

namespace DementCore.MultiTenantKit.Core.Services.Default
{
    public class DefaultTenantProvider<TTenant> : ITenantProvider<TTenant> where TTenant : ITenant
    {
        public IHttpContextAccessor HttpContextAccessor { get; }

        public DefaultTenantProvider(IHttpContextAccessor httpContextAccessor)
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
