using DementCore.MultiTenantKit.Core;
using DementCore.MultiTenantKit.Core.Context;
using DementCore.MultiTenantKit.Core.Models;
using DementCore.MultiTenantKit.Core.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Hosting
{
    /// <summary>
    /// This middleware needs the three services: Resolver, Mapper,Info
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    public class MultiTenantKitMiddleware<TTenant> where TTenant : ITenant
    {
        private readonly RequestDelegate _next;

        public MultiTenantKitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITenantResolverService tenantResolverService, ITenantMapperService tenantMapperService, ITenantInfoService<TTenant> tenantInfoService)
        {

            TenantResolveResult _tenantResolveResult = null;
            TenantContext<TTenant> _tenantContext = null;
            TTenant _tenant = default;
            string _tenantId = "";
            string _tenantSlug = "";

            _tenantResolveResult = await tenantResolverService.ResolveTenantAsync(httpContext);

            switch (_tenantResolveResult.ResolvedType)
            {
                case ResolvedType.TenantId:

                    #region TenantIdResolution

                    _tenantId = _tenantResolveResult.Value;

                    //call the info service directly with resolved TenantId
                    if (!string.IsNullOrWhiteSpace(_tenantId))
                    {
                        _tenant = await tenantInfoService.GetTenantInfoAsync(_tenantId);
                    }

                    if (_tenant == null)
                    {
                        _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.NotFound);
                    }
                    else
                    {
                        _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.TenantId);
                    }

                    #endregion

                    break;

                case ResolvedType.TenantSlug:

                    #region TenantSlugResolution

                    //only call the mapper service if the resolution is of type tenantSlug
                    if (!string.IsNullOrWhiteSpace(_tenantResolveResult.Value))
                    {
                        _tenantId = await tenantMapperService.MapTenantAsync(_tenantResolveResult.Value);

                        _tenantSlug = _tenantResolveResult.Value;
                    }

                    //call the info service after mapping the tenant slug
                    if (!string.IsNullOrWhiteSpace(_tenantId))
                    {
                        _tenant = await tenantInfoService.GetTenantInfoAsync(_tenantId);
                    }

                    if (_tenant == null)
                    {
                        _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.NotFound);
                    }
                    else
                    {
                        _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.TenantSlug);
                    }

                    #endregion

                    break;

                case ResolvedType.NotApply:

                    //do nothing because the resolution does not apply in this request
                    _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.NotApply);

                    break;

                case ResolvedType.NotFound:

                    //tenant not found
                    _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.NotFound);

                    break;

                case ResolvedType.Error:

                    _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolvedType.Error);

                    break;
            }

            httpContext.SetTenantContext(_tenantContext);


            await _next(httpContext);
        }
    }
}
