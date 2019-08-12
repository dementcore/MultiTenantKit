using Microsoft.AspNetCore.Http;
using MultiTenantKit.Core;
using MultiTenantKit.Core.Models;
using System;

namespace MultiTenantKit.Hosting.Events
{
    public interface IMultiTenantKitMiddlewareEvents<TTenant, TTenantMapping>
        where TTenant : ITenant
        where TTenantMapping : ITenantMapping
    {
        Action<HttpResponse, string> TenantResolutionSuccessEvent { get; set; }

        Action<HttpResponse> TenantResolutionNotFoundEvent { get; set; }

        Action<HttpResponse> TenantResolutionNotApplyEvent { get; set; }

        Action<HttpResponse, Exception> TenantResolutionErrorEvent { get; set; }


        Action<HttpResponse, TenantMapResult<TTenantMapping>> TenantMappingSuccessEvent { get; set; }

        Action<HttpResponse> TenantMappingNotFoundEvent { get; set; }

        Action<HttpResponse, Exception> TenantMappingErrorEvent { get; set; }

        Action<HttpResponse, TenantInfoResult<TTenant>> TenantInfoSuccessEvent { get; set; }

        Action<HttpResponse> TenantInfoNotFoundEvent { get; set; }

        Action<HttpResponse, Exception> TenantInfoErrorEvent { get; set; }

    }
}
