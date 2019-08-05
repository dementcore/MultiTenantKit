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

            switch (_tenantResolveResult.ResolutionResult)
            {
                #region ResolutionResult.Success

                case ResolutionResult.Success:

                    switch (_tenantResolveResult.ResolutionType)
                    {
                        #region ResolutionType.TenantId

                        case ResolutionType.TenantId:

                            _tenantId = _tenantResolveResult.Value;

                            //call the info service directly with resolved TenantId
                            if (!string.IsNullOrWhiteSpace(_tenantId))
                            {
                                _tenant = await tenantInfoService.GetTenantInfoAsync(_tenantId);
                            }

                            if (_tenant == default)
                            {
                                _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolutionResult.NotFound);
                            }
                            else
                            {
                                _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, _tenantResolveResult.ResolutionResult, _tenantResolveResult.ResolutionType);
                            }

                            break;

                        #endregion

                        #region ResolutionType.TenantSlug

                        case ResolutionType.TenantName:


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

                            if (_tenant == default)
                            {
                                _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolutionResult.NotFound);
                            }
                            else
                            {
                                _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, _tenantResolveResult.ResolutionResult, _tenantResolveResult.ResolutionType);
                            }

                            break;

                        #endregion

                        #region ResolutionType.Nothing

                        case ResolutionType.Nothing:

                            break;

                        #endregion
                    }

                    break;

                #endregion

                #region ResolutionResult.NotApply

                case ResolutionResult.NotApply:

                    //do nothing because the resolution does not apply in this request
                    _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolutionResult.NotApply);

                    break;

                #endregion

                #region ResolutionResult.NotFound

                case ResolutionResult.NotFound:

                    //tenant not found
                    _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolutionResult.NotFound);

                    break;

                #endregion

                #region ResolutionResult.Error

                case ResolutionResult.Error:

                    _tenantContext = new TenantContext<TTenant>(_tenant, _tenantSlug, ResolutionResult.Error);

                    break;

                    #endregion
            }

            httpContext.SetTenantContext(_tenantContext);

            await _next(httpContext);
        }
    }
}
