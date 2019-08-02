using DementCore.MultiTenantKit.Core;
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
    public class MultiTenantKitMiddleware<TTenant> where TTenant : ITenant
    {
        private readonly RequestDelegate _next;

        public MultiTenantKitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITenantResolverService tenantResolverService, ITenantMapperService tenantMapperService, ITenantInfoService<TTenant> tenantInfoService)
        {
            TenantResolveResult tenantResolveResult = null;

            string tenantId = "";
            TTenant tenant = default;
            string tenantSlug = "";

            tenantResolveResult = await tenantResolverService.ResolveTenantAsync(httpContext);

            if (tenantResolveResult.Success)
            {

                switch (tenantResolveResult.ResolvedType)
                {
                    case ResolvedType.TenantId:

                        tenantId = tenantResolveResult.Value;

                        //call the info service directly with resolved TenantId
                        if (!string.IsNullOrWhiteSpace(tenantId))
                        {
                            tenant = await tenantInfoService.GetTenantInfoAsync(tenantId);
                        }

                        break;

                    case ResolvedType.TenantSlug:

                        //only call the mapper service if the resolution is of type tenantSlug
                        if (!string.IsNullOrWhiteSpace(tenantResolveResult.Value))
                        {
                            tenantId = await tenantMapperService.MapTenantAsync(tenantResolveResult.Value);

                            tenantSlug = tenantResolveResult.Value;
                        }

                        //call the info service after mapping the tenant slug
                        if (!string.IsNullOrWhiteSpace(tenantId))
                        {
                            tenant = await tenantInfoService.GetTenantInfoAsync(tenantId);
                        }

                        break;
                }

                TenantContext<TTenant> tenantContext = new TenantContext<TTenant>(tenant, tenantSlug);

                httpContext.SetTenantContext(tenantContext);
            }

            await _next(httpContext);
        }
    }
}
