using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CheckoutKata.Domain.Exceptions;
using CheckoutKata.Models;

namespace CheckoutKata.Tests
{

    [TestClass]
    public class Tests
    {

        /// <summary>
        /// When I invoke ToString on the PriceList I am returned a pricelist in the expected format
        /// </summary>
        [TestMethod]
        public void PriceList__PriceList_ToString()
        {
            var pricelist = new PriceList();
            Assert.AreEqual("Empty pricelist", pricelist.ToString());
            pricelist.UpsertItem(new ShopItem("A", 10));
            pricelist.UpsertItem(new ShopItem("B", 10, 1, 5));
            pricelist.UpsertItem(new ShopItem("C", 20, 2, 10));
            Assert.AreEqual(@"A is 10 each
B is 5 each (discounted from 10)
C is 20 each or 2 for 30", pricelist.ToString());
        }

        /// <summary>
        /// When I invoke 'HasItem' then if the sku is in the pricelist I am returned true, otherwise false
        /// </summary>
        [TestMethod]
        public void PriceList__Can_Add_item_to_pricelist()
        {
            var pricelist = new PriceList(new List<ShopItem> { new ShopItem("A", 10) });
            Assert.AreEqual(true, pricelist.HasItem("A"));
            Assert.AreEqual(false, pricelist.HasItem("B"));
        }

        /// <summary>
        /// When I invoke 'HasItem' then if the sku is in the pricelist I am returned true, otherwise false
        /// </summary>
        [TestMethod]
        public void PriceList__Can_Add_update_item_in_pricelist()
        {
            var pricelist = new PriceList(new List<ShopItem> { new ShopItem("A", 10) });
            Assert.AreEqual(10, pricelist.LookupItem("A").ItemPrice);
            pricelist.UpsertItem(new ShopItem("A", 20));
            Assert.AreEqual(20, pricelist.LookupItem("A").ItemPrice);
        }

        /// <summary>
        /// When I add an item to the pricelist and it's discount would be greater than or equal to the item price then an excessive discount exception is thrown
        /// </summary>
        [TestMethod]
        public void PriceList__ExceptionHandling_ExcessiveDiscount()
        {
            var pricelist = new PriceList();
            var exceptionMessage = "No Exception Caught";
            try
            {
                pricelist.UpsertItem(new ShopItem("A", 10, 2, 20));
            }
            catch (ExcessiveDiscountException e)
            {
                exceptionMessage = e.Message;
            }
            Assert.AreEqual("Cannot add a discount of 20 when buying 2 items for SKU A as the discount per item is greater than or equal to the price 10", exceptionMessage);
        }

    }
}




