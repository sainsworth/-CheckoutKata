using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckoutKata.Models.Contract;
using CheckoutKata.Domain.Exceptions;

namespace CheckoutKata.Models
{
    public class PriceList : IPriceList
    {
        #region private properties

        private List<ShopItem> _shopItemList;

        #endregion

        #region public properties
        #endregion

        #region private methods
        #endregion

        #region public methods

        public bool HasItem(string sku)
        {
            return _shopItemList.FirstOrDefault(x => x.SKU == sku) != null;
        } 

        public ShopItem LookupItem(string sku)
        {
            var pricelistItem = _shopItemList.FirstOrDefault(x => x.SKU == sku);
            if (pricelistItem == null)
                throw new UnknownItemException(sku);
            return pricelistItem;
        }

        public void UpsertItem(ShopItem item)
        {
            var pricelistItem = _shopItemList.FirstOrDefault(x => x.SKU == item.SKU);
            if (pricelistItem == null)
                _shopItemList.Add(item);
            else
                pricelistItem.Update(item);
        }

        public void UpsertItem(string sku, int price)
        {
            UpsertItem(new ShopItem(sku, price));
        }

        public void UpsertItem(string sku, int price, int discountFor, int discount)
        {
            UpsertItem(new ShopItem(sku, price, discountFor, discount));
        }

        public override string ToString()
        {
            if (_shopItemList.Count == 0)
                return "Empty pricelist";
            else
            {
                StringBuilder ret = new StringBuilder(_shopItemList[0].ToString());
                foreach (var item in _shopItemList.Skip(1))
                    ret.Append(System.Environment.NewLine).Append(item.ToString());

                return ret.ToString();
            }
        }

        #endregion

        #region constructors

        public PriceList()
        {
            _shopItemList = new List<ShopItem>();
        }

        public PriceList(List<ShopItem> initialPriceList)
        {
            _shopItemList = initialPriceList;
        }

        #endregion
    }
}

