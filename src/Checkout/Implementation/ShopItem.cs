using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Implementation
{
    public class ShopItem
    {

        #region Private Properties
        #endregion

        #region Public Properties

        public string SKU { get; set; }
        public int ItemPrice { get; set; }
        public int DiscountFor { get; set; }
        public int Discount { get; set; }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

        #region Constructors

        public ShopItem(string sku, int itemPrice, int discountFor, int discount)
        {
            SKU = sku;
            ItemPrice = itemPrice;
            DiscountFor = discountFor;
            Discount = discount;
        }

        public ShopItem(string sku, int itemPrice)
            :this(sku,itemPrice,0,0)
        {}

        #endregion
    }
}
