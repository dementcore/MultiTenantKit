using DementCore.MultiTenantKit.Configuration.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantPathResolverService : ITenantResolverService
    {
        private PathResolverOptions Options { get; }

        public TenantPathResolverService(IOptionsMonitor<PathResolverOptions> options)
        {
            Options = options.CurrentValue;
        }

        public Task<TenantResolveResult> ResolveTenantAsync(HttpContext httpContext)
        {
            TenantResolveResult resolveResult = new TenantResolveResult();

            try
            {
                string tenantSlug = "";

                tenantSlug = ExtractSlugFromRoute(httpContext);

                if (string.IsNullOrWhiteSpace(tenantSlug))
                {
                    resolveResult.Success = false;
                }
                else
                {
                    resolveResult.Success = true;
                    resolveResult.ResolvedType = ResolvedType.TenantSlug;
                    resolveResult.Value = tenantSlug;
                }
            }
            catch
            {
                resolveResult.Success = false;
                resolveResult.ErrorMessage = "Unable to resolve the tenant's slug from route";
            }

            return Task.FromResult(resolveResult);
        }

        private string ExtractSlugFromRoute(HttpContext httpContext)
        {
            string tenantRouteFragment = string.Empty;

            RouteData rData = httpContext.GetRouteData();

            if (rData != null && rData.Values != null && rData.Values.ContainsKey(Options.RouteSegmentName))
            {
                tenantRouteFragment = rData.Values.GetValueOrDefault(Options.RouteSegmentName).ToString();
            }

            return tenantRouteFragment;
        }
    }
}
