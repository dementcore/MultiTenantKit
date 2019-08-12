﻿using MultiTenantKit.Core;
using MultiTenantKit.Core.Context;
using MultiTenantKit.Core.Enumerations;
using MultiTenantKit.Core.Models;
using MultiTenantKit.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MultiTenantKit.Hosting.Events;

namespace MultiTenantKit.Hosting
{
    /// <summary>
    /// This middleware needs the three services: Resolver, Mapper,Info
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public class MultiTenantKitMiddleware<TTenant, TTenantMapping> where TTenant : ITenant where TTenantMapping : ITenantMapping
    {
        private RequestDelegate _next { get; }
        private ITenantResolverService TenantResolverService { get; set; }
        private ITenantMapperService<TTenantMapping> TenantMapperService { get; set; }
        private ITenantInfoService<TTenant> TenantInfoService { get; set; }

        private MultiTenantKitMiddlewareEvents MiddlewareEvents { get; }

        public MultiTenantKitMiddleware(RequestDelegate next, IOptionsMonitor<MultiTenantKitMiddlewareEvents> middlewareEvents)
        {
            _next = next;
            MiddlewareEvents = middlewareEvents.CurrentValue;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string _tenantResolvedData = "";
            string _tenantUrlSlug = "";

            TenantContext<TTenant> _tenantContext = null;
            TenantResolveResult _tenantResolveResult = null;
            TenantMapResult<TTenantMapping> _tenantMapResult = null;

            TTenant _tenant = default;

            bool _needMapping = false;

            TenantResolverService = httpContext.RequestServices.GetRequiredService<ITenantResolverService>();
            TenantInfoService = httpContext.RequestServices.GetRequiredService<ITenantInfoService<TTenant>>();

            try
            {
                _tenantResolveResult = await TenantResolverService.ResolveTenantAsync(httpContext);
            }
            catch (Exception ex)
            {
                MiddlewareEvents.TenantResolutionErrorEvent(httpContext.Response, ex);
            }

            if (_tenantResolveResult.ResolutionResult == ResolutionResult.Success)
            {
                _tenantResolvedData = _tenantResolveResult.Value;

                if (_tenantResolveResult.ResolutionType == ResolutionType.TenantName)
                {
                    _tenantUrlSlug = _tenantResolvedData;
                    _needMapping = true;
                }
            }

            if (_tenantResolveResult.ResolutionResult == ResolutionResult.NotApply)
            {
                await _next(httpContext);
                return;
            }

            if (_tenantResolveResult.ResolutionResult == ResolutionResult.NotFound)
            {
                MiddlewareEvents.TenantResolutionNotFoundEvent(httpContext.Response);
            }

            if (_needMapping)
            {
                TenantMapperService = httpContext.RequestServices.GetRequiredService<ITenantMapperService<TTenantMapping>>();

                try
                {
                    _tenantMapResult = await TenantMapperService.MapTenantAsync(_tenantResolveResult.Value);
                }
                catch (Exception ex)
                {
                    MiddlewareEvents.TenantMappingErrorEvent(httpContext.Response, ex);
                }

                if (_tenantMapResult.MappingResult == MappingResult.Success)
                {
                    _tenantResolvedData = _tenantMapResult.Value.TenantId;
                }

                if (_tenantMapResult.MappingResult == MappingResult.NotFound)
                {
                    MiddlewareEvents.TenantMappingNotFoundEvent(httpContext.Response);
                }
            }

            //at this point it must be info
            if (!string.IsNullOrWhiteSpace(_tenantResolvedData))
            {
                _tenant = await TenantInfoService.GetTenantInfoAsync(_tenantResolvedData);
            }

            if (_tenant != null)
            {
                _tenantContext = new TenantContext<TTenant>(_tenant);

                if (_needMapping)
                {
                    _tenantContext.TenantUrlSlug = _tenantUrlSlug;
                }
                else
                {
                    _tenantContext.TenantUrlSlug = _tenantResolvedData;
                }

                httpContext.SetTenantContext(_tenantContext);
            }

            await _next(httpContext);
        }
    }
}
