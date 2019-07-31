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
        public static TTenant GetTenant<TTenant>(this HttpContext httpContext)
            where TTenant : ITenant
        {
            return (TTenant)httpContext.Items["Tenant"];
        }

        internal static void SetTenant<TTenant>(this HttpContext httpContext, TTenant tenant)
            where TTenant : ITenant
        {
            httpContext.Items.Add("Tenant", tenant);
        }
    }
}
