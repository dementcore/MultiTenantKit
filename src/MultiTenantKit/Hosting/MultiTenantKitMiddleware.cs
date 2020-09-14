using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantKit.Core;
using MultiTenantKit.Core.Context;
using MultiTenantKit.Core.Enumerations;
using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Services;
using MultiTenantKit.Hosting.Events;
using System;
using System.Threading.Tasks;

namespace MultiTenantKit.Hosting
{
    /// <summary>
    /// This middleware needs the three services: Resolver, Mapper,Info
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public class MultiTenantKitMiddleware<TTenant>
        where TTenant : ITenant
    {
        private MultiTenantKitMiddlewareEvents<TTenant> MiddlewareEvents { get; }
        private RequestDelegate _next { get; }

        private HttpContext HttpContext { get; set; }

        public MultiTenantKitMiddleware(RequestDelegate next, IOptionsMonitor<MultiTenantKitMiddlewareEvents<TTenant>> middlewareEvents)
        {
            _next = next;
            MiddlewareEvents = middlewareEvents.CurrentValue;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            HttpContext = httpContext;

            string tenantResolvedData = await ResolveTenant();

            TenantContext<TTenant> tenantContext = new TenantContext<TTenant>();
            if (string.IsNullOrWhiteSpace(tenantResolvedData))
            {
                await _next(httpContext);
                return;
            }

            tenantContext.TenantUrlSlug = tenantResolvedData;

            TTenant tenant = await InstanceTenant(tenantResolvedData);

            if (tenant.Equals(default(TTenant)))
            {
                await _next(httpContext);
                return;
            }

            tenantContext.Tenant = tenant;

            httpContext.SetTenantContext(tenantContext);

            await _next(httpContext);
        }

        private async Task<string> ResolveTenant()
        {
            string tenantResolvedData = "";

            try
            {
                ITenantResolverService tenantResolverService = HttpContext.RequestServices.GetRequiredService<ITenantResolverService>();
                TenantResolveResult tenantResolveResult = await tenantResolverService.ResolveTenantAsync(HttpContext);

                switch (tenantResolveResult.ResolutionResult)
                {
                    case ResolutionResult.Success:
                        tenantResolvedData = tenantResolveResult.Value;

                        MiddlewareEvents.TenantResolutionSuccessEvent(HttpContext.Response,tenantResolvedData);
                        break;

                    case ResolutionResult.NotApply:
                        MiddlewareEvents.TenantResolutionNotApplyEvent(HttpContext.Response);
                        break;

                    case ResolutionResult.NotFound:
                        MiddlewareEvents.TenantResolutionNotFoundEvent(HttpContext.Response);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid tenant resolution result");
                }
            }
            catch (MultiTenantKitException exx)
            {
                throw exx;
            }
            catch (Exception ex)
            {
                MiddlewareEvents.TenantResolutionErrorEvent(HttpContext.Response, ex);
            }

            return tenantResolvedData;
        }

        private async Task<TTenant> InstanceTenant(string tenantId)
        {
            TTenant result = default;

            try
            {
                ITenantInfoService<TTenant> tenantInfoService = HttpContext.RequestServices.GetRequiredService<ITenantInfoService<TTenant>>();
                TenantInfoResult<TTenant> tenantInfoResult = await tenantInfoService.GetTenantInfoAsync(tenantId);

                switch (tenantInfoResult.InfoResult)
                {
                    case InfoResult.Success:
                        result = tenantInfoResult.Value;
                        MiddlewareEvents.TenantInfoSuccessEvent(HttpContext.Response,tenantInfoResult);
                        break;

                    case InfoResult.NotFound:
                        MiddlewareEvents.TenantInfoNotFoundEvent(HttpContext.Response);
                        break;

                    default:
                        throw new InvalidOperationException("Invalid tenant info result");
                }
            }
            catch (MultiTenantKitException exx)
            {
                throw exx;
            }
            catch (Exception ex)
            {
                MiddlewareEvents.TenantInfoErrorEvent(HttpContext.Response, ex);
            }

            return result;
        }
    }
}
