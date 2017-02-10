using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckoutKata.Models.Contract;
using CheckoutKata.Domain.Exceptions;

namespace CheckoutKata.Models
{
    public class Checkout : ICheckout
    {
        #region Private Properties

        private IPriceList _priceList;
        private Dictionary<string, int> _scanned;

        #endregion

        #region Public Properties

        public IPriceList PriceList {
            get { return _priceList; }
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        public void Scan(string sku)
        {
            if (_priceList.HasItem(sku))
            {
                if (_scanned.ContainsKey(sku))
                    _scanned[sku]++;
                else
                    _scanned.Add(sku, 1);
            }
            else
                throw new UnknownItemException(sku);

        }

        public int Total()
        {
            int total = 0;

            foreach(var item in _scanned.ToList()){
                var pricelistItem = _priceList.LookupItem(item.Key);
                total += item.Value * pricelistItem.ItemPrice;
                if (pricelistItem.DiscountFor > 0)
                {
                    int discount = (item.Value / pricelistItem.DiscountFor) * pricelistItem.Discount;
                    total -= discount;
                }
            }

            return total;
        }

        #endregion

        #region Constructors

        public Checkout(List<ShopItem> shopItemlist)
        {
            _scanned = new Dictionary<string, int>();
            _priceList = new PriceList(shopItemlist);
        }

        #endregion
    }
}
