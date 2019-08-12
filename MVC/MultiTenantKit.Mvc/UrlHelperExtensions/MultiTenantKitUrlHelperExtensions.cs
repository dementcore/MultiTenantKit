using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Core;
using MultiTenantKit.Core.Context;
using MultiTenantKit.Core.Models;
using System;

namespace Microsoft.AspNetCore.Mvc
{
    public static class MultiTenantKitUrlHelperExtensions
    {
        /// <summary>
        /// Generates routes using configured PathResolverOptions.RouteSegmentName to add tenant url slug to generated route.
        /// </summary>
        /// <typeparam name="TTenant">Type representing tenant</typeparam>
        /// <param name="actionName">Name of the action</param>
        /// <param name="controller">Name of the controller</param>
        /// <param name="routeData">Route values</param>
        /// <returns></returns>
        public static string ActionWithTenant<TTenant>(this IUrlHelper urlHelper, string actionName, string controller, object routeData)
            where TTenant : ITenant
        {
            HttpContext httpContext = urlHelper.ActionContext.HttpContext;

            TenantContext<TTenant> tCtx = httpContext.GetTenantContext<TTenant>();

            if (tCtx == null)
            {
                return string.Empty;
            }

            IOptionsMonitor<PathResolverOptions> options = httpContext.RequestServices.GetService(typeof(IOptionsMonitor<PathResolverOptions>)) as IOptionsMonitor<PathResolverOptions>;

            if (options == null)
            {
                throw new MultiTenantKitException("Only use this extension method if you are using route resolving!!!");
            }

            string tenantSlug = options.CurrentValue.RouteSegmentName;

            RouteValueDictionary routeValues = null;

            if (routeData != null)
            {
                routeValues = new RouteValueDictionary(routeData);
            }
            else
            {
                routeValues = new RouteValueDictionary();
            }

            routeValues.Add(tenantSlug, tCtx.TenantUrlSlug);

            return urlHelper.Action(actionName, controller, routeValues);

        }

        /// <summary>
        /// Generates routes using configured PathResolverOptions.RouteSegmentName to add tenant url slug to generated route.
        /// </summary>
        /// <typeparam name="TTenant"></typeparam>
        /// <param name="actionName">Name of the action</param>        
        /// <returns></returns>
        public static string ActionWithTenant<TTenant>(this IUrlHelper urlHelper, string actionName)
            where TTenant : ITenant
        {
            string controller = urlHelper.ActionContext.RouteData.Values["controller"].ToString();

            return ActionWithTenant<TTenant>(urlHelper, actionName, controller, null);
        }

        /// <summary>
        /// Generates routes using configured PathResolverOptions.RouteSegmentName to add tenant url slug to generated route.
        /// </summary>
        /// <typeparam name="TTenant"></typeparam>
        /// <param name="actionName">Name of the action</param>
        /// <param name="routeData">Route values</param>
        /// <returns></returns>
        public static string ActionWithTenant<TTenant>(this IUrlHelper urlHelper, string actionName, object routeData)
          where TTenant : ITenant
        {
            string controller = urlHelper.ActionContext.RouteData.Values["controller"].ToString();

            return ActionWithTenant<TTenant>(urlHelper, actionName, controller, routeData);
        }

        /// <summary>
        /// Generates routes using configured PathResolverOptions.RouteSegmentName to add tenant url slug to generated route.
        /// </summary>
        /// <typeparam name="TTenant"></typeparam>
        /// <param name="actionName">Name of the action</param>       
        /// <param name="controller">Name of the controller</param>
        /// <returns></returns>
        public static string ActionWithTenant<TTenant>(this IUrlHelper urlHelper, string actionName, string controller)
          where TTenant : ITenant
        {
            return ActionWithTenant<TTenant>(urlHelper, actionName, controller, null);
        }
    }
}
