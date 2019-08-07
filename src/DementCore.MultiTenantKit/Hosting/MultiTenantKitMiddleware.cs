using DementCore.MultiTenantKit.Core;
using DementCore.MultiTenantKit.Core.Context;
using DementCore.MultiTenantKit.Core.Enumerations;
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
    public class MultiTenantKitMiddleware<TTenant, TTenantMapping> where TTenant : ITenant where TTenantMapping : ITenantMapping
    {
        private readonly RequestDelegate _next;

        public MultiTenantKitMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext httpContext, ITenantResolverService tenantResolverService, ITenantMapperService<TTenantMapping> tenantMapperService,
            ITenantInfoService<TTenant> tenantInfoService)
        {
            TenantContext<TTenant> _tenantContext = null;

            TenantResolveResult _tenantResolveResult = null;
            TenantMapResult<TTenantMapping> _tenantMapResult = null;

            TTenant _tenant = default;
            TTenantMapping _tenantMapping = default;

            ResolutionResult _resolutionResult = ResolutionResult.NotFound;
            MappingResult _mappingResult = MappingResult.NotFound;
            ResolutionType _resolutionType = ResolutionType.Nothing;

            string _tenantResolvedData = "";

            _tenantResolveResult = await tenantResolverService.ResolveTenantAsync(httpContext);
            _resolutionResult = _tenantResolveResult.ResolutionResult;

            switch (_resolutionResult)
            {
                case ResolutionResult.Success:

                    _resolutionType = _tenantResolveResult.ResolutionType;
                    _tenantResolvedData = _tenantResolveResult.Value;

                    switch (_resolutionType)
                    {
                        case ResolutionType.TenantId:
                            _mappingResult = MappingResult.NotApply;
                            break;

                        case ResolutionType.TenantName:
                            _mappingResult = MappingResult.Success;
                            break;
                    }

                    break;

                case ResolutionResult.NotApply:

                    _mappingResult = MappingResult.NotApply;
                    _tenantResolvedData = "";
                    break;

                case ResolutionResult.NotFound:

                    _mappingResult = MappingResult.NotFound;
                    _tenantResolvedData = "";
                    break;

                case ResolutionResult.Error:

                    _mappingResult = MappingResult.Error;
                    _tenantResolvedData = "";
                    break;
            }

            if (_resolutionResult == ResolutionResult.Success)
            {
                if (_mappingResult != MappingResult.NotApply) //if applies mapping call mapping service
                {
                    _tenantMapResult = await tenantMapperService.MapTenantAsync(_tenantResolveResult.Value);
                    _mappingResult = _tenantMapResult.MappingResult;

                    switch (_mappingResult)
                    {
                        case MappingResult.Success:

                            _tenantMapping = _tenantMapResult.Value;
                            _tenantResolvedData = _tenantMapping.TenantId;

                            break;

                        case MappingResult.NotFound:

                            _tenantResolvedData = "";
                            _resolutionResult = ResolutionResult.NotFound;
                            _resolutionType = ResolutionType.Nothing;

                            break;

                        case MappingResult.Error:

                            _tenantResolvedData = "";
                            _resolutionResult = ResolutionResult.Error;
                            _resolutionType = ResolutionType.Nothing;

                            break;
                    }
                }

                //at this point it must be the id
                if (!string.IsNullOrWhiteSpace(_tenantResolvedData))
                {
                    _tenant = await tenantInfoService.GetTenantInfoAsync(_tenantResolvedData);
                }
            }

            _tenantContext = new TenantContext<TTenant>(_tenant, _resolutionResult, _mappingResult, _resolutionType);

            httpContext.SetTenantContext(_tenantContext);

            await _next(httpContext);
        }
    }
}
