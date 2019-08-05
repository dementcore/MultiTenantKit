using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
{
    public class TenantResolveResult
    {
        /// <summary>
        /// Create a TenantResolveResult that indicates that the tenant's resolution does not apply in this request.
        /// </summary>
        public TenantResolveResult()
        {
            Value = "";
            ResolvedType = ResolvedType.NotApply;
            ErrorMessage = "";
        }

        /// <summary>
        /// Creates a TenantResolveResult that indicates a tenant resolution has been performed.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="resolvedType"></param>
        public TenantResolveResult(string value, ResolvedType resolvedType = ResolvedType.TenantSlug)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = value;
                ResolvedType = ResolvedType.NotApply;
                ErrorMessage = "";
            }
            else
            {
                Value = value;
                ResolvedType = resolvedType;
                ErrorMessage = "";
            }
        }

        /// <summary>
        /// Creates a TenantResolveResult that indicates and error in tenant's resolution
        /// </summary>
        /// <param name="exception"></param>
        public TenantResolveResult(Exception exception)
        {
            if (exception == null)
            {
                exception = new Exception("Unable to resolve the tenant's slug from route.");
            }

            Value = "";
            ResolvedType = ResolvedType.Error;
            ErrorMessage = exception?.Message ?? "";
        }

        /// <summary>
        /// Creates a TenantResolveResult that indicates and error in tenant's resolution
        /// </summary>
        /// <param name="errorMessage"></param>
        public TenantResolveResult(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = "Unable to resolve the tenant's slug from route.";
            }

            Value = "";
            ResolvedType = ResolvedType.Error;
            ErrorMessage = errorMessage;
        }

        public string Value { get; }

        public string ErrorMessage { get; }

        public ResolvedType ResolvedType { get; }

        /// <summary>
        /// Indicates that the tenant's resolution does not apply in this request.
        /// </summary>
        public static TenantResolveResult NotApply { get; } = new TenantResolveResult();

        /// <summary>
        /// Indicates that the tenant's is not found.
        /// </summary>
        public static TenantResolveResult NotFound { get; } = new TenantResolveResult("", ResolvedType.NotFound);
    }
}
