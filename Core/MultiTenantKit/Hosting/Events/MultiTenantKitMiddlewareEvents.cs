using Microsoft.AspNetCore.Http;
using MultiTenantKit.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Hosting.Events
{
    public class MultiTenantKitMiddlewareEvents
    {
        public Action<HttpResponse> TenantResolutionNotFoundEvent { get; set; } = (res) =>
        {
            throw new MultiTenantKitException("The tenant can't be resolved because is not found in request");
        };

        public Action<HttpResponse,Exception> TenantResolutionErrorEvent { get; set; } = (res,ex) =>
        {
            throw new MultiTenantKitException("Tenant resolution error", ex);
        };

        public Action<HttpResponse> TenantMappingNotFoundEvent { get; set; } = (res) =>
        {
            throw new MultiTenantKitException("The tenant's identification could not be mapped against the resolved name because no mapping can be found.");
        };

        public Action<HttpResponse, Exception> TenantMappingErrorEvent { get; set; } = (res, ex) =>
        {
            throw new MultiTenantKitException("Tenant mapping error", ex);
        };
    }
}
