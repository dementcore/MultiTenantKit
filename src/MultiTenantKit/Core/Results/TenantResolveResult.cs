using MultiTenantKit.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantKit.Core
{
    public sealed class TenantResolveResult
    {
        #region Public Constructors

        /// <summary>
        /// Creates a TenantResolveResult that indicates a tenant resolution has been performed.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="resolvedType"></param>
        public TenantResolveResult(string value, ResolutionType resolvedType)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = value;
                ResolutionResult = ResolutionResult.NotFound;
            }
            else
            {
                Value = value;
                ResolutionResult = ResolutionResult.Success;
                ResolutionType = resolvedType;
            }
        }

        #endregion

        #region Private Constructors

        /// <summary>
        /// Create a TenantResolveResult that indicates that the tenant resolution result of this request.
        /// </summary>
        /// <param name="resolutionResult"></param>
        private TenantResolveResult(ResolutionResult resolutionResult)
        {
            Value = "";
            ResolutionResult = resolutionResult;
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Indicates that the tenant resolution does not apply in this request.
        /// </summary>
        public static TenantResolveResult NotApply { get; } = new TenantResolveResult(ResolutionResult.NotApply);

        /// <summary>
        /// Indicates that the tenant is not found.
        /// </summary>
        public static TenantResolveResult NotFound { get; } = new TenantResolveResult(ResolutionResult.NotFound);

        #endregion

        #region Public Properties

        public string Value { get; }

        public ResolutionResult ResolutionResult { get; }

        public ResolutionType ResolutionType { get; }

        #endregion
    }
}
