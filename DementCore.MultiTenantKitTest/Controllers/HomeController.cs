using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMultitenantWebApplication.Models;
using MyMultitenantWebApplication.MultiTenantImplementations;
using System.Diagnostics;

namespace MyMultitenantWebApplication.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            IndexModel model = new IndexModel();

            MyTenant tenant = HttpContext.GetTenant<MyTenant>();

            model.TenantName = tenant.Name;
           
            return View(model);
        }

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
