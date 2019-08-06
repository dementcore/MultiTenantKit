using DementCore.MultiTenantKit.Core.Enumerations;
using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
{
    public class TenantMapResult<TTenantMapping>
    {
        #region Public Constructors

        public TenantMapResult(TTenantMapping value)
        {
            if (value == null)
            {
                MappingResult = MappingResult.NotFound;
            }
            else
            {
                MappingResult = MappingResult.Success;
            }

            Value = value;
            ErrorMessage = "";
        }

        public TenantMapResult(Exception exception)
        {
            if (exception == null)
            {
                exception = new Exception("Unable to map the tenant's from tenant's name");
            }

            Value = default;
            MappingResult = MappingResult.Error;
            ErrorMessage = exception?.Message ?? "";
        }

        public TenantMapResult(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = "Unable to resolve the tenant's name from route.";
            }

            Value = default;
            MappingResult = MappingResult.Error;
            ErrorMessage = errorMessage;
        }

        #endregion

        #region Private Constructors

        private TenantMapResult(MappingResult mappingResult)
        {
            MappingResult = mappingResult;
            Value = default;
            ErrorMessage = "";
        }

        #endregion

        #region Public Static Properties

        public static TenantMapResult<TTenantMapping> NotFound { get; } = new TenantMapResult<TTenantMapping>(MappingResult.NotFound);

        #endregion

        #region Public Properties

        public TTenantMapping Value { get; }

        public string ErrorMessage { get; }

        public MappingResult MappingResult { get; }

        #endregion
    }
}
