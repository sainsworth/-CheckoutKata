using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Domain.Exceptions
{
    public class UnknownItemException : Exception
    {
        public UnknownItemException(string item)
            : base (string.Format("Unknown item scanned: {0}", item))
        {
        }
    }
}
