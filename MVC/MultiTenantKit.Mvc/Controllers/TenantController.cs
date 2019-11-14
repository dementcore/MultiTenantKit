using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MultiTenantKit.Core.Models;

namespace MultiTenantKit.Mvc.Controllers
{
    public class TenantController<TTenant> : Controller
        where TTenant : ITenant
    {
        /// <summary>
        /// Current Tenant
        /// </summary>
        public TTenant Tenant
        {
            get
            {
                Core.Context.TenantContext<TTenant> tCtx = HttpContext.GetTenantContext<TTenant>();

                TTenant tenant = (tCtx != null) ? tCtx.Tenant : default;

                return tenant;
            }
        }

        /// <summary>
        /// Current Tenant Url Slug
        /// </summary>
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
