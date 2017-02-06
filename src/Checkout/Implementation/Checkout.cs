using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckoutKata.Contract;
using CheckoutKata.Domain.Exceptions;

namespace CheckoutKata.Implementation
{
    public class Checkout : ICheckout
    {
        #region Private Properties

        private List<ShopItem> _priceList;
        private Dictionary<string, int> _scanned;

        #endregion

        #region Public Properties
        
        #endregion

        #region Private Methods
        private ShopItem _lookupItem(string item)
        {
            var pricelistItem = _priceList.FirstOrDefault(x => x.SKU == item);
            if (pricelistItem == null)
                throw new UnknownItemException(item);
            return pricelistItem;
        }
        #endregion

        #region Public Methods

        public void Scan(string item)
        {
            _lookupItem(item);

            if (_scanned.ContainsKey(item))
                _scanned[item]++;
            else
                _scanned.Add(item, 1);
        }

        public int Total()
        {
            int total = 0;

            foreach(var item in _scanned.ToList()){
                var pricelistItem = _lookupItem(item.Key);
                total += item.Value * pricelistItem.ItemPrice;
                if (pricelistItem.DiscountFor > 0)
                {
                    int discount = (item.Value / pricelistItem.DiscountFor) * pricelistItem.Discount;
                    total -= discount;
                }
            }

            return total;
        }

        public void UpsertShopItem(ShopItem item)
        {
            var pricelistItem = _priceList.FirstOrDefault(x => x.SKU == item.SKU);
            if (pricelistItem == null)
                _priceList.Add(item);
            else
                pricelistItem.Update(item);
        }

        #endregion

        #region Constructors

        public Checkout()
        {
            _priceList = new List<ShopItem>();
            _scanned = new Dictionary<string, int>();
        }

        public Checkout(List<ShopItem> pricelist)
            : this()
        {
            _priceList = pricelist;
        }
        #endregion
    }
}
