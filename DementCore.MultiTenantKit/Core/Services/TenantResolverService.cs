using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Services
{
    internal class TenantResolverService : ITenantResolverService
    {
        //https://blog.markvincze.com/matching-route-templates-manually-in-asp-net-core/

        //https://gunnarpeipman.com/net/ef-core-global-query-filters/

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

            if (rData != null && rData.Values != null && rData.Values.ContainsKey("tenant"))
            {
                tenantRouteFragment = rData.Values.GetValueOrDefault("tenant").ToString();
            }

            return tenantRouteFragment;
        }
    }
}
