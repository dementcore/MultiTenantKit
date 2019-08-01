using DementCore.MultiTenantKit.Core.Attributes;
using DementCore.MultiTenantKit.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DementCore.MultiTenantKit.Core.Stores.Default
{
    public class InMemoryTenantStore<TTenant> : ITenantStore<TTenant> where TTenant : ITenant
    {
        IConfiguration Configuration { get; }

        public InMemoryTenantStore(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Task<TTenant> GetTenantInfo(string tenantId)
        {
            IConfigurationSection cs = Configuration.GetSection("Tenants:TenantsData");
            List<TTenant> tenants = new List<TTenant>();

            cs.Bind(tenants);

            System.Reflection.PropertyInfo[] props = typeof(TTenant).GetProperties();

            string tenantIdPropertyName = "";

            foreach (System.Reflection.PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(TenantIdAttribute), false);

                if (attrs.Length > 0)
                {
                    //tenemos el atributo
                    tenantIdPropertyName = prop.Name;
                    break;
                }
            }

            if (!string.IsNullOrWhiteSpace(tenantIdPropertyName))
            {
                TTenant tenant = tenants.Find(ts => ts.GetType().GetProperty(tenantIdPropertyName).GetValue(ts).ToString() == tenantId);

                if (tenant == null)
                {
                    tenant = (TTenant)Activator.CreateInstance(typeof(TTenant));
                }

                return Task.FromResult(tenant);
            }
            else
            {
                TTenant entidad = default;

                entidad = (TTenant)Activator.CreateInstance(typeof(TTenant));

                return Task.FromResult(entidad);
            }
        }

        public List<TTenant> GetTenants()
        {
            throw new NotImplementedException();
        }

        public TTenant GetTenantByTenantId(string tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
