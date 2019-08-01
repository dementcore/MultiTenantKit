using DementCore.MultiTenantKit.Core.Stores;
using System.Threading.Tasks;

namespace MyMultitenantWebApplication.MultiTenantImplementations
{
    public class MyCustomTenantStore : ITenantStore<MyTenant>
    {
        public Task<MyTenant> GetTenantInfo(string tenantId)
        {
            MyTenant tenantImp = new MyTenant()
            {
                Id = "1",
                Name = "Tenant 1",
                CSSTheme = "/css/site.css"
            };

            MyTenant tenantImp2 = new MyTenant()
            {
                Id = "2",
                Name = "Tenant 2",
                CSSTheme = "/css/site2.css"
            };

            switch (tenantId)
            {
                case "1":
                    return Task.FromResult(tenantImp);
                    break;

                case "2":
                    return Task.FromResult(tenantImp2);
                    break;

                default:
                    return Task.FromResult(new MyTenant());
                    break;
            }
        }
    }
}
