using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CheckoutKata.Domain.Exceptions;
using CheckoutKata.Models;

namespace CheckoutKata.Tests
{

    [TestClass]
    public class CheckoutTests
    {
        private Checkout _initialiseCheckout()
        {
            var _shopItemList = new List<ShopItem>();
            _shopItemList.Add(new ShopItem("A", 50, 3, 30));
            _shopItemList.Add(new ShopItem("B", 30, 2, 15));
            _shopItemList.Add(new ShopItem("C", 60));
            _shopItemList.Add(new ShopItem("D", 99));

            return new Checkout(new PriceList(_shopItemList));
        }

        /// <summary>
        /// When I scan an item then the total I am given is the price of that item
        /// </summary>
        [TestMethod]
        public void Checkout__Scan_single_item_returns_price_of_item()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");

            Assert.AreEqual(50, checkout.Total());
        }

        /// <summary>
        /// When I scan a the number of an item that will give me the discount then the total I am given is the expected discount price
        /// </summary>
        [TestMethod]
        public void Checkout__Scan_multiple_same_item_returns_appropriate_discount()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.AreEqual(120, checkout.Total());
        }

        /// <summary>
        /// When I scan a variety of items then the total I am given is the expected price of the entire basket
        /// </summary>
        [TestMethod]
        public void Checkout__Scan_various_items_returns_total_price_for_all()
        {
            var checkout = _initialiseCheckout();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.AreEqual(200, checkout.Total());
        }

        /// <summary>
        /// When I attempt to 'scan' an item which is not in the pricelist then an unknown item exception is thrown
        /// </summary>
        [TestMethod]
        public void Checkout__ExceptionHandling_ScannedUnknownSKU()
        {
            var checkout = _initialiseCheckout();
            var exceptionMessage = "No Exception Caught";
            try
            {
                checkout.Scan("E");
            }
            catch (UnknownItemException e)
            {
                exceptionMessage = e.Message;
            }

            Assert.AreEqual("Unknown item scanned: E", exceptionMessage);
        }

       

    }
}
