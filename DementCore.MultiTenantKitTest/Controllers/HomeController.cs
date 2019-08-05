using DementCore.MultiTenantKit.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyMultitenantWebApplication.Models;
using MyMultitenantWebApplication.MultiTenantImplementations;
using System.Diagnostics;

namespace MyMultitenantWebApplication.Controllers
{
    [Route("{Inquilino}")]
    public class HomeController : Controller
    {
        [Route("Dashboard")]
        public IActionResult Index()
        {
            IndexModel model = new IndexModel();

            TenantContext<MyTenant> tenantCtx = HttpContext.GetTenantContext<MyTenant>();

            
            model.TenantName = tenantCtx.Tenant?.Name ?? "";

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
