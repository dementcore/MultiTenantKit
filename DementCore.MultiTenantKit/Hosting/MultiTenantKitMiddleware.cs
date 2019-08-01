using DementCore.MultiTenantKit.Core;
using DementCore.MultiTenantKit.Core.Context;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Hosting
{
    public class MultiTenantKitMiddleware<TTenant> where TTenant : ITenant
    {
        private readonly RequestDelegate _next;

        public MultiTenantKitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITenantResolverService tenantResolverService, ITenantMapperService tenantMapperService, ITenantStore<TTenant> tenantStoreService)
        {
            string tenantSlug = "";
            string tenantId = "";
            TTenant tenant = default;

            tenantSlug = await tenantResolverService.ResolveTenantSlugAsync(httpContext);

            if (!string.IsNullOrWhiteSpace(tenantSlug))
            {
                tenantId = await tenantMapperService.MapTenantFromSlugAsync(tenantSlug);
            }

            if (!string.IsNullOrWhiteSpace(tenantId))
            {
                tenant = await tenantStoreService.GetTenantInfo(tenantId);
            }

            TenantContext<TTenant> tenantContext = new TenantContext<TTenant>(tenant, tenantSlug);

            httpContext.SetTenantContext(tenantContext);

            await _next(httpContext);
        }
    }
}
