using System;
using System.Collections.Generic;
using System.Text;

namespace DementCore.MultiTenantKit.Core
{
    public class MultiTenantKitException : Exception
    {
        public MultiTenantKitException(string message) : base(message)
        {

        }

        public MultiTenantKitException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
