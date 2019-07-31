using DementCore.MultiTenantKit.Core;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using DementCore.MultiTenantKit.Core.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Hosting
{
    public class MultiTenantKitMiddleware<TTenant> where TTenant : ITenant
    {
        private readonly RequestDelegate _next;
        private readonly MultiTenantKitMiddlewareOptions _tenantResolverOptions;

        public MultiTenantKitMiddleware(RequestDelegate next, IOptionsMonitor<MultiTenantKitMiddlewareOptions> tenantResolverOptions)
        {
            _next = next;
            _tenantResolverOptions = tenantResolverOptions.CurrentValue;
        }

        public async Task Invoke(HttpContext httpContext, ITenantResolver tenantResolverService, ITenantStore<TTenant> tenantStoreService)
        {
            string tenantSlug = await tenantResolverService.ResolveTenantAsync(httpContext);

            TTenant tenant = await tenantStoreService.GetTenantInfo(tenantSlug);

            if (_tenantResolverOptions.IncludeInUserClaims)
            {
                AddTenantDataInUserClaims(tenant, httpContext);
            }

            if (_tenantResolverOptions.IncludeInHttpContext)
            {
                httpContext.SetTenant(tenant);
            }

            await _next(httpContext);
        }

        /// <summary>
        /// Agrega los valores de las propiedades a los claims del usuario
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="valorPropiedad"></param>
        /// <param name="nombrePropiedad"></param>
        private void AddTenantDataInUserClaims(TTenant tenant, HttpContext httpContext)
        {
            if (tenant == null)
            {
                return;
            }

            if (_tenantResolverOptions.IncludeOnlyIfAuthenticated)
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    return;
                }
            }

            System.Reflection.PropertyInfo[] properties = tenant.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo property in properties)
            {

                object valorPropiedad = property.GetValue(tenant);
                string nombrePropiedad = property.Name;

                if (!string.IsNullOrWhiteSpace(nombrePropiedad) && valorPropiedad != null)
                {
                    string claimType = $"{_tenantResolverOptions.ClaimsPrefix}{nombrePropiedad}";

                    ClaimsIdentity claimsIdentity = ((ClaimsIdentity)httpContext.User.Identity);

                    //incluyo los claims en la identidad del usuario
                    if (claimsIdentity.HasClaim(c => c.Type == claimType))
                    {
                        Claim claim = claimsIdentity.FindFirst(claimType);

                        if (claim != null)
                        {
                            claimsIdentity.RemoveClaim(claim);
                        }
                    }

                    claimsIdentity.AddClaim(new Claim(claimType, valorPropiedad.ToString()));
                }
            }
        }
    }
}
