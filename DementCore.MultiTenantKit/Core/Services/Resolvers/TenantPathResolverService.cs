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
            string tenantSlug = "";

            try
            {
                if (!ExtractSlugFromRoute(httpContext, out tenantSlug))
                {
                    //not apply tenant resolution
                    return Task.FromResult(TenantResolveResult.NotApply);
                }

                if (!string.IsNullOrWhiteSpace(tenantSlug))
                {
                    return Task.FromResult(new TenantResolveResult(tenantSlug, ResolutionType.TenantName));
                }
                else
                {
                    //slug not found or empty
                    return Task.FromResult(TenantResolveResult.NotFound);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(new TenantResolveResult(ex));
            }

        }

        /// <summary>
        /// Extracts slug from url
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="tenantRouteFragment"></param>
        /// <returns>True if the route contains the Options.RouteSegmentName. False if the route not contains Options.RouteSegmentName</returns>
        private bool ExtractSlugFromRoute(HttpContext httpContext, out string tenantRouteFragment)
        {
            bool returnValue = false;

            tenantRouteFragment = string.Empty;

            RouteData rData = httpContext.GetRouteData();

            if (rData != null && rData.Values != null && rData.Values.ContainsKey(Options.RouteSegmentName))
            {
                tenantRouteFragment = rData.Values.GetValueOrDefault(Options.RouteSegmentName).ToString();
                returnValue = true;
            }

            return returnValue;
        }
    }
}
