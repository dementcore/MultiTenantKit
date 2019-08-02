using DementCore.MultiTenantKit.Core.Context;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Hosting
{
    /// <summary>
    /// This middleware needs the three services: Resolver, Mapper,Info
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public class MultiTenantKitFullMiddleware<TTenant> where TTenant : ITenant
    {
        private readonly RequestDelegate _next;

        public MultiTenantKitFullMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITenantResolverService tenantResolverService, ITenantMapperService tenantMapperService, ITenantInfoService<TTenant> tenantInfoService)
        {
            string tenantSlug = "";
            string tenantId = "";
            TTenant tenant = default;

            tenantSlug = await tenantResolverService.ResolveTenantAsync(httpContext);

            if (!string.IsNullOrWhiteSpace(tenantSlug))
            {
                tenantId = await tenantMapperService.MapTenantAsync(tenantSlug);
            }

            if (!string.IsNullOrWhiteSpace(tenantId))
            {
                tenant = await tenantInfoService.GetTenantInfoAsync(tenantId);
            }

            TenantContext<TTenant> tenantContext = new TenantContext<TTenant>(tenant, tenantSlug);

            httpContext.SetTenantContext(tenantContext);

            await _next(httpContext);
        }
    }
}
