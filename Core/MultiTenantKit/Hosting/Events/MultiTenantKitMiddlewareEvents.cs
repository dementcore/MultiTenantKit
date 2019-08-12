using Microsoft.AspNetCore.Http;
using MultiTenantKit.Core;
using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Hosting.Events
{
    public class MultiTenantKitMiddlewareEvents<TTenant, TTenantMapping> : IMultiTenantKitMiddlewareEvents<TTenant, TTenantMapping>
        where TTenant : ITenant
        where TTenantMapping : ITenantMapping
    {
        public Action<HttpResponse,string> TenantResolutionSuccessEvent { get; set; } = (res,data) =>
        {

        };

        public Action<HttpResponse> TenantResolutionNotFoundEvent { get; set; } = (res) =>
        {
            throw new MultiTenantKitException("The tenant can't be resolved because is not found in request");
        };

        public Action<HttpResponse> TenantResolutionNotApplyEvent { get; set; } = (res) =>
        {

        };

        public Action<HttpResponse, Exception> TenantResolutionErrorEvent { get; set; } = (res, ex) =>
        {
            throw new MultiTenantKitException("Tenant resolution internal error: ", ex);
        };

        public Action<HttpResponse,TenantMapResult<TTenantMapping>> TenantMappingSuccessEvent { get; set; } = (res,mapping) =>
        {

        };

        public Action<HttpResponse> TenantMappingNotFoundEvent { get; set; } = (res) =>
        {
            throw new MultiTenantKitException("The tenant's identification could not be mapped against the resolved name because no mapping can be found.");
        };

        public Action<HttpResponse, Exception> TenantMappingErrorEvent { get; set; } = (res, ex) =>
        {
            throw new MultiTenantKitException("Tenant mapping internal error: ", ex);
        };

        public Action<HttpResponse,TenantInfoResult<TTenant>> TenantInfoSuccessEvent { get; set; } = (res,tenant) =>
        {

        };

        public Action<HttpResponse> TenantInfoNotFoundEvent { get; set; } = (res) =>
        {
            throw new MultiTenantKitException("The tenant entity can't be found in info service");
        };

        public Action<HttpResponse, Exception> TenantInfoErrorEvent { get; set; } = (res, ex) =>
        {
            throw new MultiTenantKitException("Tenant info internal error: ", ex);
        };
    }
}
