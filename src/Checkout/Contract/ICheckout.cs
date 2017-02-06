using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Contract
{
    interface ICheckout
    {
        void Scan(string item);
        int Total();
    }
}
