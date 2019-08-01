using DementCore.MultiTenantKit.Core.Stores;
using System.Threading.Tasks;

namespace MyMultitenantWebApplication.MultiTenantImplementations
{
    public class MyCustomTenantStore : ITenantStore<MyTenant>
    {
        public Task<MyTenant> GetTenantInfo(string tenantId)
        {
            if (tenantId != "carlos")
            {
                return Task.FromResult<MyTenant>(null);
            }

            MyTenant tenantImp = new MyTenant()
            {
                Id = "1",
                Name = "Carlos SA",
                CSSTheme = "/css/site2.css"
            };

            return Task.FromResult(tenantImp);
        }
    }
}
