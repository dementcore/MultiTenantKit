using DementCore.MultiTenantKit.Configuration.Options;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DementCore.MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class DefaultServices
    {

        #region PathResolver

        public static IMultiTenantKitBuilder AddDefaultTenantRouteResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.AddDefaultTenantRouteResolverService("tenant");

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantRouteResolverService(this IMultiTenantKitBuilder builder, string routeSegmentName)
        {
            builder.AddDefaultTenantRouteResolverService(options =>
            {
                options.RouteSegmentName = routeSegmentName;
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantRouteResolverService(this IMultiTenantKitBuilder builder, Action<PathResolverOptions> configureOptions)
        {

            if (!builder.Services.Any(x => x.ServiceType == typeof(IConfigureOptions<PathResolverOptions>)))
            {
                builder.Services.Configure(configureOptions);
            }

            builder.Services.TryAddTransient<ITenantResolverService, TenantPathResolverService>();

            return builder;
        }

        #endregion

        #region DomainResolver

        public static IMultiTenantKitBuilder AddDefaultTenantDomainResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.AddDefaultTenantDomainResolverService("{0}.midomain.com");

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantDomainResolverService(this IMultiTenantKitBuilder builder, string domainTemplate)
        {
            builder.AddDefaultTenantDomainResolverService(options =>
            {
                options.DomainTemplate = domainTemplate;
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantDomainResolverService(this IMultiTenantKitBuilder builder, Action<HostResolverOptions> configureOptions)
        {

            if (!builder.Services.Any(x => x.ServiceType == typeof(IConfigureOptions<HostResolverOptions>)))
            {
                builder.Services.Configure(configureOptions);
            }

            builder.Services.TryAddTransient<ITenantResolverService, TenantHostResolverService>();

            return builder;
        }

        #endregion

        #region ClaimsResolver

        public static IMultiTenantKitBuilder AddDefaultTenantClaimResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.AddDefaultTenantClaimResolverService("TenantId");

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantClaimResolverService(this IMultiTenantKitBuilder builder, string claimName)
        {
            builder.AddDefaultTenantClaimResolverService(options =>
            {
                options.ClaimName = claimName;
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantClaimResolverService(this IMultiTenantKitBuilder builder, Action<ClaimResolverOptions> configureOptions)
        {

            if (!builder.Services.Any(x => x.ServiceType == typeof(IConfigureOptions<ClaimResolverOptions>)))
            {
                builder.Services.Configure(configureOptions);
            }

            builder.Services.TryAddTransient<ITenantResolverService, TenantClaimResolverService>();

            return builder;
        }

        #endregion

        public static IMultiTenantKitBuilder AddDefaultTenantMapperService<TTenantMapping>(this IMultiTenantKitBuilder builder)
            where TTenantMapping : ITenantMapping
        {
            builder.Services.TryAddTransient<ITenantMapperService, TenantMapperService<TTenantMapping>>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantInfoService<TTenant>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
        {
            builder.Services.TryAddTransient<ITenantInfoService<TTenant>, TenantInfoService<TTenant>>();

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultTenantProviderService<TTenant>(this IMultiTenantKitBuilder builder)
            where TTenant : ITenant
        {
            builder.Services.TryAddScoped<ITenantProvider<TTenant>, TenantProviderService<TTenant>>();

            return builder;
        }

    }
}
