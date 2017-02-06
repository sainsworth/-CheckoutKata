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

    public class ExcessiveDiscountException : Exception
    {
        public ExcessiveDiscountException(string sku, int itemPrice, int discountFor, int discount)
            : base (string.Format("Cannot add a discount of {0} when buying {1} items for SKU {2} as the discount per item is greater than or equal to the price {3}", discount,discountFor,sku,itemPrice))
        {
        }
    }
}
