using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Utilities.Exceptions
{
    public class ShopMatesException : Exception
    {
        public ShopMatesException() { }
        public ShopMatesException(string message) : base(message) { }

        public ShopMatesException(string message, Exception innerException) : base(message, innerException) { }
    }
}
