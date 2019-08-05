using DementCore.MultiTenantKit.Core.Context;
using DementCore.MultiTenantKit.Core.Models;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Obtiene el contexto de resolucion de Inquilino
        /// </summary>
        /// <typeparam name="TTenant">tipo que define el inquilino</typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TenantContext<TTenant> GetTenantContext<TTenant>(this HttpContext httpContext)
            where TTenant : ITenant
        {
            return (TenantContext<TTenant>)httpContext.Items["TenantContext"];
        }

        public static string GetTenantName<TTenant>(this HttpContext httpContext)
            where TTenant : ITenant
        {
            var tCtx = (TenantContext<TTenant>)httpContext.Items["TenantContext"];

            return tCtx?.CurrentTenantName ?? "";
        }

        internal static void SetTenantContext<TTenant>(this HttpContext httpContext, TenantContext<TTenant> tenantContext)
            where TTenant : ITenant
        {
            httpContext.Items.Add("TenantContext", tenantContext);
        }
    }
}
