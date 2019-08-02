using DementCore.MultiTenantKit.Configuration.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
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

        public Task<string> ResolveTenantAsync(HttpContext httpContext)
        {
            string tenantSlug = "";

            tenantSlug = ExtractSlugFromRoute(httpContext);

            return Task.FromResult(tenantSlug);
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
