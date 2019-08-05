using DementCore.MultiTenantKit.Core.Attributes;
using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;

namespace DementCore.MultiTenantKit.Core.Stores.Default
{
    public class InMemoryTenantStore<TTenant> : ITenantStore<TTenant> where TTenant : ITenant
    {
        private List<TTenant> Tenants { get; }

        public InMemoryTenantStore(List<TTenant> tenants)
        {
            Tenants = tenants;
        }

        public List<TTenant> GetTenants()
        {
            return Tenants;
        }

        public TTenant GetTenantByTenantId(string tenantId)
        {
            System.Reflection.PropertyInfo[] props = typeof(TTenant).GetProperties();

            string tenantIdPropertyName = "";

            //search for property representing TenantId in the TTenant type, the property should be marked with TenantId attributte
            foreach (System.Reflection.PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(typeof(TenantIdAttribute), false);

                if (attrs.Length > 0)
                {
                    //we have found the Tenant´s Id property 
                    tenantIdPropertyName = prop.Name;
                    break;
                }
            }

            //if we haven`t found the Tenant's Id property we do nothing
            if (!string.IsNullOrWhiteSpace(tenantIdPropertyName))
            {
                TTenant tenant = Tenants.Find(ts => Convert.ToString(ts.GetType().GetProperty(tenantIdPropertyName).GetValue(ts)) == tenantId);

                if (tenant == null)
                {
                    tenant = default;
                }

                return tenant;
            }

            return default;
        }
    }
}
