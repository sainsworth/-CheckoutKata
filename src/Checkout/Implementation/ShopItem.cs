using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckoutKata.Domain.Exceptions;

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

        public override string ToString()
        {
            if (DiscountFor == 0)
                return string.Format("{0} is {1} each", SKU, ItemPrice);
            else if (DiscountFor == 1)
                return string.Format("{0} is {1} each (discounted from {2})", SKU, (ItemPrice - Discount), ItemPrice);
            else
                return string.Format("{0} is {1} each or {2} for {3}", SKU, ItemPrice, DiscountFor, (ItemPrice * DiscountFor) - Discount);
        }

        public void Update(ShopItem item)
        {
            ItemPrice = item.ItemPrice;
            DiscountFor = item.DiscountFor;
            Discount = item.Discount;
        }
        #endregion

        #region Constructors

        public ShopItem(string sku, int itemPrice, int discountFor, int discount)
        {
            if (discountFor > 0)
            {
                var discountedPricePerItem = (itemPrice * discountFor) - discount;
                if (discountedPricePerItem <= 0)
                    throw new ExcessiveDiscountException(sku, itemPrice, discountFor, discount);
            }
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
