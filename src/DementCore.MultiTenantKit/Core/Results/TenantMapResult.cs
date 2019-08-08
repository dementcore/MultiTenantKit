using DementCore.MultiTenantKit.Core.Enumerations;
using DementCore.MultiTenantKit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
{
    public class TenantMapResult<TTenantMapping> where TTenantMapping : ITenantMapping
    {
        #region Public Constructors

        public TenantMapResult(TTenantMapping value)
        {
            if (value.Equals(default(TTenantMapping)))
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
                throw new ArgumentNullException("You must specify an exception.");
            }

            Value = default;
            MappingResult = MappingResult.Error;
            ErrorMessage = exception?.Message ?? "";
        }

        public TenantMapResult(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException("You must specify a error message ");
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
