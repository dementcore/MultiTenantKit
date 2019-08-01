using DementCore.MultiTenantKit.Configuration.DependencyInjection;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using DementCore.MultiTenantKit.Core.Services.Default;
using DementCore.MultiTenantKit.Core.Stores;
using DementCore.MultiTenantKit.Core.Stores.Default;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MultiTenantKitServiceCollectionExtensions
    {
        /// <summary>
        /// Registra los servicios basicos para la resolucion de inquilino.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IMultiTenantKitBuilder<TTenant> AddMultiTenantKit<TTenant>(this IServiceCollection services) where TTenant : ITenant
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ITenantResolver, DefaultTenantResolver>();
            services.AddTransient<ITenantMapper, DefaultTenantMapper>();
            services.AddTransient<ITenantStore<TTenant>, DefaultTenantStore<TTenant>>();
           
            return new MultiTenantKitBuilder<TTenant>(services);
        }
    }
}
