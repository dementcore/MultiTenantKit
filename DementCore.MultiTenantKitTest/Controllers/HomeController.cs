using DementCore.MultiTenantKit.Core.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MyMultitenantWebApplication.Models;
using MyMultitenantWebApplication.MultiTenantImplementations;
using System.Diagnostics;
using System.Security.Claims;

namespace MyMultitenantWebApplication.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity("Cookies");
            claimsIdentity.AddClaim(new Claim("Inquilino", "tenant2"));
            claimsIdentity.AddClaim(new Claim("Nombre", "Inquilino 1"));

            claimsPrincipal.AddIdentity(claimsIdentity);

            HttpContext.SignInAsync("Cookies", claimsPrincipal);

            return RedirectToAction("Index", new { Inquilino = "tenant2" });
        }

        [Route("{Inquilino}/Dashboard")]
        [Authorize]
        public IActionResult Index()
        {
            IndexModel model = new IndexModel();

            TenantContext<MyTenant> tenantCtx = HttpContext.GetTenantContext<MyTenant>();

            model.TenantName = tenantCtx.Tenant?.Name ?? "";

            return View(model);
        }

        [Route("{Inquilino}/Privacy")]
        [Authorize]
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
