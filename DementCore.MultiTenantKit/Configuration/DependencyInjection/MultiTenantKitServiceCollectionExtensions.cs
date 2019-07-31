using DementCore.MultiTenantKit.Configuration.DependencyInjection;
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
        public static IMultiTenantKitBuilder AddTenantResolver(this IServiceCollection services)
        {
            services.AddOptions();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return new MultiTenantKitBuilder(services);
        }
    }
}
