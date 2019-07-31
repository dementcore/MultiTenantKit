using DementCore.MultiTenantKit.Core.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyMultitenantWebApplication.MultiTenantImplementations
{
    public class MyCustomTenantResolver : ITenantResolver
    {
        public Task<string> ResolveTenantAsync(HttpContext httpRequest)
        {
            return Task.FromResult("carlos");
        }
    }
}
