using MultiTenantKit.Configuration.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiTenantKit.Core.Services
{
    public class TenantPathResolverService : ITenantResolverService
    {
        private PathResolverOptions Options { get; }

        public TenantPathResolverService(IOptionsMonitor<PathResolverOptions> options)
        {
            Options = options.CurrentValue;

            if (Options.ExcludedRouteTemplates == null)
            {
                Options.ExcludedRouteTemplates = new List<string>();
            }
        }

        public Task<TenantResolveResult> ResolveTenantAsync(HttpContext httpContext)
        {
            string tenantInfo = "";

            foreach (string ruta in Options.ExcludedRouteTemplates)
            {
                if (MatchRoute(ruta, httpContext.Request.Path))
                {
                    //if the request route is in the exclusion list, the resolution does not apply.
                    return Task.FromResult(TenantResolveResult.NotApply);
                }
            }

            tenantInfo = ExtractInfoFromRoute(httpContext);

            if (string.IsNullOrWhiteSpace(tenantInfo))
            {
                //route contains the configured route segment name but the extracted tenant is empty or null
                return Task.FromResult(TenantResolveResult.NotFound);
            }

            //route contains the configured route segment name and the extracted tenant contains something
            return Task.FromResult(new TenantResolveResult(tenantInfo, Options.ResolutionType));
        }

        public static bool MatchRoute(string routeTemplate, string requestPath)
        {
            RouteTemplate template = TemplateParser.Parse(routeTemplate);
            TemplateMatcher matcher = new TemplateMatcher(template, GetRouteDefaults(template));
            RouteValueDictionary values = new RouteValueDictionary();
            return matcher.TryMatch(requestPath, values);
        }

        private static RouteValueDictionary GetRouteDefaults(RouteTemplate parsedTemplate)
        {
            RouteValueDictionary result = new RouteValueDictionary();

            foreach (TemplatePart parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Extracts name from path
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="tenantRouteFragment"></param>
        /// <returns>True if the route contains the Options.RouteSegmentName. False if the route not contains Options.RouteSegmentName</returns>
        private string ExtractInfoFromRoute(HttpContext httpContext)
        {
            string tenantRouteFragment = "";

            RouteData rData = httpContext.GetRouteData();

            if (rData != null && rData.Values != null && rData.Values.ContainsKey(Options.RouteSegmentName))
            {
                tenantRouteFragment = rData.Values.GetValueOrDefault(Options.RouteSegmentName).ToString();
            }

            return tenantRouteFragment;
        }
    }
}
