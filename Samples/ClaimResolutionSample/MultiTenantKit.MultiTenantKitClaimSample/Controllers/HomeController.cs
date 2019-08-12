using MultiTenantKit.Core.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MultiTenantKit.MultiTenantClaimSample.Models;
using MultiTenantKit.MultiTenantClaimSample.MultiTenantImplementations;
using System.Diagnostics;
using System.Security.Claims;

namespace MultiTenantKit.MultiTenantClaimSample.Controllers
{
    public class HomeController : Controller
    {
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("Cookies");

            if (model.Password != "123456")
            {
                ViewBag.ErrorMessage = "Username or password invalid.";
                return View();
            }

            switch (model.Username)
            {
                case "tenant1":
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "tenant1"));
                    claimsIdentity.AddClaim(new Claim("TenantId", "e1009e1b-da1e-481a-b902-417e84ad349a"));
                    break;

                case "tenant2":
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "tenant2"));
                    claimsIdentity.AddClaim(new Claim("TenantId", "21a7227c-0471-4b3c-976a-2e09cfed537b"));
                    break;

                default:
                    ViewBag.ErrorMessage = "Username or password invalid.";
                    return View();
            }

            claimsPrincipal.AddIdentity(claimsIdentity);

            HttpContext.SignInAsync("Cookies", claimsPrincipal);

            return RedirectToAction("Index");
        }

        [Route("Dashboard")]
        [Authorize]
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
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Signout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
