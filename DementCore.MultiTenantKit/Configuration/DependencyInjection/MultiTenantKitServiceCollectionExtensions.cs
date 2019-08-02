using DementCore.MultiTenantKit.Configuration.DependencyInjection;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
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
        public static IMultiTenantKitBuilder AddMultiTenantKit(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            return new MultiTenantKitBuilder(services);
        }
    }
}
