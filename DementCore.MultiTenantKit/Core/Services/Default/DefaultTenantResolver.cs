using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace DementCore.MultiTenantKit.Core.Services.Default
{
    public class DefaultTenantResolver : ITenantResolver
    {
        //https://blog.markvincze.com/matching-route-templates-manually-in-asp-net-core/

        //https://gunnarpeipman.com/net/ef-core-global-query-filters/

        public Task<string> ResolveTenantAsync(HttpContext httpRequest)
        {
            string tenantSlug = "";

            var rData = httpRequest.GetRouteData();

            if (rData != null && rData.Values != null && rData.Values.ContainsKey("tenant"))
            {
                tenantSlug = rData.Values.GetValueOrDefault("tenant").ToString();
            }

            return Task.FromResult(tenantSlug);
        }
    }
}
