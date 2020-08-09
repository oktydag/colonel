using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Core.Exceptions
{
    public class ProductIsNotAvailableException : Exception
    {
        public ProductIsNotAvailableException()
        {

        }

        public ProductIsNotAvailableException(string message)
            : base(message)
        {

        }
    }
}
