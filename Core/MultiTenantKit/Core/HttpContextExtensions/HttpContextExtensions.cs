using MultiTenantKit.Core.Context;
using MultiTenantKit.Core.Models;
using System;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Gets the tenant's resolution context
        /// </summary>
        /// <typeparam name="TTenant">Tenant's type</typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TenantContext<TTenant> GetTenantContext<TTenant>(this HttpContext httpContext)
            where TTenant : ITenant
        {
            return (TenantContext<TTenant>)httpContext.Items["TenantContext"];
        }

        /// <summary>
        /// Gets the tenant's resolution context
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TenantContext<ITenant> GetTenantContext(this HttpContext httpContext)
        {
            var ctxType = typeof(TenantContext<>).MakeGenericType(typeof(ITenant));

            return (TenantContext<ITenant>)Convert.ChangeType(httpContext.Items["TenantContext"], ctxType);
        }

        internal static void SetTenantContext<TTenant>(this HttpContext httpContext, TenantContext<TTenant> tenantContext)
            where TTenant : ITenant
        {
            httpContext.Items.Add("TenantContext", tenantContext);
        }
    }
}
