using MultiTenantKit.Core.Attributes;
using MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;

namespace MultiTenantKit.Core.Stores.InMemory
{
    public class InMemoryTenantStore<TTenant> : ITenantStore<TTenant> where TTenant : ITenant
    {
        private List<TTenant> Tenants { get; }

        public InMemoryTenantStore(List<TTenant> tenants)
        {
            Tenants = tenants;
        }

        public TTenant GetTenantByTenantId(string tenantId)
        {
            string tenantIdPropertyName = SearchTenantIdProperty();

            //if we haven`t found the Tenant's Id property we do nothing
            if (string.IsNullOrWhiteSpace(tenantIdPropertyName))
            {
                throw new MultiTenantKitException("Unable to find TenantId property. Use the TenantId Attribute to mark the id property or rename the id property to TenantId");
            }

            TTenant tenant = Tenants.Find(ts => Convert.ToString(ts.GetType().GetProperty(tenantIdPropertyName).GetValue(ts)) == tenantId);

            if (tenant == null)
            {
                tenant = default;
            }

            return tenant;
        }

        private string SearchTenantIdProperty()
        {
            string tenantIdPropertyName = "";

            System.Reflection.PropertyInfo[] props = typeof(TTenant).GetProperties();

            #region Search by Attribute

            //search for property representing TenantId in the TTenant type, the property should be marked with TenantId attributte or by name convention TenantId 
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

            #endregion

            #region Search by name convention

            //if we couldn't find the property finding properties marked with TenantId attribute, fallback to name convention searching
            if (string.IsNullOrWhiteSpace(tenantIdPropertyName))
            {
                foreach (System.Reflection.PropertyInfo prop in props)
                {
                    if (prop.Name == "TenantId")
                    {
                        //we have found the Tenant´s Id property 
                        tenantIdPropertyName = prop.Name;
                        break;
                    }
                }
            }

            #endregion

            return tenantIdPropertyName;
        }
    }
}
