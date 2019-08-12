using MultiTenantKit.Core.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MultiTenantKit.MultiTenantDomainSample.Models;
using MultiTenantKit.MultiTenantDomainSample.MultiTenantImplementations;
using System.Diagnostics;
using System.Security.Claims;

namespace MultiTenantKit.MultiTenantDomainSample.Controllers
{
    public class HomeController : Controller
    {
        [Route("Dashboard")]
        public IActionResult Index()
        {
            IndexModel model = new IndexModel();

            TenantContext<CustomTenant> tenantCtx = HttpContext.GetTenantContext<CustomTenant>();

            if (tenantCtx != null)
            {

                model.TenantName = tenantCtx.Tenant?.Name ?? "";
                model.TenantId = tenantCtx.Tenant?.Id ?? "";
                model.TenantCssTheme = tenantCtx.Tenant?.CSSTheme ?? "";

            }

            return View(model);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
