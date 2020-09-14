using MultiTenantKit.Configuration.Options;
using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiTenantKit.Configuration.DependencyInjection.BuilderExtensions
{
    public static class DefaultServices
    {
        #region Default Services

        #region Resolvers

        #region PathResolver

        public static IMultiTenantKitBuilder AddDefaultRouteResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.AddDefaultRouteResolverService("tenant");

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultRouteResolverService(this IMultiTenantKitBuilder builder, string routeSegmentName)
        {
            builder.AddDefaultRouteResolverService(options =>
            {
                options.RouteSegmentName = routeSegmentName;
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultRouteResolverService(this IMultiTenantKitBuilder builder, Action<PathResolverOptions> configureOptions)
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

        public static IMultiTenantKitBuilder AddDefaultDomainResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.AddDefaultDomainResolverService("{0}.midomain.com");

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultDomainResolverService(this IMultiTenantKitBuilder builder, string domainTemplate)
        {
            builder.AddDefaultDomainResolverService(options =>
            {
                options.DomainTemplate = domainTemplate;
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultDomainResolverService(this IMultiTenantKitBuilder builder, Action<HostResolverOptions> configureOptions)
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

        public static IMultiTenantKitBuilder AddDefaultClaimResolverService(this IMultiTenantKitBuilder builder)
        {
            builder.AddDefaultClaimResolverService("TenantId");

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultClaimResolverService(this IMultiTenantKitBuilder builder, string claimName)
        {
            builder.AddDefaultClaimResolverService(options =>
            {
                options.ClaimName = claimName;
            });

            return builder;
        }

        public static IMultiTenantKitBuilder AddDefaultClaimResolverService(this IMultiTenantKitBuilder builder, Action<ClaimResolverOptions> configureOptions)
        {

            if (!builder.Services.Any(x => x.ServiceType == typeof(IConfigureOptions<ClaimResolverOptions>)))
            {
                builder.Services.Configure(configureOptions);
            }

            builder.Services.TryAddTransient<ITenantResolverService, TenantClaimResolverService>();

            return builder;
        }

        #endregion

        #endregion

        #region Info

        public static IMultiTenantKitBuilder AddDefaultTenantInfoService(this IMultiTenantKitBuilder builder)
        {

            Type ITenantInfoType = typeof(ITenantInfoService<>).MakeGenericType(builder.TenantType);
            Type STenantInfoType = typeof(TenantInfoService<>).MakeGenericType(builder.TenantType);

            builder.Services.TryAddTransient(ITenantInfoType, STenantInfoType);

            return builder;
        }

        #endregion

        #region Provider

        public static IMultiTenantKitBuilder AddDefaultTenantProviderService(this IMultiTenantKitBuilder builder)
        {
            Type ITenantProviderType = typeof(ITenantProvider<>).MakeGenericType(builder.TenantType);
            Type STenantProviderType = typeof(TenantProviderService<>).MakeGenericType(builder.TenantType);

            builder.Services.TryAddTransient(ITenantProviderType, STenantProviderType);

            return builder;
        }

        #endregion

        #endregion
    }
}
