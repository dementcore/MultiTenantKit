using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantKit.Core.Models;

namespace MultiTenantKit.Mvc.Controllers
{
    public class TenantController<TTenant> : Controller
        where TTenant : ITenant
    {

        public TTenant Tenant
        {
            get
            {
                Core.Context.TenantContext<TTenant> tCtx = HttpContext.GetTenantContext<TTenant>();

                TTenant tenant = (tCtx != null) ? tCtx.Tenant : default;

                return tenant;
            }
        }

        public string TenantUrlSlug
        {
            get
            {
                Core.Context.TenantContext<TTenant> tCtx = HttpContext.GetTenantContext<TTenant>();

                string tenantSlug = (tCtx != null) ? tCtx.TenantUrlSlug : default;

                return tenantSlug;
            }
        }
    }
}
